using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace LooneyDog
{

    public class AiMovementController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        private bool _isMoving;
        private Transform _target;
        [SerializeField] private float _rotationSpeed, _distanceFromTarget;
        [SerializeField] private Animator _ani;
        private void Start()
        {
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;   
        }

        private void Update()
        {
            _ani.SetBool("IsMoving", _isMoving);
            if (_isMoving) {
                _agent.SetDestination(_target.position);
                LookRotation();
                if (Vector2.Distance(_target.position, this.transform.position) < _distanceFromTarget) {
                    _isMoving = false;
                }
            }
        }
        public void Move(Transform target, float _stoppingDistance)
        {
            _isMoving = true;
            _target = target;
            _agent.stoppingDistance = _stoppingDistance;
            _distanceFromTarget = _stoppingDistance;
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
    }
}