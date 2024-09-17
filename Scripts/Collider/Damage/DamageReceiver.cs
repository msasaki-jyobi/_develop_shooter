using Cysharp.Threading.Tasks;
using develop_common;
using develop_shooter;
using System.Collections;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace develop_shooter
{
    /// <summary>
    /// �Ώۃ��C���[�ɐG�ꂽ��_���[�W���󂯂�N���X
    /// </summary>
    public class DamageReceiver : MonoBehaviour
    {
        [SerializeField] private SerializableInterface<IHealth> _health = new SerializableInterface<IHealth>();

        public float DamageScale = 1;
        public GameObject DamageEffect;
        public AudioClip DamageSE;
        //public List<int> TargetLayers = new List<int> { 0, 1, 15 };  // �Ώۃ��C���[�̃��X�g

        private void Start()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            // �Փ˂����I�u�W�F�N�g�̃��C���[���Ώۃ��C���[�Ɋ܂܂�Ă��邩�`�F�b�N
            //if (TargetLayers.Contains(collision.gameObject.layer))
                OnHit(collision.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            // �Փ˂����I�u�W�F�N�g�̃��C���[���Ώۃ��C���[�Ɋ܂܂�Ă��邩�`�F�b�N
            //if (TargetLayers.Contains(other.gameObject.layer))
                OnHit(other.gameObject);
        }

        public async void OnHit(GameObject hit)
        {
            if(hit.TryGetComponent<HitInfo>(out var hitInfo))
            {
                if (_health != null && _health.Value != null)
                {
                    var damageValue = new DamageValue();
                    var damageAmount = hitInfo.Amount;
                    damageValue.Amount = damageAmount * (int)DamageScale;

                    Debug.Log($"Health:{_health}, DamageValue :{damageValue}");
                    _health.Value.TakeDamage(damageValue);
                }
                else
                {
                    Debug.LogWarning("Health component or value is not assigned.");
                }

                UtilityFunction.PlayEffect(gameObject, DamageEffect);
                AudioManager.Instance.PlayOneShot(DamageSE, EAudioType.Se);
            }

        }
    }
}
