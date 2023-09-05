using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class AIController : MonoBehaviour
    {
        public AiMovementController Movement { get { return _movement; }set { _movement = value; } }

        [SerializeField] private AiMovementController _movement;
        [SerializeField] private bool _moveAgent=false;
        [SerializeField] Transform _target;
        [SerializeField] float _companionStoppingDistance;
        private void Update()
        {
            if (_moveAgent) {
                Movement.Move(_target, _companionStoppingDistance);
                _moveAgent = false;
            }
        }
    }
}
