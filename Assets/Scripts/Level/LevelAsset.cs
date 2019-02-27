using Assets.Scripts.Enemies;
using Level;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Level
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "GoToDungeon/New/Level")]
    public class LevelAsset : ScriptableObject
    {
        public int levelOrder;
        public string levelName;
        public int numberOfRooms;
        public int numberOfSmallMonsters;
        public int numberOfMediumMonsters;
        public int numberOfBigMonsters;

        public Room[] rooms;
        public Room[] aisles;

        [Range(0,1)]
        public float sidewaysProbability;

        
        public MonsterAsset[] monsters;
    }
}
