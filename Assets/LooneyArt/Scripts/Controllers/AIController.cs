using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Rendering.Universal;
namespace LooneyDog
{

    public class AIController : MonoBehaviour
    {
        public AiMovementController Movement { get { return _movement; }set { _movement = value; } }
        public AiTaskID AiTask { get { return _aiTask; }set { _aiTask = value; }  }

        public CoverDetectorController CoverDetector { get { return _coverDetector; }set { _coverDetector = value; } }

        [SerializeField] private AiMovementController _movement;
        [SerializeField] CoverDetectorController _coverDetector;
        [SerializeField] private bool _moveAgent=false,_scanObstacle;
        [SerializeField] Transform _target;
        [SerializeField] float _companionStoppingDistance,_hidingDistance;
        [SerializeField] private AiTaskID _aiTask;
 
        
        private void Update()
        {
            if (_moveAgent) {
                Movement.Move(_target, _companionStoppingDistance);
                _moveAgent = false;
            }
            if (_scanObstacle)
            {
                _coverDetector.DetectCovers();
                _scanObstacle = false;
            }
        }
        public void ActionExitCar() {
            _movement.GetOutCar();
        }

        public void ActionEnterCar(Transform PassengerEnterPositionPlaceHolder) {
            Car_Controller Car=null;
            try
            {
                Car = PassengerEnterPositionPlaceHolder.gameObject.GetComponentInParent<Car_Controller>();  
            }
            catch
            {
                Debug.Log("no Car_controller Script Found on Player, please add Car_controller Script on Door");
            }
            if (Car != null)
            {
                if (Car != null)
                {
                    _aiTask = AiTaskID.idle;
                    Car.PassengerController.BoardPassenger(this);
                    _movement.GetInCar(PassengerEnterPositionPlaceHolder.gameObject.transform);
                }
            }
        }

        public void ActionHide(List<Vector2> hideablePoints)
        {
            Vector2 FarthestHidingSpot = FindFathersetHidingSpot(hideablePoints);
            
            if (Vector2.Distance(FarthestHidingSpot, transform.position) < _hidingDistance) { 
                MoveToLocation(FarthestHidingSpot);
            }
        }

        private Vector2 FindFathersetHidingSpot(List<Vector2> hideablePoints) {
            Vector2 FarthestHidingSpot = hideablePoints[0];
            if (hideablePoints.Count > 0) {
                for (int i = 1; i < hideablePoints.Count; i++) {
                    if (Vector2.Distance(FarthestHidingSpot, transform.position) < _hidingDistance && Vector2.Distance(hideablePoints[i], transform.position) < _hidingDistance)
                    {
                        if (Vector2.Distance(FarthestHidingSpot, _target.position) < Vector2.Distance(hideablePoints[i], _target.position))
                        {
                            FarthestHidingSpot = hideablePoints[i];
                        }
                    }
                }
                Debug.Log("hiding spot selected = " + FarthestHidingSpot);
                return FarthestHidingSpot;
            }
            else {
                Debug.Log("hiding spot default : "+ FarthestHidingSpot);
                return FarthestHidingSpot;
            }
        }

        private void ChangeHidingSpot() {
            _scanObstacle = true;
        }

        private void MoveToLocation(Vector3 Location) {
            _movement.Move(Location, _companionStoppingDistance);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (_aiTask == AiTaskID.GetInCar)
            {
                if (collision.gameObject.transform.CompareTag("Door"))
                {
                    ActionEnterCar(collision.gameObject.transform);
                }
            }
        }
    }
}
