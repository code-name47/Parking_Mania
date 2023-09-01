using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LooneyDog
{
    [CreateAssetMenu(fileName = "Vehicle",menuName ="ScriptableObjects/VehicleDataScriptableObject",order =2)]
    public class VehicleData : ScriptableObject
    {
        public VehicleID CarId { get { return _carId; }set { _carId = value; } }
        public int Accelaration { get { return _accelaration; }set { _accelaration = value; } }
        public int Handling { get { return _handling; }set { _handling = value; } }

        public float Health { get { return _health; }set { _health = value; } }

        public float Armor { get { return _armor; }set { _armor = value; } }

        public Sprite CarSkin { get { return _carSkin; }set { _carSkin = value; } }

        public bool Unlocked { get { return _unLocked; }set { _unLocked = value; } }
        public GameObject CarPrefab { get { return _carPrefab; }set { _carPrefab = value; } }

        [SerializeField] private VehicleID _carId;
        [SerializeField] private int _accelaration;
        [SerializeField] private int _handling;
        [SerializeField] private Sprite _carSkin;
        [SerializeField] private bool _unLocked;
        [SerializeField] private float _health;
        [SerializeField] private float _armor;
        [SerializeField] private GameObject _carPrefab;
         
    }
}
