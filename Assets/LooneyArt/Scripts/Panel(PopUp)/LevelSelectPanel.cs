using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace LooneyDog {
    public class LevelSelectPanel : MonoBehaviour
    {
        public Button[] LevelButtons { get { return _levelButtons; }set { _levelButtons = value; } }
        public TMP_Dropdown DifficultySelector { get { return _difficultytSelector; }set { _difficultytSelector = value; } }

        [SerializeField] private Button _backButton;

        [SerializeField] private float _transitionSpeed;

        [SerializeField] private Button[] _levelButtons;

        [SerializeField] private TMP_Dropdown _difficultytSelector;

        private void Awake()
        {
            _backButton.onClick.AddListener(OnClickBack);
            for (int i = 0; i < _levelButtons.Length; i++) {
                _levelButtons[i].onClick.AddListener(() => {
                    OnClickLevelButton();
                });
            }
            
        }

        private void OnDisable()
        {
            for (int i = 0; i < _levelButtons.Length; i++)
            {
                _levelButtons[i].GetComponent<LevelButton>().DisableActiveStars();
            }
        }

        private void OnClickLevelButton() {
            GameManager.Game.Screen.LoadFadeScreen(GameManager.Game.Screen.Home.gameObject, GameManager.Game.Screen.Load.gameObject);
            GameManager.Game.Screen.Load.SetSceneIndexAndDifficulty(EventSystem.current.currentSelectedGameObject.GetComponent<LevelButton>().ButtonId, (GameDifficulty)_difficultytSelector.value);
            GameManager.Game.Screen.ClosePopUpScreen(transform, ScreenLocation.Pop, _transitionSpeed);
            //GameManager.Game.Level.LoadLevel(GameManager.Game.Screen.Home.gameObject, GameManager.Game.Level)
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
                _levelButtons[i].GetComponent<LevelButton>().SetActiveStars(GameManager.Game.Level.GetStarForLevel(i));
            }
        }
    }
}
