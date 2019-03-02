using Assets.Scripts.Actions;
using UnityEditor;

namespace Assets.Scripts.Enemies
{
    [CustomEditor(typeof(Health), true)]
    [System.Serializable]
    public class Monster : Health
    {
        public enum Size { Small, Medium, Big }


    }
}
