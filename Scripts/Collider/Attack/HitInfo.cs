using develop_common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace develop_shooter
{
    /// <summary>
    /// 対象レイヤーに触れたらエフェクトと効果音が再生されるクラス
    /// </summary>
    public class HitInfo : MonoBehaviour
    {
        public int Amount = 1;
        public GameObject HitEffect;
        public AudioClip HitSE;
        [SerializeField] private bool IsHitDestroy;
        public List<int> TargetLayers = new List<int> { 0, 10, 15 };  // 対象レイヤーのリスト
        public UnityEvent HitEvent;

        public EUnitType IgnoreHitUnit;

        private GameObject _hit;

       


        private void OnCollisionEnter(Collision collision)
        {
            // 衝突したオブジェクトのレイヤーが対象レイヤーに含まれているかチェック
            if (TargetLayers.Contains(collision.gameObject.layer))
                OnHit(collision.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            // 衝突したオブジェクトのレイヤーが対象レイヤーに含まれているかチェック
            if (TargetLayers.Contains(other.gameObject.layer))
                OnHit(other.gameObject);
        }

        public async void OnHit(GameObject hit)
        {
            UtilityFunction.PlayEffect(gameObject, HitEffect);
            AudioManager.Instance.PlayOneShot(HitSE, EAudioType.Se);
            _hit = hit;
            HitEvent?.Invoke();
            if (IsHitDestroy)
                Destroy(gameObject);
        }

        public void OnFouceJump()
        {
            if(_hit.TryGetComponent<Rigidbody>(out var rigid))
            {
                rigid.AddForce(_hit.transform.up * 5f, ForceMode.Impulse);
            }
        }


    }
}