using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace LooneyDog
{
	public class Car_Controller : MonoBehaviour
	{
		public SpriteRenderer CarSprite { get { return _carSprite; } set { _carSprite = value; } }
		public float AccelerationPower { get { return accelerationPower; } set { accelerationPower = value; } }
		public float SteeringPower { get { return steeringPower; } set { steeringPower = value; } }
		public GameObject CarPrefab { get { return _carPrefab; } set { _carPrefab = value; } }
		public HealthControler Healthcontroller { get { return _healthcontroller; }set{ _healthcontroller = value; } }

		public CarPassengerController PassengerController { get { return _passengerController; }set { _passengerController = value; } }
		public CarAnimatorController CarAniController { get { return _carAnimatorController; }set { _carAnimatorController = value; } }

		[SerializeField]  Rigidbody2D rb;

		[SerializeField]
		float accelerationPower = 5f;
		[SerializeField]
		float steeringPower = 5f;
		float steeringAmount, speed, direction;
		public AudioSource Car_Sound;

		[SerializeField] bool _gameCompleted;


		[SerializeField] private SpriteRenderer _carSprite;
		[SerializeField] private HealthControler _healthcontroller;
		[SerializeField] private float _defaultDamage,_carFlashTime,_knockBack;
		[SerializeField] private Transform _nextObjective;
		[SerializeField] private CameraController _cameraController;
		[SerializeField] private GameObject _carPrefab;
		[SerializeField] private CarPassengerController _passengerController;
		[SerializeField] private CarAnimatorController _carAnimatorController;


		

		//------------------------------  Initialisations   -----------------------------------
		void Start()
		{

			Parking_dot_Front.Parking_Front = false;
			Parking_Spot_back.Parking_Back = false;
			GameManager.Game.Screen.GameScreen.SetCarControllerToUi(transform,_nextObjective);
			GameManager.Game.Skin.ApplyCarPrefab(this);
			_cameraController.SetCameraFollowObject(transform);
		}

		//----------------------------------------------------------------------
		void FixedUpdate()
		{

            //----------------------------------------  Game Win Parking Detection   ----------
            /*if (Parking_dot_Front.Parking_Front == true && Parking_Spot_back.Parking_Back == true)
            {
                //Game_Win.SetActive(true);
                rb.velocity = Vector2.zero;
                if (!_gameCompleted)
                {
                    _gameCompleted = true;
                    GameManager.Game.Level.GameCompleted(true);
                }
            }*/
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

		//-------------------------------- Car Stop --------------------------------------
		public void StopCar(Vector2 collisionPoint) {
			rb.velocity = Vector2.zero;
			rb.angularVelocity = 0;
			OnCollisionPush(collisionPoint,_knockBack);
		}

		//-------------------------------- Engine Sound ----------------------------------
		void carvol()
		{
			if (ButtonBeingPressed.Accelerator_Output == 1)
			{
				Car_Sound.volume = (Car_Sound.volume + 0.1f);
			}
			else
				Car_Sound.volume = (Car_Sound.volume - 0.01f);
		}
		//----------------------------- Calculate BackWardPush    ----------------------------------
		private void OnCollisionPush(Vector3 collisionObject, float knockBackForce) {
			var explosionDir = collisionObject - transform.position;
			var explosionDistance = explosionDir.magnitude;
			float upwardsModifier = 0;
			// Normalize without computing magnitude again
			if (upwardsModifier == 0)
				explosionDir /= explosionDistance;
			else
			{
				// From Rigidbody.AddExplosionForce doc:
				// If you pass a non-zero value for the upwardsModifier parameter, the direction
				// will be modified by subtracting that value from the Y component of the centre point.
				explosionDir.y += upwardsModifier;
				explosionDir.Normalize();
			}
			rb.AddForce((-1)* knockBackForce * explosionDir, ForceMode2D.Impulse);
			//rb.AddForce(Mathf.Lerp(0, 50, (1 - explosionDistance)) * explosionDir,ForceMode2D.Impulse);
		}
	

		
		//----------------------------- Game Over Collison Detection -------------------------------
		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (!collision.gameObject.CompareTag("Parking_Spot"))
			{
				/*if (!_gameCompleted)
				{
					//Game_Over.SetActive(true);
					_gameCompleted = true;
					GameManager.Game.Level.GameCompleted(false);
				}*/
			}

			if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("ObstacleAndCover")) {

				StopCar(collision.GetContact(0).point);
				GameManager.Game.Anime.Flash(CarSprite, _carFlashTime);
				try
				{
					_healthcontroller.DamageHealth(collision.gameObject.GetComponent<ObstacleController>().DamageAfterCollision);
				}
				catch {
					_healthcontroller.DamageHealth(_defaultDamage);
				}
			}
		}
		//---------------------------------   End   ----------------------------------------------
	}
}