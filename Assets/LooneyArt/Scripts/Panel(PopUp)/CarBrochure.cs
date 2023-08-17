using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LooneyDog
{
    public class CarBrochure : MonoBehaviour
    {
        public Button BuyButton { get { return _buyButton; }set { _buyButton = value; } }
        public Button ActivateSkinButton { get { return _activateSkinButton; } set { _activateSkinButton = value; } }
        public Image SkinActivatedImage { get { return _skinActivatedImage; } set { _skinActivatedImage = value; } }
        public VehicleID BrochureId { get { return _brochureId; }set { _brochureId = value; } }

        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _activateSkinButton;
        [SerializeField] private Image _skinActivatedImage;
        [SerializeField] private VehicleID _brochureId;


        private void Awake()
        {
            _buyButton.onClick.AddListener(OnClickBuy);
            _activateSkinButton.onClick.AddListener(OnClickApplySkin);
        }
        private void OnEnable()
        {
            SetButtonStatusWRTVehicle();
        }

        private void OnClickBuy() {
            GameManager.Game.Skin.Vehicles[(int)_brochureId].Unlocked = true;
            SetButtonStatusWRTVehicle();
        }

        private void OnClickApplySkin() {
            GameManager.Game.Skin.SetVehicleSkin(_brochureId);
            SetButtonStatusWRTVehicle();
        }

        public void SetButtonStatusWRTVehicle() {
            if (GameManager.Game.Skin.CurrentCarSkin == _brochureId)
            {
                _buyButton.gameObject.SetActive(false);
                _skinActivatedImage.gameObject.SetActive(true);
                _activateSkinButton.gameObject.SetActive(false);

            }
            else if (GameManager.Game.Skin.Vehicles[(int)_brochureId].Unlocked)
            {
                _buyButton.gameObject.SetActive(false);
                _skinActivatedImage.gameObject.SetActive(false);
                _activateSkinButton.gameObject.SetActive(true);
            }
            else
            {
                _buyButton.gameObject.SetActive(true);
                _skinActivatedImage.gameObject.SetActive(false);
                _activateSkinButton.gameObject.SetActive(false);
            }
        }
    }
}
