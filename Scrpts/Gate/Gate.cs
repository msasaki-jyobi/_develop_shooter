using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace develop_shooter
{


    public class Gate : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GateManager.Instance.AddGate();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            OnHit();
        }

        private void OnTriggerEnter(Collider other)
        {
            OnHit();
        }

        private void OnHit()
        {
            GateManager.Instance.PlayGate();
            Destroy(gameObject);
        }
    }
}