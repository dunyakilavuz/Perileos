using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet : MonoBehaviour 
{
	Atmosphere atmosphere;


	Vector2 gravityVector;
	[SerializeField]
	public float gravityMagnitude;
//	float gravityConstant = 6.6740831f * Mathf.Pow (10, -11);
	float gravityConstant = 6.6740831f * Mathf.Pow (10, -3);
	public List<Rigidbody> gravityAffectedObjects = new List<Rigidbody> ();

	[SerializeField]
	float mass;
	public float radius;

	[SerializeField]
	GameObject moonOrbit;
	float orbitalPeriod = 15;


	void Start () 
	{
		radius = transform.localScale.x;
		if (GameObject.Find ("loadedShip") != null) 
		{
			gravityAffectedObjects.Add ( GameObject.Find ("loadedShip").GetComponent<Rigidbody> ());
		}

	}

	void Update()
	{
		if (moonOrbit != null) 
		{
			moonOrbit.transform.RotateAround (moonOrbit.transform.position, Vector3.forward, 1 / orbitalPeriod * Time.deltaTime);
		}
	}
		

	void FixedUpdate () 
	{
		applyGravity ();

	}

	void applyGravity()
	{
		if (gravityAffectedObjects.Count != 0 || gravityAffectedObjects != null)
		{
			for (int i = 0; i < gravityAffectedObjects.Count; i++)
			{
				gravityVector = transform.position - gravityAffectedObjects [i].transform.position;
				gravityMagnitude = (gravityAffectedObjects [i].mass * mass * gravityConstant) / Mathf.Pow (gravityVector.magnitude, 2);
				gravityAffectedObjects[i].AddForce(gravityMagnitude * gravityVector.normalized);
			}
		}
	}

}
