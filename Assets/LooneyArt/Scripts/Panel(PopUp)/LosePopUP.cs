using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LooneyDog
{

    public class LosePopUP : MonoBehaviour
    {
        public Button RetryButton { get { return _retryButton; }set { _retryButton = value; } }
        public Button MenuButton { get { return _menuButton; }set { _menuButton = value; } }

        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private float _transitionSpeed;

        private void Awake()
        {
            _retryButton.onClick.AddListener(OnClickRetryButton);
            _menuButton.onClick.AddListener(OnClickMenuButton);
        }

        private void OnClickMenuButton()
        {
            GameManager.Game.Level.LoadLevel(GameManager.Game.Screen.GameScreen.gameObject, 0);
            GameManager.Game.Screen.ClosePopUpScreen(transform, ScreenLocation.Pop, _transitionSpeed);
        }

        private void OnClickRetryButton()
        {
            GameManager.Game.Level.LoadLevel(GameManager.Game.Screen.GameScreen.gameObject, GameManager.Game.Level.LevelNumber);
            GameManager.Game.Screen.ClosePopUpScreen(transform, ScreenLocation.Pop, _transitionSpeed);
        }
    }
}
