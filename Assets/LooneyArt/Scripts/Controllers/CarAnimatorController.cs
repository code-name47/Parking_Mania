using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class CarAnimatorController : MonoBehaviour
    {
        public Animator CarAnimator { get { return _carAnimator; } set { _carAnimator = value; } }
        public Transform LeftDoorPosition { get { return _leftDoorPosition; }set { _leftDoorPosition = value; } }

        public SpriteRenderer CarBodySpriteRenderer { get { return _carBodySpriterender; }set { _carBodySpriterender = value; } }

        [SerializeField] private Animator _carAnimator;
        [SerializeField] private Transform _leftDoorPosition;
        [SerializeField] private SpriteRenderer _carBodySpriterender;

        public void OpenCarDoorCar() {
            _carAnimator.SetTrigger("LeftDoorOpen");
        }
        
    }
}
