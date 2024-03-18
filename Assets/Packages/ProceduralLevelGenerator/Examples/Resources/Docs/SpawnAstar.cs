using UnityEngine;

namespace Edgar.Unity.Examples.Resources
{
    [CreateAssetMenu(menuName = "Dungeon generator/Examples/Docs/SpawnAstar", fileName = "AstarPostProcess")]
    public class SpawnAstar : DungeonGeneratorPostProcessBase
    {
        public GameObject Astar;

        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {
            Instantiate(Astar);
        }
    }
}