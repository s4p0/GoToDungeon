using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class HealthBar : MonoBehaviour
    {
        public float showTime = .5f;
        private float shownTime = 0;



        public Sprite[] sprites = new Sprite[0];

        public IHealth health { get; private set; }

        private SpriteRenderer rend;
        private bool initialized;
        public int previous;
        public bool updated;
        private float status;

        private void Start()
        {
            
        }

        void UpdateHealthStatus()
        {
            var pos = 0;
            if (health.CurrentHealth == health.TotalHealth)
            {
                pos = System.Convert.ToInt32(sprites.Length - 1);
            }
            else
            {
                var ratio = (health.CurrentHealth / health.TotalHealth) * 10;
                pos = System.Convert.ToInt32(Mathf.Clamp(ratio, 1, sprites.Length - 1));
            }

            rend.sprite = sprites[pos];
        }

        public void Initialize()
        {
            health = GetComponentInChildren<IHealth>();
            rend = GetComponent<SpriteRenderer>();
            initialized = true;
        }

        private void Update()
        {
            if (!initialized)
                return;

            updated = status != health.CurrentHealth;
            
            if(updated)
            {
                status = health.CurrentHealth;
                UpdateHealthStatus();

                shownTime = showTime;
            }

            if (shownTime >= 0)
            {
                shownTime -= Time.deltaTime;
                rend.enabled = true;
            }
            else
            {
                rend.enabled = false;
            }


            
        }
    }
}
