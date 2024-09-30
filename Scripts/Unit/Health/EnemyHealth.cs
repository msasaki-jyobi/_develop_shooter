using Cysharp.Threading.Tasks;
using develop_common;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace develop_shooter
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField]
        private EUnitType unitType = EUnitType.Player;
        public EUnitType UnitType => unitType;
        [field: SerializeField] public int CurrentHealth { get; private set; } = 5;
        public int MaxHealth { get; private set; } = 50;

        public float DeadDelay = 0f;

        public GameObject DestroyEffect;
        public GameObject OverKillEffect;
        public AnimatorStateController AnimatorStateController;

        private void Awake()
        {
            if (ScoreManager.Instance != null)
                ScoreManager.Instance.AddTarget();
        }

        public void Heal(float amount)
        {
            throw new System.NotImplementedException();
        }

        public async void TakeDamage(DamageValue damageValue = null)
        {
            if (damageValue == null)
            {
                damageValue = new DamageValue();
                damageValue.Amount = 1;
            }

            Debug.Log("Damage");
            CurrentHealth -= damageValue.Amount;
            if (CurrentHealth <= 0)
            {
                if (ScoreManager.Instance != null)
                    ScoreManager.Instance.AddScore();

                UtilityFunction.PlayEffect(gameObject, DestroyEffect);

                if (CurrentHealth <= -1)
                    if (DestroyEffect != null)
                        UtilityFunction.PlayEffect(gameObject, OverKillEffect);

                // Animator次第で
                if (AnimatorStateController != null)
                    if (AnimatorStateController.Animator != null)
                    {
                        if (gameObject.TryGetComponent<CapsuleCollider>(out var capsuleCollider))
                            capsuleCollider.enabled = false;
                        if (gameObject.TryGetComponent<NavMeshController>(out var navMeshController))
                            navMeshController.OnStopAgent();

                        AnimatorStateController.StatePlay("Dead", EStatePlayType.SinglePlay, false);
                        await UniTask.Delay(3000);
                        Destroy(gameObject);
                        return;
                    }


                Destroy(gameObject, DeadDelay);
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