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
        //[SerializeField] private SerializableInterface<IHealth> mySerializableInterface;

        public float DamageScale = 1;
        public GameObject HitEffect;
        public AudioClip HitSE;
        //public List<int> TargetLayers = new List<int> { 0, 1, 15 };  // �Ώۃ��C���[�̃��X�g

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
            if(hit.TryGetComponent<AttackCollider>(out var attackCollider))
            {
                if (gameObject.TryGetComponent<IHealth>(out var health))
                {
                    var damageValue = new DamageValue();
                    var damageAmount = 1;

                    damageAmount = attackCollider.Amount;


                    damageValue.Amount = damageAmount * (int)DamageScale;
                    health.TakeDamage(damageValue);
                }

                UtilityFunction.PlayEffect(gameObject, HitEffect);
                AudioManager.Instance.PlayOneShot(HitSE, EAudioType.Se);
            }
        }
    }
}
