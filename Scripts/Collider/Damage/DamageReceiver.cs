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
    /// 対象レイヤーに触れたらダメージを受けるクラス
    /// </summary>
    public class DamageReceiver : MonoBehaviour
    {
        //[SerializeField] private SerializableInterface<IHealth> mySerializableInterface;

        public float DamageScale = 1;
        public GameObject HitEffect;
        public AudioClip HitSE;
        //public List<int> TargetLayers = new List<int> { 0, 1, 15 };  // 対象レイヤーのリスト

        private void OnCollisionEnter(Collision collision)
        {
            // 衝突したオブジェクトのレイヤーが対象レイヤーに含まれているかチェック
            //if (TargetLayers.Contains(collision.gameObject.layer))
                OnHit(collision.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            // 衝突したオブジェクトのレイヤーが対象レイヤーに含まれているかチェック
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
