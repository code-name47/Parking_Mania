using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LooneyDog {
    public class GameScreen : MonoBehaviour
    {
        public Controller ControllerUi { get { return _controllerUi; }set { _controllerUi = value; } }
        public ResultPopUp ResultPanel { get { return _resultPanel; }set { _resultPanel = value; }  }
        public StopWatch StopWatchTimer { get { return _stopWatchTimer; }set { _stopWatchTimer = value; } }
        public HealthMeterControllerUI HealthMeter { get { return _healthMeter; }set { _healthMeter = value; } }
        public float TransitionSpeed { get { return _transitionSpeed; }set { _transitionSpeed = value; } }

        public MiniMapController MiniMapControl { get { return _minimapControl; }set { _minimapControl = value; } }

        [SerializeField] private Controller _controllerUi;
        [SerializeField] private ResultPopUp _resultPanel;
        [SerializeField] private float _transitionSpeed;
        [SerializeField] private StopWatch _stopWatchTimer;
        [SerializeField] private bool _startGameTimer;
        [SerializeField] private HealthMeterControllerUI _healthMeter;
        [SerializeField] private MiniMapController _minimapControl;


        private void OnEnable()
        {
            _resultPanel.gameObject.SetActive(false);
            _controllerUi.gameObject.SetActive(true);
            _startGameTimer=false;
        }

        private void OnDisable()
        {
            StopTimer();
        }

        public void SetCarControllerToUi(Transform CarController,Transform nextObjective) {
            _controllerUi.SteeringWheel.Car = CarController;
            _minimapControl.SetMiniMapDetails(CarController, nextObjective);
        }

        public void GameWin(int StarsObtained, int Reward) {
            StopTimer();
            _resultPanel.SetGameCompleteStatus(true, StarsObtained);
            GameManager.Game.Screen.OpenPopUpScreen(_resultPanel.transform, ScreenLocation.Pop, _transitionSpeed);
            
        }

        public void GameLose() {
            _resultPanel.SetGameCompleteStatus(false,0);
            GameManager.Game.Screen.OpenPopUpScreen(_resultPanel.transform, ScreenLocation.Pop, _transitionSpeed);
        }

        public void StartGameTimer() {
            if (!_startGameTimer)
            {
                _startGameTimer = true;
                GameManager.Game.Screen.GameScreen.StopWatchTimer.StartTimer();
            }
        }

        public void PauseTimer() {
            GameManager.Game.Screen.GameScreen.StopWatchTimer.PauseTimer();
            _startGameTimer = false;
        }
        public void StopTimer() { GameManager.Game.Screen.GameScreen.StopWatchTimer.StopTimer();
            _startGameTimer = false;
        }

        public void ResetTimer() { GameManager.Game.Screen.GameScreen.StopWatchTimer.ResetTimer();
            _startGameTimer = false;
        }

        public void ResetLevel() {
            StopTimer();
            _resultPanel.gameObject.SetActive(false);
            //_resultPanel.SetGameCompleteStatus(false,0);
        }
    }
}
