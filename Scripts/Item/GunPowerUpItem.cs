using develop_common;
using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace develop_shooter
{
    public class GunPowerUpItem : MonoBehaviour
    {
        private Weapon _gunWeapon;

        public int SetRate = 600;
        public int SetImpulus = 400;
        public int SetAmount = 999;
        [Space(20)]
        public bool IsRandom;
        public int MinRate = 10;
        public int MaxRate = 1000;
        public int MinImpulus = 10;
        public int MaxImpulus = 1000;
        public int MinAmount = 1;
        public int MaxAmount = 300;


        // P_LPSP_WEP_AR_01 Weapon
        // RoundsParMinites 低いとレート遅くなる
        // Projectile Impulse 低いと、落下する（重くなる）
        // private Ammountion Current リアルタイム段数


        // SM_AR_01_Magazine_Default
        // Magazine
        // マガジン上限

        private void Start()
        {
            _gunWeapon = GameObject.FindAnyObjectByType<Weapon>();

            if (IsRandom)
                SetRandomParameter();
            //var ar = GameObject.Find("P_LPSP_WEP_AR_01 Weapon");
            //if(ar.TryGetComponent<Weapon>(out var weapon))
            //{
            //}
            //var ar_magazine = GameObject.Find("SM_AR_01_Magazine_Default");
            //if(ar_magazine.TryGetComponent<Magazine>(out var magazine))
            //{

                //}
        }



        private void OnCollisionEnter(Collision collision)
        {
            OnHit(collision.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnHit(other.gameObject);
        }

        private void OnHit(GameObject hit)
        {
            if(hit.TryGetComponent<IHealth>(out var health))
            {
                if(health.UnitType == EUnitType.Player)
                {
                    // アイテム取得処理
                    _gunWeapon.ChangeFireLate(SetRate, SetImpulus, SetAmount);
                    Destroy(gameObject);
                }
            }
        }

        public void SetRandomParameter()
        {
            SetRate = Random.Range(MinRate, MaxRate);
            SetImpulus = Random.Range(MinImpulus, MaxImpulus);
            SetAmount = Random.Range(MinAmount, MaxAmount);
        }
    }

}
