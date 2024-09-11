using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace develop_shooter
{

    public class ScreenFlash : SingletonMonoBehaviour<ScreenFlash>
    {
        public PostProcessVolume postProcessVolume;
        private Vignette vignette;
        private float duration = 1f;  // �Ԃ��Ȃ鎞�Ԃ̒���

        void Start()
        {
            // �|�X�g�v���Z�X�{�����[������Vignette���擾
            if (postProcessVolume.profile.TryGetSettings(out vignette))
            {
                // �����ݒ�
                vignette.intensity.value = 0f;
                vignette.color.value = Color.red;  // Vignette�̐F��Ԃɐݒ�
            }
        }

        // ���b�ԐԂ����鏈��
        public void FlashRedScreen()
        {
            StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine()
        {
            float elapsed = 0.2f;
            float halfDuration = duration / 2f;

            // ���X��Vignette��Intensity�𑝂₷�i�Ԃ��Ȃ�j
            while (elapsed < halfDuration)
            {
                elapsed += Time.deltaTime;
                vignette.intensity.value = Mathf.Lerp(0.2f, 0.45f, elapsed / halfDuration);
                yield return null;
            }

            elapsed = 0f;

            // ���X�Ɍ��̏�Ԃɖ߂�
            while (elapsed < halfDuration)
            {
                elapsed += Time.deltaTime;
                vignette.intensity.value = Mathf.Lerp(0.45f, 0f, elapsed / halfDuration);
                yield return null;
            }

            // ���S�Ɍ��ɖ߂�
            vignette.intensity.value = 0f;
        }
    }

}