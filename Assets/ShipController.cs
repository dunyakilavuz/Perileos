using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour 
{
	public float verticalScaleOfShip;
	public float fuel = 50;
	public float mass = 1;
	public float thrustCapacity = 10;
	public float throttle = 0;
	float thrustAtMoment;
	float rotateSpeed = 1;
	GameObject[] engineFlames;
	public SphereCollider shipTrigger;
	[SerializeField]
	public Collider shipTouchingWith;

	void OnTriggerStay(Collider other)
	{
		shipTouchingWith = other;
	}

	void Start ()
	{
		GetComponent<Rigidbody> ().mass = mass;
		engineFlames = GameObject.FindGameObjectsWithTag ("rocketFlame");
	}

	void FixedUpdate ()
	{
		ThrottleSpaceCraft ();
	}

	void Update()
	{
		RotateSpaceCraft ();
	}

	void ThrottleSpaceCraft()
	{
		thrustAtMoment = thrustCapacity * throttle;

		GetComponent<Rigidbody> ().AddForce(transform.forward * thrustAtMoment);

		if (fuel > 0) 
		{
			if (Input.GetKey (KeyCode.LeftShift) && throttle < 1) 
			{
				throttle += 0.05f;
			}
			if (Input.GetKey (KeyCode.LeftControl) && throttle > 0)
			{
				throttle -= 0.05f;
			}
			if (throttle < 0) 
			{
				throttle = 0;
			}
			if (Input.GetKey (KeyCode.Z)) 
			{
				throttle = 1;
			}
			if (Input.GetKey (KeyCode.X)) 
			{
				throttle = 0;
			}

			if(throttle > 0)
			{
				for (int i = 0; i < engineFlames.Length; i++) 
				{
					if (engineFlames [i] != null) 
					{
						engineFlames [i].GetComponent<ParticleSystem> ().Play ();
					}
				}
				fuel = fuel - throttle * 0.003f;
			}
			else
			{
				for (int i = 0; i < engineFlames.Length; i++) 
				{
					if (engineFlames [i] != null) 
					{
						if (engineFlames [i] != null) 
						{
							engineFlames [i].GetComponent<ParticleSystem> ().Stop ();
						}
					}
				}
			}
		}
	}
	void RotateSpaceCraft()
	{
		if (Input.GetKey (KeyCode.A)) 
		{
			transform.Rotate(0,rotateSpeed,0);
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			transform.Rotate(0,-rotateSpeed,0);
		}
	}


}
