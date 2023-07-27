using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LooneyDog
{
    public class HomeScreen : MonoBehaviour
    {

        public Button StartButton { get { return _startButton; } set { _startButton = value; } }
        public Button Option { get { return _optionButton; } set { _optionButton = value; } }
        public Button ExitButton { get { return _exitButton; } set { _exitButton = value; } }

        public LevelSelectPanel LevelSelectpanel { get { return _levelSelectPanel; } set { _levelSelectPanel = value; } }

        public HomeOptionPanel HomeOptionpanel { get { return _homeOptionPanel; }set { _homeOptionPanel = value; } }

        [SerializeField] private Button _startButton;
        [SerializeField] private Button _optionButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private LevelSelectPanel _levelSelectPanel;
        [SerializeField] private HomeOptionPanel _homeOptionPanel;
        [SerializeField] private float _transitionSpeed;

        private void Awake()
        {
            _startButton.onClick.AddListener(OnClickStart);
            _optionButton.onClick.AddListener(OnClickOptionButton);
            _exitButton.onClick.AddListener(OnClickExitButton);
        }

        private void OnClickStart()
        {
            GameManager.Game.Screen.OpenPopUpScreen(_levelSelectPanel.transform, ScreenLocation.down, _transitionSpeed);
        }

        private void OnClickOptionButton()
        {
            GameManager.Game.Screen.OpenPopUpScreen(_homeOptionPanel.transform, ScreenLocation.left, _transitionSpeed);
        }

        private void OnClickExitButton()
        {

        }
       
    }
}