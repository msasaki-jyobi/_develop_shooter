using Cysharp.Threading.Tasks;
using develop_common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace develop_shooter
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        private EUnitType unitType = EUnitType.Player;
        public EUnitType UnitType => unitType;
        [field: SerializeField] public int CurrentHealth { get; private set; } = 50;
        public int MaxHealth { get; private set; } = 50;

        public event Action<int, int> UpdateHealthEvent;
        public event Action DeadEvent;

        private void Start()
        {
            CurrentHealth = MaxHealth;
            UpdateHealthEvent?.Invoke(CurrentHealth, MaxHealth);
        }
        public void Heal(float amount)
        {
            UpdateHealthEvent?.Invoke(CurrentHealth, MaxHealth);
        }

        public void TakeDamage(DamageValue damageValue = null)
        {
            if (CurrentHealth >= MaxHealth)
                MaxHealth = CurrentHealth;

            var damage = MaxHealth * 0.1f;

            CurrentHealth -= (int)damage;
            UpdateHealthEvent?.Invoke(CurrentHealth, MaxHealth);

            // ダメージ演出

            // 死亡時
            if (CurrentHealth <= 0)
            {
                DeadEvent?.Invoke();
                Die();
            }
        }

        private async Task Die()
        {
            DeadEvent?.Invoke();
            Destroy(gameObject);
            if (Camera.main.TryGetComponent<Collider>(out var collider))
                collider.enabled = true;
            if (Camera.main.TryGetComponent<Rigidbody>(out var rigid))
            {
                rigid.transform.parent = null;
                rigid.isKinematic = false;
                var x = UnityEngine.Random.Range(-4, 4);
                var y = UnityEngine.Random.Range(0, 4);
                var z = UnityEngine.Random.Range(-4, 4);
                rigid.AddForce(transform.right * x + transform.up * y + transform.forward * z, ForceMode.Impulse);
                await UniTask.Delay(1500);
                rigid.isKinematic = true;
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
            if (hit.TryGetComponent<Projectile>(out var projectile))
            {
                TakeDamage();

                ScreenFlash.Instance.FlashRedScreen();
            }
            if (hit.TryGetComponent<DamageBox>(out var damageBox))
            {
                TakeDamage();

                ScreenFlash.Instance.FlashRedScreen();
 
            }
        }
    }
}

