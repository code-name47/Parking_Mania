using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
namespace LooneyDog
{

    public class AiMovementController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        private bool _isMoving,_inCar,_sittingInCar,_isMovingTowardTransform=false;
        private Transform _target,_car;
        [SerializeField] private float _rotationSpeed, _moveToCarDoorSpeed, _lookAtCarDoorSpeed;
        private float _distanceFromTarget;
        [SerializeField] private Animator _ani;
        [SerializeField] private ShadowCaster2D _shadowCaster;
        private Vector3 moveLocation;
        private void Start()
        {
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _shadowCaster = GetComponentInChildren<ShadowCaster2D>();
        }

        private void Update()
        {
            _ani.SetBool("IsMoving", _isMoving);
            if (_isMovingTowardTransform)
            {
                moveLocation = _target.position;
            }
            if (_isMoving) {
                _agent.SetDestination(moveLocation);
                LookRotation();
                if (Vector2.Distance(moveLocation, this.transform.position) < _distanceFromTarget)
                {
                    StopMoving();
                }
            }

            if (_inCar) {
                MoveWithCar();
            }
        }
        public void Move(Transform target, float _stoppingDistance)
        {
            _isMovingTowardTransform = true;
            _agent.ResetPath();
            _isMoving = true;
            _target = target;
            _agent.stoppingDistance = _stoppingDistance;
            _distanceFromTarget = _stoppingDistance;
        }
        public void Move(Vector3 target, float _stoppingDistance)
        {
            _isMovingTowardTransform = false;
            _agent.ResetPath();
            _isMoving = true;
            moveLocation = target;
            _agent.stoppingDistance = _stoppingDistance;
            _distanceFromTarget = _stoppingDistance;
        }
        public void StopMoving() {
            _isMoving = false;
        }

        public void LookRotation() {
            Quaternion torotation;

            if (_agent.path.corners.Length > 1)
            {
                torotation = Quaternion.LookRotation(_agent.path.corners[1] - transform.position, Vector3.forward);
            }
            else
            {
                torotation = transform.rotation;
            }

            torotation = new Quaternion(0, 0, torotation.z, torotation.w);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, torotation, _rotationSpeed * Time.deltaTime);
        }


        public void GetInCar(Transform CarDoor) {
            _agent.isStopped = true;
            _agent.ResetPath();
            StopMoving();
            _car = CarDoor;
            _ani.SetTrigger("SitInCarLeft");
            _inCar = true;
            _shadowCaster.enabled = false;
        }

        public void GetOutCar()
        {
            _shadowCaster.enabled = true;
            unsetSittingInCarThroughAnimationEvent();
            _agent.isStopped = false;
            _inCar = false;
            _ani.SetTrigger("GetOutOfCar");
            _car = null;
        }

        public void MoveWithCar() {
            if (_sittingInCar) {
                transform.position = _car.position;
                transform.rotation = _car.rotation;
            }                        
            else
            {
                transform.position = Vector3.Lerp(transform.position, _car.position, _moveToCarDoorSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, _car.rotation, _lookAtCarDoorSpeed * Time.deltaTime);
            }
        }

        public void DisableCharacter() { // called from animation Event
            gameObject.SetActive(false);
        }

        public void setSittingInCarThroughAnimationEvent() {
            _sittingInCar = true;
        }

        public void unsetSittingInCarThroughAnimationEvent()
        {
            _sittingInCar = false;
        }
        public void EnableCharacter() {
            gameObject.SetActive(true);
        }
    }
}