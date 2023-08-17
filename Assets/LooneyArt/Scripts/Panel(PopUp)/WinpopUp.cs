using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LooneyDog
{

    public class WinpopUp : MonoBehaviour
    {
        
        public GameObject[] Stars { get { return _stars; }set { _stars = value; } }
        [SerializeField] GameObject[] _stars;
        [SerializeField] float _transitionSpeed;

        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _nextButton;

        private void Awake()
        {
            _nextButton.onClick.AddListener(OnClickNextButton);
            _menuButton.onClick.AddListener(OnClickMenuButton);
            _retryButton.onClick.AddListener(OnClickRetryButton);
        }

        private void OnDisable()
        {
            DisableStars();
        }

        private void OnClickMenuButton() {
            GameManager.Game.Level.LoadLevel(GameManager.Game.Screen.GameScreen.gameObject, 0);
            GameManager.Game.Screen.ClosePopUpScreen(transform, ScreenLocation.Pop, _transitionSpeed);
        }

        private void OnClickRetryButton() {
            GameManager.Game.Level.LoadLevel(GameManager.Game.Screen.GameScreen.gameObject, GameManager.Game.Level.LevelNumber);
            GameManager.Game.Screen.ClosePopUpScreen(transform, ScreenLocation.Pop, _transitionSpeed);
        }

        private void OnClickNextButton() {
            GameManager.Game.Level.LoadLevel(GameManager.Game.Screen.GameScreen.gameObject,
            GameManager.Game.Level.LevelNumber + 1);

            GameManager.Game.Screen.ClosePopUpScreen(transform, ScreenLocation.Pop, _transitionSpeed);
        }
        public void ActivateStars(int starsObtained)
        {
            DisableStars();
            for (int i = 0; i < _stars.Length; i++) {
                if (i <= starsObtained) {
                    _stars[i].SetActive(true);
                    GameManager.Game.Anime.SmashFromScreen(_stars[i].transform, _transitionSpeed);
                }
            }
        }

        public void DisableStars() {
            for(int i = 0; i < _stars.Length; i++) {
               _stars[i].SetActive(false);
            }
        }
    }
}