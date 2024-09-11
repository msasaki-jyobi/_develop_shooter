using Cysharp.Threading.Tasks;
using develop_common;
using develop_shooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace develop_shooter
{

    public class DamageBox : MonoBehaviour
    {
        public GameObject HitEffect;
        public AudioClip HitSE;
        [SerializeField] private bool IsHitDestroy;

        private void OnCollisionEnter(Collision collision)
        {
            OnHit(collision.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnHit(other.gameObject);
        }

        public async void OnHit(GameObject hit)
        {
            if(hit.TryGetComponent<IHealth>(out var health))
            {
                UtilityFunction.PlayEffect(gameObject, HitEffect);
                AudioManager.Instance.PlayOneShot(HitSE, EAudioType.Se);
                if (IsHitDestroy)
                    Destroy(gameObject);
            }
        }
    }
}
