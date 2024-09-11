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
        private float duration = 1f;  // 赤くなる時間の長さ

        void Start()
        {
            // ポストプロセスボリュームからVignetteを取得
            if (postProcessVolume.profile.TryGetSettings(out vignette))
            {
                // 初期設定
                vignette.intensity.value = 0f;
                vignette.color.value = Color.red;  // Vignetteの色を赤に設定
            }
        }

        // 数秒間赤くする処理
        public void FlashRedScreen()
        {
            StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine()
        {
            float elapsed = 0.2f;
            float halfDuration = duration / 2f;

            // 徐々にVignetteのIntensityを増やす（赤くなる）
            while (elapsed < halfDuration)
            {
                elapsed += Time.deltaTime;
                vignette.intensity.value = Mathf.Lerp(0.2f, 0.45f, elapsed / halfDuration);
                yield return null;
            }

            elapsed = 0f;

            // 徐々に元の状態に戻す
            while (elapsed < halfDuration)
            {
                elapsed += Time.deltaTime;
                vignette.intensity.value = Mathf.Lerp(0.45f, 0f, elapsed / halfDuration);
                yield return null;
            }

            // 完全に元に戻す
            vignette.intensity.value = 0f;
        }
    }

}