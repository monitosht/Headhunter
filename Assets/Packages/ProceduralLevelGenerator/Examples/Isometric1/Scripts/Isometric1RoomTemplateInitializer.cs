﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Edgar.Unity.Examples.Isometric1
{
    public class Isometric1RoomTemplateInitializer : RoomTemplateInitializerBase
    {
        protected override void InitializeTilemaps(GameObject tilemapsRoot)
        {
            var tilemapLayersHandlers = ScriptableObject.CreateInstance<Isometric1TilemapLayersHandler>();
            tilemapLayersHandlers.InitializeTilemaps(tilemapsRoot);
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Dungeon generator/Examples/Isometric 1/Room template")]
        public static void CreateRoomTemplatePrefab()
        {
            RoomTemplateInitializerUtils.CreateRoomTemplatePrefab<Isometric1RoomTemplateInitializer>();
        }
#endif
    }
}