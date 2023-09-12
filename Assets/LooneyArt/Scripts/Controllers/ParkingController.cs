using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class ParkingController : MonoBehaviour
    {
        [SerializeField]private ParkSpotController _parkSpotFront, _parkSpotRear;
        [SerializeField] int _placedSpotsInCorrectPosition=0;
        [SerializeField] private Car_Controller _parkedCar;

        public void Check() {
            if (_placedSpotsInCorrectPosition >= 2) {
                Debug.Log("Car Parked Successfully");
                _parkedCar.PassengerController.DropPassenger();
            }
        }

        public void AddSpot(Car_Controller parkedCar) {
            _parkedCar = parkedCar;
            _placedSpotsInCorrectPosition++;
            Check();
        }

        public void RemoveSpot() {
            _placedSpotsInCorrectPosition--;
            Check();
        }

    }
}
