using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LooneyDog
{
    public class StopWatch : MonoBehaviour
    {
        public TextMeshProUGUI TimerText { get { return _timerText; }set { _timerText = value; } }
        public float TimeValue { get { return _timeValue; }set { _timeValue = value; } }
        public bool TimerStarted { get { return _timerStarted; }set { _timerStarted = value; } }

        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private float _timeValue;
        [SerializeField] private bool _timerStarted;
        [SerializeField] private float _wobbleDuration;
        [SerializeField] private int _wobbleStrength,_shakes;
        //[]
        private void Start()
        {
           
        }

        private void FixedUpdate()
        {
            if (_timerStarted) {
                _timeValue += Time.deltaTime;
                _timerText.text = "" + (int)_timeValue;
            }
        }
        public void StartTimer() { 
            _timerStarted = true;
            GameManager.Game.Anime.WobbleAnimation(transform, _wobbleDuration, _wobbleStrength, _shakes);
        }

        public void PauseTimer() { _timerStarted = false; }

        public void StopTimer() { PauseTimer(); ResetTimer(); }

        public void ResetTimer() { _timeValue = 0; }

        public int GetElaspedTime()
        {
            return (int)_timeValue;
        }


    }
}
