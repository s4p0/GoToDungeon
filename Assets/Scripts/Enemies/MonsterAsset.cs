using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    [CreateAssetMenu(menuName = "GoToDungeon/New/Monster")]
    public class MonsterAsset : ScriptableObject
    {
        [SerializeField]
        public Monster.Size size;
        [SerializeField]
        public int minHealth;
        [SerializeField]
        public int maxHealth;
        [SerializeField]
        public GameObject[] prefabs;
    }
}
