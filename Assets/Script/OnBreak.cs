using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnBreak : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public bool PointerDown;

    public static bool Break_Applied;

    void Start() {
        Break_Applied = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (PointerDown == true)
        {
            Break_Applied = true;
        }
        else
        {
            Break_Applied = false;
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
