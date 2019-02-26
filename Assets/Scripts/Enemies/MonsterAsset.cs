using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "GoToDungeon/New/Monster")]
    public class MonsterAsset : ScriptableObject
    {
        public Monster.Size size;
        public int minHealth;
        public int maxHealth;
        public GameObject[] prefabs;
    }
}
