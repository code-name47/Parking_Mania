using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear_Controller : MonoBehaviour
{
    public GameObject ForwardGear;
    public GameObject ReverseGear;
    public static float Gear_Type;

    private void Start()
    {
        Gear_Type = 1;
    }
    public void OnClick_Gear() {
        Gear_Type = Gear_Type * -1;
        if (Gear_Type == 1)
        {
            ForwardGear.SetActive(true);
            ReverseGear.SetActive(false);
        }
        else {
            ForwardGear.SetActive(false);
            ReverseGear.SetActive(true);
        }
    }
}
