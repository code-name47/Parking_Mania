using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LooneyDog {
    public class LevelSelectPanel : MonoBehaviour
    {
        [SerializeField] private Button _backButton;

        [SerializeField] private float _transitionSpeed;

        private void Awake()
        {
            _backButton.onClick.AddListener(OnClickBack);
        }

        private void OnClickBack() {
            GameManager.Game.Screen.ClosePopUpScreen(transform, ScreenLocation.Pop, _transitionSpeed,_backButton);
        }
    }
}
