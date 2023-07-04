using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBeingPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public bool PointerDown;
    public static float Accelerator_Output;


    // Update is called once per frame
    void Update()
    {
        if (PointerDown == true || Input.GetButton("Fire2"))
        {
            Accelerator_Output = 1;
        }
        else
        {
            Accelerator_Output = 0;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerDown = false;
    }
}
