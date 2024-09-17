using develop_common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (IsHitDestroy)
                Destroy(gameObject);
        }
    }
}