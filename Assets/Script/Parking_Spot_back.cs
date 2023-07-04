using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parking_Spot_back : MonoBehaviour
{
    public static bool Parking_Back;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Parking_Spot")
        {
            Parking_Back = true;
        }
        else
        {
            Parking_Back = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Parking_Spot")
        {
            Parking_Back = false;
        }
    }
}
