using develop_shooter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace develop_shooter
{
    public class BarrelController : MonoBehaviour
    {
        public ExplosiveBarrelScript Barrel;
        public float ExplodeTime = 0;

        private float _timer;

        private void Start()
        {
            Barrel.DestroyEvent += () => Destroy(gameObject);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= ExplodeTime)
                Barrel.explode = true;
        }


    }
}
