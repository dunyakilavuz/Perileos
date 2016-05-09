using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour 
{
	public float verticalScaleOfShip;
	public float fuel = 10;
	public float mass = 1;
	public float thrustCapacity;
	public float throttle;
	float rotateSpeed = 1;

	void Start ()
	{
		GetComponent<Rigidbody> ().mass = mass;
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
		GetComponent<Rigidbody> ().AddForce(transform.forward * throttle);

		if (fuel > 0) 
		{
			if (Input.GetKey (KeyCode.LeftShift)/* && throttle < 1*/) 
			{
				throttle += 0.1f;
			}
			if (Input.GetKey (KeyCode.LeftControl) && throttle > 0)
			{
				throttle -= 0.1f;
			}
			if (Input.GetKey (KeyCode.Z)) 
			{
				throttle = 1;
			}
			if (Input.GetKey (KeyCode.X)) 
			{
				throttle = 0;
			}
			/*
			if(throttle > 0)
			{
				MainFlame.SetActive(true);
				UpperFlame.SetActive(false);
				fuel = fuel - throttle * 0.003f;
			}
			else
			{
				MainFlame.SetActive(false);
				UpperFlame.SetActive(false);
			}
			*/
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
