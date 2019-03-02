﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    [System.Serializable]
    
    public class Health : MonoBehaviour, IHealth
    {
        public int TotalHealth { get; set; }

        public int CurrentHealth { get; set; }

        public void Start()
        {
            CurrentHealth = TotalHealth;
        }

        public virtual void Damage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
                Die();
        }

        public void Heal(int healthPoints)
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

        public void IncreaseTotal(int total, bool updateHealth)
        {
            TotalHealth += total;
            if (updateHealth)
                Restore();
        }
    }
}
