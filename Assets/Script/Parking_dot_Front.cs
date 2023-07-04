using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parking_dot_Front : MonoBehaviour
{
    public static bool Parking_Front;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Parking_Spot")
        {
            Parking_Front = true;
        }
        else {
            Parking_Front = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Parking_Spot")
        {
            Parking_Front = false;
        }
    }
}
