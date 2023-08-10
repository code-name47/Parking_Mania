using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LooneyDog {
    public class LevelSelectPanel : MonoBehaviour
    {
        public LevelButton[] LevelButtons { get { return _levelButtons; }set { _levelButtons = value; } }

        [SerializeField] private Button _backButton;

        [SerializeField] private float _transitionSpeed;

        [SerializeField] private LevelButton[] _levelButtons;

        private void Awake()
        {
            _backButton.onClick.AddListener(OnClickBack);
        }

        private void OnClickBack() {
            GameManager.Game.Screen.ClosePopUpScreen(transform, ScreenLocation.Pop, _transitionSpeed,_backButton);
        }

        private void OnEnable()
        {
            StartCoroutine(waitForStarsEnabling());
        }

        IEnumerator waitForStarsEnabling() {
            yield return new WaitForSeconds(GameManager.Game.Screen.Home.TransitionSpeed);
            for (int i = 0; i < _levelButtons.Length; i++) {
                _levelButtons[i].SetActiveStars(GameManager.Game.Level.GetStarForLevel(i));
            }
        }
    }
}
