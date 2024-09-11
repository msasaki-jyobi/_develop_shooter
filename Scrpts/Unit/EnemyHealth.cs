using develop_common;
using System.Collections;
using UnityEngine;

namespace develop_shooter
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField]
        private EUnitType unitType = EUnitType.Player;
        public EUnitType UnitType => unitType;
        [field: SerializeField] public int CurrentHealth { get; private set; } = 5;
        public int MaxHealth { get; private set; } = 50;

        public ExplosiveBarrelScript Smile;

        private void Awake()
        {
            if (ScoreManager.Instance != null)
                ScoreManager.Instance.AddTarget();
        }

        public void Heal(float amount)
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(DamageValue damageValue = null)
        {
            Debug.Log("Damage");
            CurrentHealth -= 1;
            if (CurrentHealth <= 0)
            {
                if (ScoreManager.Instance != null)
                    ScoreManager.Instance.AddScore();

                if (CurrentHealth <= -1)
                    if (Smile != null)
                        Smile.explode = true;

                Destroy(gameObject, 0.25f);
            }
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
            // 銃弾なら
            if (hit.TryGetComponent<Projectile>(out var projectile))
            {
                TakeDamage();
            }
        }
    }
}