using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace develop_shooter
{

    public class HealthSlider : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private PlayerHealth _playerHealth;

        private void Start()
        {
            _playerHealth.UpdateHealthEvent += OnUpdateHealthHandle;
        }


        private void OnUpdateHealthHandle(int current, int max)
        {
            _healthSlider.value = (float)current / max;
        }
    }

}