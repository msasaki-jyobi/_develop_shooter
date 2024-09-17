using develop_common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace develop_shooter
{
    public class EasyJump : MonoBehaviour
    {
        private Rigidbody _rigidBody;
        [SerializeField] private float _jumpPower = 30f;
        [SerializeField] private LineData _groundData;

        [SerializeField] private float _drag = 0;
        [SerializeField] private float _angularDrag = 0.05f;


        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _rigidBody.drag = _drag;
            _rigidBody.angularDrag = _angularDrag;
            _rigidBody.interpolation = RigidbodyInterpolation.None;
        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //    OnJump();
    
        }

        public void OnJump()
        {
            bool check = UtilityFunction.CheckLineData(_groundData, transform);
            if (check)
                _rigidBody.AddForce(transform.up * _jumpPower, ForceMode.Impulse);
        }
    }
}

