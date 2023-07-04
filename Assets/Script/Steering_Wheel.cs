using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Steering_Wheel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool Weilding_Wheel;
    private float Max_Wheel_angle = 200f,Wheel_angle, Final_Wheel_angle , Rotation_Speed = 300f;
    private Vector2 Wheel_Position;
    public RectTransform Steering;
    public static float output_Steering;
    public Transform Car;


    void Update()
    {
        if (!Weilding_Wheel && Wheel_angle != 0f) {
            float DeltaAngle = Rotation_Speed * Time.deltaTime;   //Time.deltatime obligatory
            if (Mathf.Abs(DeltaAngle) > Mathf.Abs(Wheel_angle))
                Wheel_angle = 0f;
            else if (Wheel_angle > 0f)
                Wheel_angle -= DeltaAngle;
            else if (Wheel_angle < 0f)
                Wheel_angle += DeltaAngle;
        }
        Steering.localEulerAngles = new Vector3(0, 0, -Wheel_angle);
        output_Steering = (Wheel_angle / Max_Wheel_angle);
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        float angle_turned = Vector2.Angle(Vector2.up,eventData.position-Wheel_Position);
        if ((eventData.position - Wheel_Position).sqrMagnitude >= 400)
        {
            if (eventData.position.x > Wheel_Position.x)
            {
                Wheel_angle += angle_turned - Final_Wheel_angle;
            }
            else {
                Wheel_angle -= angle_turned - Final_Wheel_angle;
            }
            Wheel_angle = Mathf.Clamp(Wheel_angle, -Max_Wheel_angle, Max_Wheel_angle);
            Final_Wheel_angle = angle_turned;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Weilding_Wheel = true;
        Wheel_Position = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, Steering.position);
        Final_Wheel_angle = Vector2.Angle(Vector2.up, eventData.position - Wheel_Position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnDrag(eventData);
        Weilding_Wheel = false;
    }
}
