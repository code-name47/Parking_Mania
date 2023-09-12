using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class CarDoorTrigger : MonoBehaviour
    {
        public CarDoor CarDoorId { get { return _cardoorId; } set { _cardoorId = value; } }
        public CarAnimatorController CarAnicontroller { get { return _carAniController; }set { _carAniController = value; } }

        [SerializeField] private CarDoor _cardoorId;
        [SerializeField] private CarAnimatorController _carAniController;
        
    }
}
