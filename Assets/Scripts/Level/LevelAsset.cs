using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Level
{
    [CreateAssetMenu(menuName = "GoToDungeon/New/Level")]
    public class LevelAsset : ScriptableObject
    {
        public int levelOrder;
        public int levelName;
        public int numberOfRooms;
        public int numberOfSmallMonsters;
        public int numberOfMediumMonsters;
        public int numberOfBigMonsters;
    }
}
