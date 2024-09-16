using develop_common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace develop_shooter
{
    /// <summary>
    /// �Ώۃ��C���[�ɐG�ꂽ��G�t�F�N�g�ƌ��ʉ����Đ������N���X
    /// </summary>
    public class AttackCollider : MonoBehaviour
    {
        public int Amount = 1;
        public GameObject HitEffect;
        public AudioClip HitSE;
        [SerializeField] private bool IsHitDestroy;
        public List<int> TargetLayers = new List<int> { 0, 1, 15 };  // �Ώۃ��C���[�̃��X�g

        private void OnCollisionEnter(Collision collision)
        {
            // �Փ˂����I�u�W�F�N�g�̃��C���[���Ώۃ��C���[�Ɋ܂܂�Ă��邩�`�F�b�N
            if (TargetLayers.Contains(collision.gameObject.layer))
                OnHit(collision.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            // �Փ˂����I�u�W�F�N�g�̃��C���[���Ώۃ��C���[�Ɋ܂܂�Ă��邩�`�F�b�N
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