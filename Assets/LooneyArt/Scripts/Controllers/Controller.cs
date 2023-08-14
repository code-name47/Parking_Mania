using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace LooneyDog
{
    public class Controller : MonoBehaviour
    {
        public Steering_Wheel SteeringWheel { get { return _steeringWheel; }set { _steeringWheel = value; } }
        public ButtonBeingPressed Accelerator { get { return _accelerator; } set { _accelerator = value; } }
        public OnBreak Break { get { return _break; } set { _break = value; } }
        public Gear_Controller Gear { get { return _gear; } set { _gear = value; } }


        [SerializeField] private Steering_Wheel _steeringWheel;
        [SerializeField] private ButtonBeingPressed _accelerator;
        [SerializeField] private OnBreak _break;
        [SerializeField] private Gear_Controller _gear;

    }
}
