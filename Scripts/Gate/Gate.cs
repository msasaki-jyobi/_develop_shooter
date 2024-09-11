using develop_common;
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
                GateManager.Instance.PlayGate();
                Destroy(gameObject);
            }
          
        }
    }
}