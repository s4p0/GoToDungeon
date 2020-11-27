using System;
using Assets.Scripts.Actions;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class HealthBarControl : MonoBehaviour
    {
        [Range(0,1)]
        public float scale = 1f;
        private Vector3 currentScale = Vector3.one;
        public GameObject foregroundSprite;

        public IHealth health;
        
        private void Start()
        {
            currentScale = foregroundSprite.transform.localScale;
        }

        private void FixedUpdate()
        {
            SetHealth(health.HealthPercentage);
        }

        internal void SetHealth(float perc)
        {
            currentScale.x = Mathf.Clamp(perc, 0f, 1.065f);
            foregroundSprite.transform.localScale = currentScale;
        }
    }
}
