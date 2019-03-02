using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class HealthBarControl : MonoBehaviour
    {
        [Range(0,1)]
        public float scale = 1f;
        private Vector3 current;

        public GameObject foregroundSprite;


        private void Start()
        {
            current = foregroundSprite.transform.localScale;
        }
        private void Update()
        {
            current.x = scale;
            foregroundSprite.transform.localScale = current;
        }
    }
}
