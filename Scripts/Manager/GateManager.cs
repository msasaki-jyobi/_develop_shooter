using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace develop_shooter
{

    public class GateManager : SingletonMonoBehaviour<GateManager>
    {
        [SerializeField] private TextMeshProUGUI _gateTextGUI;

        private int _currentGate;
        private int _maxGate;
        public UnityEvent ClearEvent;
        public List<StaticObjectInfo> StaticObjects = new List<StaticObjectInfo>();

        private void Start()
        {
            foreach (StaticObjectInfo info in StaticObjects)
            {
                info.GateObject.transform.position = info.GatePosition;
                info.GateObject.transform.rotation = Quaternion.Euler(info.GateRotation);
                info.GateObject.transform.localScale = info.GateScale;
                info.GateObject.isStatic = true;
            }
        }


        public void AddGate()
        {
            _maxGate++;
            UpdateGateGUI();
        }

        public void PlayGate()
        {
            _currentGate++;
            UpdateGateGUI();

            if (_currentGate == _maxGate)
            {
                // ÉNÉäÉAèàóù
                ClearEvent.Invoke();
            }
        }

        private void UpdateGateGUI()
        {
            if (_gateTextGUI != null)
                _gateTextGUI.text = $"Gate: {_currentGate} / {_maxGate}";
        }
    }
}
