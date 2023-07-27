using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LooneyDog
{
    public class HomeOptionPanel : MonoBehaviour
    {
        public Button BackButton { get { return _backButton; }set { _backButton = value; } }

        [SerializeField] private Button _backButton;
        [SerializeField] private float _transitionSpeed;


        private void Awake()
        {
            _backButton.onClick.AddListener(OnClickBackButton);
        }

        private void OnClickBackButton() {
            GameManager.Game.Screen.ClosePopUpScreen(transform, ScreenLocation.left, _transitionSpeed,_backButton);        
        }
    }
}