using develop_common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace develop_shooter
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        private EUnitType unitType = EUnitType.Player;
        public EUnitType UnitType => unitType;
        [field: SerializeField] public int CurrentHealth { get; private set; } = 50;
        public int MaxHealth { get; private set; } = 50;

        public event Action<int, int> UpdateHealthEvent;

        private void Start()
        {
            CurrentHealth = MaxHealth;
            UpdateHealthEvent(CurrentHealth, MaxHealth);
        }
        public void Heal(float amount)
        {
            UpdateHealthEvent(CurrentHealth, MaxHealth);
        }

        public void TakeDamage(DamageValue damageValue)
        {
            if (CurrentHealth >= MaxHealth)
                MaxHealth = CurrentHealth;

            var damage = MaxHealth * 0.1f;

            CurrentHealth -= (int)damage;
            UpdateHealthEvent(CurrentHealth, MaxHealth);
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnHit(collision.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnHit(other.gameObject);
        }

        private void OnHit(GameObject hit)
        {
            if (hit.TryGetComponent<Projectile>(out var projectile))
            {
                DamageValue damageValue = new DamageValue();
                damageValue.Amount = 1;
                TakeDamage(damageValue);
            }
            if (hit.TryGetComponent<DamageBox>(out var damageBox))
            {
                DamageValue damageValue = new DamageValue();
                damageValue.Amount = 1;
                TakeDamage(damageValue);
            }
        }
    }
}

