using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class ParkSpotController : MonoBehaviour
    {
        public bool ParkSpot { get { return _parkSpot; }set { _parkSpot = false; } }
        [SerializeField] private bool _parkSpot;
        [SerializeField] ParkingController _parkingController;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Car_Controller parkedCar = null;
            if (collision.gameObject.CompareTag("Parking_Spot"))
            {
                try
                {
                    parkedCar = collision.gameObject.GetComponentInParent<Car_Controller>();
                }
                catch {
                    parkedCar = null;
                }
                if (parkedCar != null) {
                    _parkingController.AddSpot(parkedCar);
                }
                
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Parking_Spot"))
            {
                _parkingController.RemoveSpot();
            }
        }
    }
}
