using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    
    public class Health : MonoBehaviour, IHealth
    {
        public UI.HealthBarControl control;

        [SerializeField]
        private float currentHealth;
        public float CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        [SerializeField]
        private float totalHealth;
        private Vector3 controlOffset;
        private Vector3 controlPosition;

        public float TotalHealth
        {
            get { return totalHealth; }
            set { totalHealth = value; }
        }

        public void Start()
        {
            CurrentHealth = TotalHealth;
            control.health = this;
            controlOffset = control.transform.position;
            controlPosition = control.transform.position;
        }

        public void FixedUpdate()
        {
            controlPosition = transform.position;
            controlPosition.x += controlOffset.x;
            controlPosition.y += controlOffset.y;
            control.transform.position = controlPosition;
        }

        public float HealthPercentage
        {
            get { return currentHealth / totalHealth; }
        }

        public virtual void Damage(float damage)
        {
            CurrentHealth -= damage;
            
            if (CurrentHealth <= 0)
                Die();
        }

        public void Heal(float healthPoints)
        {
            CurrentHealth += healthPoints;
            if (CurrentHealth > TotalHealth)
                CurrentHealth = TotalHealth;
        }

        public void Restore()
        {
            CurrentHealth = TotalHealth;
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        public void IncreaseTotal(float total, bool updateHealth)
        {
            TotalHealth += total;
            if (updateHealth)
                Restore();
        }
    }
}
