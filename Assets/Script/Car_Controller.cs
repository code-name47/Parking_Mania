using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Car_Controller : MonoBehaviour
{


	Rigidbody2D rb;

	[SerializeField]
	float accelerationPower = 5f;
	[SerializeField]
	float steeringPower = 5f;
	float steeringAmount, speed, direction;
	public GameObject Game_Win, Game_Over;
	public AudioSource Car_Sound;

//------------------------------  Initialisations   -----------------------------------
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		Game_Win.SetActive(false);
		Game_Over.SetActive(false);
		Parking_dot_Front.Parking_Front = false;
		Parking_Spot_back.Parking_Back = false;


	}

//----------------------------------------------------------------------
	void FixedUpdate()
	{
		
		//----------------------------------------  Game Win Parking Detection   ----------
		if (Parking_dot_Front.Parking_Front == true && Parking_Spot_back.Parking_Back == true) {
			Game_Win.SetActive(true);
			rb.velocity = Vector2.zero;
		}
		steeringAmount = -Steering_Wheel.output_Steering;
		steeringAmount = steeringAmount / 2;
		//-------------------------------------  Car Volumne Control Function  -----------
		carvol();
		//------------------------------------------- Car Move Code  ---------------------
		speed = ButtonBeingPressed.Accelerator_Output * accelerationPower * Gear_Controller.Gear_Type;//Accelerator_Output = 1 or 0  Gear_Controller = 1 or -1 
		direction = Mathf.Sign(Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up)));
		rb.rotation += steeringAmount * steeringPower * rb.velocity.magnitude * direction;

		rb.AddRelativeForce(Vector2.up * speed);

		rb.AddRelativeForce(-Vector2.right * rb.velocity.magnitude * steeringAmount / 2);
		//------------------------------------  Increase in Linear Drag When Braking  --------------------
		if (OnBreak.Break_Applied == true)
		{
			rb.drag = 20;
		}
		else
		{
			rb.drag = 4f;
		}
	}
//-------------------------------- Engine Sound ----------------------------------
	void carvol(){
		if (ButtonBeingPressed.Accelerator_Output == 1)
		{
			Car_Sound.volume = (Car_Sound.volume + 0.1f);
		}
		else
			Car_Sound.volume = (Car_Sound.volume - 0.01f);
	}

//----------------------------- Game Over Collison Detection -------------------------------
	private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag != "Parking_Spot") {
			Game_Over.SetActive(true);
		}
    }
//---------------------------------   End   ----------------------------------------------
}