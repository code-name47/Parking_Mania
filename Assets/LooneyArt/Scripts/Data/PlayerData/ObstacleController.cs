using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LooneyDog
{
    public class ObstacleController : MonoBehaviour
    {
        public float DamageAfterCollision { get { return _damageAfterCollision; }set { _damageAfterCollision = value; } } 

        [SerializeField] private float _damageAfterCollision;
    }
}
