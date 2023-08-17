using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LooneyDog
{

    public class SkinManager : MonoBehaviour
    {
        public VehicleData[] Vehicles { get { return _vehicles; }set { _vehicles = value; } }
        public VehicleID CurrentCarSkin { get { return _currentCarSkin; }set { _currentCarSkin = value; } }
        [SerializeField] private VehicleData[] _vehicles;
        [SerializeField] private VehicleID _currentCarSkin;

        public void ApplySkin(Car_Controller currentCar, SpriteRenderer CarSkin) {
            CarSkin.sprite = _vehicles[(int)_currentCarSkin].CarSkin;
            currentCar.AccelerationPower = _vehicles[(int)_currentCarSkin].Accelaration;
            currentCar.SteeringPower = _vehicles[(int)_currentCarSkin].Handling;
        }

        public void SetVehicleSkin(VehicleID _selectedCarSkin) {
            _currentCarSkin = _selectedCarSkin;
            GameManager.Game.Data.player.SetCurrentSkin(_currentCarSkin);
        }

        public void GetCurrentVehicleSkin()
        {
            _currentCarSkin = GameManager.Game.Data.player.CurrentActiveSkin;
        }
    }
}
