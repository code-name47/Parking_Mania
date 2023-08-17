using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LooneyDog
{

    public class PurchasePanelCar : MonoBehaviour
    {
        public CarBrochure[] CarBrochures { get { return _carBrochures; }set { _carBrochures=value; } }
        [SerializeField] private CarBrochure[] _carBrochures;
        [SerializeField] private Button _backButton;
        [SerializeField] private float _transitionSpeed;

        private void Awake()
        {
            _backButton.onClick.AddListener(OnClickBackButton);
        }

        public void OnClickBackButton() {
            GameManager.Game.Screen.DeactivateAllButtons(gameObject, _transitionSpeed);
            GameManager.Game.Screen.DeactivateAllButtons(GameManager.Game.Screen.Home.Storepanel.gameObject, _transitionSpeed);
            GameManager.Game.Screen.ClosePopUpScreen(transform, ScreenLocation.down, _transitionSpeed);
        }

        public void SetCarBrochure(VehicleID vehicleID) {
            for (int i = 0; i < _carBrochures.Length; i++) {
                if (i != (int)vehicleID)
                {
                    _carBrochures[i].gameObject.SetActive(false);
                }
                else
                { 
                    _carBrochures[i].gameObject.SetActive(true);
                }
            }
        }
    }
}