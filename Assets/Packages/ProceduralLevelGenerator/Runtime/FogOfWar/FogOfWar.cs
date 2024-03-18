﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Edgar.Unity
{
    /// <summary>
    /// The main class of the Fog of War feature. (PRO version only)
    /// </summary>
    /// <remarks>
    /// This class has to be placed on the game object with the camera to which should the effect be applied.
    /// See online documentation for information on how to use this feature.
    /// </remarks>
    public class FogOfWar : MonoBehaviour
    {
        /// <summary>
        /// Color of tiles that are hidden in the fog.
        /// </summary>
        public Color FogColor = UnityEngine.Color.black;

        /// <summary>
        /// Mode of the Fog of War effect. Is is either Wave or FadeIn.
        /// </summary>
        /// <remarks>
        /// Wave - tiles are revealed based on the distance from the player.
        /// FadeIn - tiles are revealed uniformly.
        /// 
        /// Some fields in the inspector are only revealed when a specific mode is used.
        /// </remarks>
        public FogOfWarMode Mode = FogOfWarMode.Wave;

        /// <summary>
        /// Transition mode of the effect.
        /// </summary>
        /// <remarks>
        /// The Smooth mode interpolates  values between neighboring tiles while the TileBased mode does not.
        /// </remarks>
        public FogOfWarTransitionMode TransitionMode = FogOfWarTransitionMode.Smooth;

        /// <summary>
        /// Speed of the wave (tiles per second) when in Wave mode. 
        /// </summary>
        public float WaveSpeed = 10;

        /// <summary>
        /// How much time should the tile be completely hidden before revealing it (see online docs).
        /// </summary>
        [Range(0f, 1f)]
        public float WaveRevealThreshold = 0.5f;

        /// <summary>
        /// How long should it take to reveal a tile in the fadeIn mode (in seconds).
        /// </summary>
        public float FadeInDuration = 1;

        /// <summary>
        /// Whether to reveal nearby tiles of neighboring corridors.
        /// </summary>
        [Tooltip("Whether to reveal nearby tiles of neighboring corridors.")]
        public bool RevealCorridors = true;

        /// <summary>
        /// How many tiles of neighboring corridors should be revealed.
        /// </summary>
        [Tooltip("How many tiles of neighboring corridors should be revealed.")]
        public int RevealCorridorsTiles = 1;

        /// <summary>
        /// Whether to reveal neighboring corridor tiles gradually.
        /// </summary>
        public bool RevealCorridorsGradually = true;

        [SerializeField, HideInInspector]
        private Transform WaveOrigin;

        [SerializeField, HideInInspector]
        private GameObject GeneratedLevelRoot;

        [SerializeField, HideInInspector]
        private List<RoomInstance> RevealedRooms;

        private Material material;

        private FogOfWarVisionGrid visionGrid;

        private FogOfWarVisionGrid.VisionTextures visionTextures;

        private int lastCoroutineId = 1;

        private Dictionary<Vector2Int, int> tilesLocks;

        private static readonly int ViewProjInv = Shader.PropertyToID("_ViewProjInv");
        private static readonly int VisionTex = Shader.PropertyToID("_VisionTex");
        private static readonly int VisionTex2 = Shader.PropertyToID("_VisionTex2");
        private static readonly int VisionTexOffset = Shader.PropertyToID("_VisionTexOffset");
        private static readonly int VisionTexSize = Shader.PropertyToID("_VisionTexSize");
        private static readonly int Color = Shader.PropertyToID("_FogColor");

        private int stepsSinceSetup = 0;
        private bool materialRetrieved = false;

        /// <summary>
        /// Instance of the Fog of War script. The class is a singleton.
        /// </summary>
        public static FogOfWar Instance { get; private set; }

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            } 
            else if (Instance != this)
            {
                Debug.LogError($"There should be only a single instance of the {nameof(FogOfWar)} component.");
            }
        }

        /// <summary>
        /// Gets all the rooms that are currently revealed.
        /// </summary>
        /// <remarks>
        /// This method might be useful if you want to save the game and then load it later.
        /// Call this method when saving the game and then use RevealRooms() when loading the game.
        /// </remarks>
        /// <returns></returns>
        public IReadOnlyList<RoomInstance> GetRevealedRooms()
        {
            return RevealedRooms;
        }

        /// <summary>
        /// Setups the component, must be called every time after a level is generated.
        /// </summary>
        /// <remarks>
        /// The waveOrigin parameter is optional if the FadeIn mode is used.
        /// The waveOrigin parameter is optional if the waveOrigin position is provided when calling one of the RevealRoom methods.
        /// </remarks>
        /// <param name="generatedLevelRoot">Root game object of the generated level.</param>
        /// <param name="waveOrigin">Origin of the fog reveal animation if the Wave mode is used.</param>
        public void Setup(GameObject generatedLevelRoot, Transform waveOrigin = null)
        {
            if (generatedLevelRoot == null) throw new ArgumentNullException(nameof(generatedLevelRoot));

            StopAllCoroutines();
            RevealedRooms = new List<RoomInstance>();
            tilesLocks = new Dictionary<Vector2Int, int>();
            GeneratedLevelRoot = generatedLevelRoot;
            visionGrid = new FogOfWarVisionGrid();
            WaveOrigin = waveOrigin;
            material = new Material(Shader.Find("Edgar/FogOfWar"));
            visionTextures = visionGrid.GetVisionTextures();

            stepsSinceSetup = 0;
        }

        private void Update()
        {
            if (visionGrid != null && visionGrid.HasChanges())
            {
                visionTextures = visionGrid.GetVisionTextures();
                visionGrid.ResetHasChanges();
            }

            if (visionGrid != null)
            {
                stepsSinceSetup++;

                if (stepsSinceSetup % 5000 == 10 && !materialRetrieved)
                {
                    Debug.LogWarning("The Fog of War feature is enabled but the shader was not yet applied. It seems like you are using URP or LWRP and did not add the custom render feature. Please visit the documentation to see which additional steps are needed to enable this feature in URP and LWRP.");
                }
            }
        }

        /// <summary>
        /// Reveals a given room.
        /// </summary>
        /// <param name="room">Room to be revealed.</param>
        /// <param name="waveOrigin">Origin of the wave if the WaveMode is used. If provided, it overrides the waveOrigin given to the Setup() method.</param>
        /// <param name="revealImmediately">Whether to reveal the room immediately without any animation.</param>
        public void RevealRoom(RoomInstance room, Vector3? waveOrigin = null, bool revealImmediately = false)
        {
            RevealRooms(new List<RoomInstance>() {room}, waveOrigin, revealImmediately);
        }

        /// <summary>
        /// Reveals a given room and all its neighboring rooms.
        /// </summary>
        /// <param name="room">Room to be revealed.</param>
        /// <param name="waveOrigin">Origin of the wave if the WaveMode is used. If provided, it overrides the waveOrigin given to the Setup() method.</param>
        /// <param name="revealImmediately">Whether to reveal the room immediately without any animation.</param>
        public void RevealRoomAndNeighbors(RoomInstance room, Vector3? waveOrigin = null, bool revealImmediately = false)
        {
            // Compute which rooms should be revealed
            var roomsToExplore = new List<RoomInstance>()
            {
                room
            };

            foreach (var roomToExplore in room.Doors.Select(x => x.ConnectedRoomInstance))
            {
                roomsToExplore.Add(roomToExplore);
            }

            RevealRooms(roomsToExplore, waveOrigin, revealImmediately);
        }

        /// <summary>
        /// Reveals given rooms.
        /// </summary>
        /// <param name="rooms">Rooms to be revealed.</param>
        /// <param name="waveOrigin">Origin of the wave if the WaveMode is used. If provided, it overrides the waveOrigin given to the Setup() method.</param>
        /// <param name="revealImmediately">Whether to reveal the rooms immediately without any animation.</param>
        public void RevealRooms(List<RoomInstance> rooms, Vector3? waveOrigin = null, bool revealImmediately = false)
        {
            if (Mode == FogOfWarMode.Wave && waveOrigin == null && WaveOrigin == null)
            {
                throw new ArgumentException($"When the {FogOfWarMode.Wave} mode is used, either the {nameof(waveOrigin)} that is passed to this method or that is used in the Setup() method must not be null.");
            }

            StartCoroutine(RevealRoomCoroutine(rooms, waveOrigin ?? WaveOrigin.transform.position, revealImmediately));
        }

        /// <summary>
        /// Reloads the component, taking already revealed rooms and revealing them again.
        /// </summary>
        /// <param name="revealImmediately">Whether to reveal the rooms immediately without any animation.</param>
        public void Reload(bool revealImmediately = false)
        {
            var rooms = RevealedRooms.ToList();
            Setup(GeneratedLevelRoot, WaveOrigin);
            RevealRooms(rooms, revealImmediately: revealImmediately);
        }

        private Dictionary<Vector2Int, TileInfo> GetTilesToReveal(List<RoomInstance> roomsToReveal)
        {
            var tiles = new Dictionary<Vector2Int, TileInfo>();
            var revealedFogValue = 1f;

            // Go through the rooms and set the target fog value
            foreach (var room in roomsToReveal)
            {
                foreach (var point in GetPolygonPoints(room.OutlinePolygon))
                {
                    tiles[point] = new TileInfo(revealedFogValue, false);
                }
            }

            // If interpolation is enabled, we must take a special care of tiles that are on the outline of revealed rooms.
            // The reason for that is that such tiles usually neighbor with tiles that are not yet revealed. These not-yet-revealed
            // tiles affect the appearance of outline tiles - the pixels that are closer to the outline appear to be still partially
            // covered in the fog because of the interpolation.
            //
            // The solution to this problem is to add an additional row of tiles next to the outline tiles. These tiles will be
            // hidden in the fog but we will treat them as if we wanted to reveal them to trick the interpolation mechanism.
            if (TransitionMode == FogOfWarTransitionMode.Smooth)
            {
                var extendedOutline = GetExtendedOutline(new HashSet<Vector2Int>(tiles.Keys));
                var interpolationHelperPoints = extendedOutline.Where(x => visionGrid.GetTile(x).IsRevealed == false).ToList();

                foreach (var point in interpolationHelperPoints)
                {
                    tiles.Add(point, new TileInfo(revealedFogValue, true));
                }
            }

            // If we want to also reveal parts of neighboring corridors, this is the place to compute which tiles should be revealed
            if (RevealCorridors && RevealCorridorsTiles > 0)
            {
                foreach (var room in roomsToReveal)
                {
                    foreach (var door in room.Doors)
                    {
                        var neighbor = door.ConnectedRoomInstance;

                        // We only need corridors that should not be completely revealed
                        if (!neighbor.IsCorridor || roomsToReveal.Contains(neighbor))
                        {
                            continue;
                        }

                        var neighborOutline = GetPolygonPoints(neighbor.OutlinePolygon);

                        var doorPointRaw = door.DoorLine.GetPoints().First();
                        var doorPoint = (Vector2Int)(doorPointRaw + room.Position);

                        // Based on whether the door is horizontal or vertical, we will compute the distance of corridor tiles from the door tile
                        var getDistance = door.IsHorizontal 
                            ? (Func<Vector2Int, float>) (x => Mathf.Abs(x.y - doorPoint.y)) 
                            : x => Mathf.Abs(x.x - doorPoint.x);

                        // Using the getDistance() function compute whit corridor tiles should be revealed
                        var closeTiles = neighborOutline
                            .Where(x => getDistance(x) >= 1 && getDistance(x) <= RevealCorridorsTiles)
                            .Where(x => !visionGrid.GetTile(x).IsRevealed)
                            .ToList();

                        var extendedCloseTiles = TransitionMode == FogOfWarTransitionMode.Smooth 
                            ? GetExtendedOutline(new HashSet<Vector2Int>(closeTiles))
                                .Where(x => !visionGrid.GetTile(x).IsRevealed)
                                .ToList()
                            : new List<Vector2Int>();

                        if (closeTiles.Count == 0)
                        {
                            continue;
                        }

                        var maxDistance = closeTiles.Union(extendedCloseTiles).Max(getDistance);

                        foreach (var closeTile in closeTiles.Union(extendedCloseTiles))
                        {
                            var distance = getDistance(closeTile);

                            if (TransitionMode == FogOfWarTransitionMode.Smooth )
                            {
                                distance += 0.5f;
                            }

                            // Some of the extendedCloseTiles may be closer than that
                            if (distance < 1)
                            {
                                continue;
                            }

                            var ratio = distance / maxDistance;
                            var minimumFogValue = TransitionMode == FogOfWarTransitionMode.Smooth  ? 0f : 0.15f;

                            var targetFogValue = RevealCorridorsGradually ? Mathf.Lerp(0.7f, minimumFogValue, ratio) : revealedFogValue;

                            if (RevealCorridorsTiles == 1 && TransitionMode != FogOfWarTransitionMode.Smooth  && RevealCorridorsGradually)
                            {
                                targetFogValue = 0.5f;
                            }

                            // Do not override existing tiles
                            if (tiles.TryGetValue(closeTile, out var existingTile) && !existingTile.IsInterpolationHelper)
                            {
                                continue;
                            }

                            tiles[closeTile] = new TileInfo(targetFogValue, extendedCloseTiles.Contains(closeTile));
                        }
                    }
                }
            }

            return tiles;
        }

        private IEnumerator RevealRoomCoroutine(List<RoomInstance> rooms, Vector2 playerPosition, bool revealImmediately = false)
        {
            if (GeneratedLevelRoot != null)
            {
                var tilemapsRoot = GeneratedLevelRoot.gameObject.transform.Find("Tilemaps");

                if (tilemapsRoot != null)
                {
                    playerPosition -= (Vector2)tilemapsRoot.position;
                }
            }

            playerPosition -= new Vector2(0.5f, 0.5f); // TODO: should we keep it like this?

            var coroutineId = lastCoroutineId++;

            var notYetRevealedRooms = rooms
                .Where(x => !RevealedRooms.Contains(x))
                .ToList();

            if (notYetRevealedRooms.Count == 0)
            {
                yield break;
            }
            var roomsToExplore = new List<RoomInstance>();

            // Compute which rooms should be revealed
            roomsToExplore.AddRange(notYetRevealedRooms);
            RevealedRooms.AddRange(notYetRevealedRooms);

            // Compute which tiles should be revealed
            var points = GetTilesToReveal(roomsToExplore);

            // Get the initial values of points that should be revealed
            var initialValues = new Dictionary<Vector2Int, FogOfWarVisionGrid.TileInfo>();
            foreach (var point in points.Keys)
            {
                initialValues[point] = visionGrid.GetTile(point);
            }

            // Lock all the points to this coroutine id
            foreach (var point in points.Keys)
            {
                tilesLocks[point] = coroutineId;
            }

            // Measure the time since the start of the coroutine
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                var allRevealed = true;
                var time = stopwatch.ElapsedMilliseconds / 1000f;

                foreach (var pair in points)
                {
                    var point = pair.Key;
                    var tileInfo = pair.Value;
                    var targetFogValue = tileInfo.TargetFogValue;
                    
                    // Check that the point is locked by this coroutine to prevent multiple coroutines changing the same tile
                    if (!tilesLocks.TryGetValue(point, out var lockValue) || lockValue != coroutineId)
                    {
                        continue;
                    }

                    var initialValue = initialValues[point];
                    var initialFogValue = initialValue.Value;

                    float fogValue;

                    var distanceVector = point - playerPosition;
                    var distance = distanceVector.magnitude;

                    if (revealImmediately)
                    {
                        fogValue = targetFogValue;
                    } 
                    else if (Mode == FogOfWarMode.Wave)
                    {
                        var timeToReveal = distance / WaveSpeed;
                        var timeHidden = WaveRevealThreshold * timeToReveal;

                        // If the time should be completely hidden
                        if (time < timeHidden)
                        {
                            fogValue = initialFogValue;
                        }
                        // Else if we should gradually reveal the tile
                        else
                        {
                            fogValue = Mathf.Lerp(initialFogValue, targetFogValue, (time - timeHidden) / (timeToReveal - timeHidden));
                            fogValue = Mathf.Clamp(fogValue, initialFogValue, targetFogValue);
                        }
                    }
                    else if (Mode == FogOfWarMode.FadeIn)
                    {
                        if (FadeInDuration == 0)
                        {
                            fogValue = targetFogValue;
                        }
                        else
                        {
                            fogValue = Mathf.Lerp(initialFogValue, targetFogValue, time / FadeInDuration);
                        }
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(nameof(Mode));
                    }

                    if (TransitionMode == FogOfWarTransitionMode.Smooth)
                    {
                        visionGrid.SetTile(point, new FogOfWarVisionGrid.TileInfo(
                            isInterpolated: !tileInfo.IsInterpolationHelper,
                            value: tileInfo.IsInterpolationHelper ? 0 : targetFogValue,
                            valueInterpolated: fogValue,
                            isRevealed: !tileInfo.IsInterpolationHelper
                        ));
                    }
                    else
                    {
                        visionGrid.SetTile(point, new FogOfWarVisionGrid.TileInfo(
                            isInterpolated: false,
                            value: fogValue,
                            valueInterpolated: fogValue,
                            isRevealed: true
                        ));
                    }

                    if (Math.Abs(fogValue - targetFogValue) > float.Epsilon)
                    {
                        allRevealed = false;
                    }
                }

                if (allRevealed)
                {
                    // Release all points that were locked by this coroutine
                    foreach (var lockPair in tilesLocks.Where(x => x.Value == coroutineId).ToList())
                    {
                        tilesLocks.Remove(lockPair.Key);
                    }

                    break;
                }

                yield return new WaitForSeconds(1/30f);
            }
        }

        private HashSet<Vector2Int> GetPolygonPoints(Polygon2D polygon)
        {
            var points = new HashSet<Vector2Int>();

            foreach (var point in polygon.GetAllPoints())
            {
                points.Add(point);
                points.Add(point + Vector2Int.up);
                points.Add(point + Vector2Int.right);
                points.Add(point + Vector2Int.up + Vector2Int.right);
            }

            return points;
        }

        private HashSet<Vector2Int> GetExtendedOutline(HashSet<Vector2Int> points)
        {
            var extendedPoints = new HashSet<Vector2Int>();

            foreach (var point in points)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        var newPoint = point + new Vector2Int(i, j);

                        if (!points.Contains(newPoint))
                        {
                            extendedPoints.Add(newPoint);
                        }
                    }
                }
            }

            return extendedPoints;
        }

        /// <summary>
        /// Gets the updated material that can be used to apply the image post-processing effect.
        /// </summary>
        /// <remarks>
        /// This method must be called in each frame to get the current fog of war information.
        /// This method is usually called internally by the tool so you do not have to call it manually.
        /// </remarks>
        /// <returns></returns>
        public Material GetMaterial()
        {
            if (visionTextures == null)
            {
                return null;
            }

            materialRetrieved = true;

            var camera = GetComponent<Camera>();

            if (camera == null)
            {
                Debug.LogError($"The {nameof(FogOfWar)} component must be attached to the game object that holds the main camera of the game.");
                throw new ArgumentException();
            }

            var viewMat = camera.worldToCameraMatrix;
            var projMat = GL.GetGPUProjectionMatrix(camera.projectionMatrix, false);
            var viewProjMat = (projMat * viewMat);
            var offset = (Vector3) (Vector3Int) visionTextures.Offset;
            offset -= new Vector3(1, 1);

            if (GeneratedLevelRoot != null)
            {
                var tilemapsRoot = GeneratedLevelRoot.gameObject.transform.Find("Tilemaps");

                if (tilemapsRoot != null)
                {
                    offset += tilemapsRoot.position;
                }
            }
            
            material.SetMatrix(ViewProjInv, viewProjMat.inverse);
            material.SetTexture(VisionTex, visionTextures.Texture);
            material.SetTexture(VisionTex2, visionTextures.TextureInterpolated);
            material.SetVector(VisionTexOffset, offset);
            material.SetVector(VisionTexSize, new Vector4(visionTextures.Texture.width, visionTextures.Texture.height));
            material.SetColor(Color, FogColor);

            return material;
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (visionTextures == null)
            {
                Graphics.Blit(source, destination);
                return;
            }

            Graphics.Blit(source, destination, GetMaterial());
        }

        private void OnValidate()
        {
            WaveSpeed = Mathf.Max(0.001f, WaveSpeed);
            RevealCorridorsTiles = Math.Max(0, RevealCorridorsTiles);
            FadeInDuration = Math.Max(0, FadeInDuration);
        }

        private class TileInfo
        {
            public float TargetFogValue { get; }

            public bool IsInterpolationHelper { get; }

            public TileInfo(float targetFogValue, bool isInterpolationHelper)
            {
                IsInterpolationHelper = isInterpolationHelper;
                TargetFogValue = targetFogValue;
            }
        }
    }
}