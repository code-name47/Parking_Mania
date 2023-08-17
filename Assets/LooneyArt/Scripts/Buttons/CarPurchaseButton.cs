using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LooneyDog
{

    public class CarPurchaseButton : MonoBehaviour
    {
        public int ButtonId { get { return _buttonId; }set { _buttonId = value; } }
        [SerializeField] private int _buttonId; 
    }
}
