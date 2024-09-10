using Cysharp.Threading.Tasks;
using develop_common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace develop_shooter
{
    public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
    {
        [SerializeField] private TextMeshProUGUI _scoreTextGUI;
        [SerializeField] private TextMeshProUGUI _messageTextGUI;
        [SerializeField] private string _scoreText = "Score:";
        [SerializeField] private string _clearMessage = "Game Clear!!";
        private int _currentScore;
        private int _maxScore;

        private async void Start()
        {
            await UniTask.Delay(10);
            UpdateScoreTextGUI();
        }

        public void AddTarget()
        {
            _maxScore++;
            UpdateScoreTextGUI();
        }

        public void AddScore()
        {
            _currentScore++;
            UpdateScoreTextGUI();
            if (_currentScore >= _maxScore)
            {
                if (TimerManager.Instance != null)
                    TimerManager.Instance.OnStopTimer(true);

                if (_messageTextGUI != null)
                    _messageTextGUI.text = _clearMessage;
            }
        }

        private void UpdateScoreTextGUI()
        {
            if (_scoreTextGUI != null)
                _scoreTextGUI.text = $"{_scoreText}{_currentScore}/{_maxScore}";
        }
    }
}