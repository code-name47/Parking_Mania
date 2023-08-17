using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace LooneyDog
{
    public class StorePanel : MonoBehaviour
    {
        public Button BackButton { get { return _backButton; }set { _backButton = value; } }
        public Button[] CarPurchaseButton { get { return _carPurchaseButtons; }set { _carPurchaseButtons = value; } }
        public PurchasePanelCar PurchasePanelcars { get { return _purchasePanelCar; }set { _purchasePanelCar = value; } }

        [SerializeField] private Button _backButton;
        [SerializeField] private float _transitionTime;
        [SerializeField] private Button[] _carPurchaseButtons;
        [SerializeField] private PurchasePanelCar _purchasePanelCar;
        [SerializeField] private float _transitionSpeed;

        private void Awake()
        {
            _backButton.onClick.AddListener(OnClickBackButton);
            for (int i = 0; i < _carPurchaseButtons.Length; i++)
            {
                _carPurchaseButtons[i].onClick.AddListener(() => {
                    OnClickCarPurchaseButton();
                });
            }
        }

        private void OnClickBackButton() {
            GameManager.Game.Screen.DeactivateAllButtons(gameObject, _transitionSpeed);
            GameManager.Game.Screen.DeactivateAllButtons(GameManager.Game.Screen.Home.gameObject, _transitionSpeed);
            GameManager.Game.Screen.ClosePopUpScreen(transform, ScreenLocation.Pop, _transitionTime);
        }

        private void OnClickCarPurchaseButton() {
            _purchasePanelCar.SetCarBrochure((VehicleID)EventSystem.current.currentSelectedGameObject.GetComponent<CarPurchaseButton>().ButtonId);
            GameManager.Game.Screen.OpenPopUpScreen(_purchasePanelCar.transform, ScreenLocation.down, _transitionSpeed);
            GameManager.Game.Screen.DeactivateAllButtons(gameObject, _transitionSpeed);
        }
    }
}
