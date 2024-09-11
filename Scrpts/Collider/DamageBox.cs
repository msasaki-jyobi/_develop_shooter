using develop_common;
using develop_shooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void OnHit(GameObject hit = null)
    {

        UtilityFunction.PlayEffect(gameObject, HitEffect);
        AudioManager.Instance.PlayOneShot(HitSE, EAudioType.Se);

        ScreenFlash.Instance.FlashRedScreen();

        if (IsHitDestroy)
            Destroy(gameObject);
    }
}
