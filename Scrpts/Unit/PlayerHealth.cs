using develop_common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace develop_shooter
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        private EUnitType unitType = EUnitType.Player;
        public EUnitType UnitType => unitType;
        [field: SerializeField] public float CurrentHealth { get; private set; } = 100f;

        public void Heal(float amount)
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(DamageValue damageValue)
        {
            throw new System.NotImplementedException();
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
        }
    }
}

