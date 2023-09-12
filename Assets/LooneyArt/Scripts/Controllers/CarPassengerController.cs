using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class CarPassengerController : MonoBehaviour
    {
        public bool PassengerOnBoard { get { return _passengerOnBoard; }set { _passengerOnBoard = value; } }
        public AIController PassengerAi { get { return _passengerAi; }set { _passengerAi = value; } }
        public Car_Controller CarController { get { return _carController; }set { _carController = value; } }

        [SerializeField] bool _passengerOnBoard;
        [SerializeField] AIController _passengerAi;
        [SerializeField] Car_Controller _carController;

        public void BoardPassenger(AIController passenger) {
            _carController.CarAniController.OpenCarDoorCar();
            _passengerOnBoard = true;
            _passengerAi = passenger;
        }

        public void DropPassenger() {
            if (_passengerOnBoard)
            {
                _carController.CarAniController.OpenCarDoorCar();
                _passengerAi.ActionExitCar();
                _passengerOnBoard = false;
                _passengerAi = null;
            } 
        }
    }
}
