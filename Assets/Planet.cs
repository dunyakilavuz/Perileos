using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour 
{
	Atmosphere atmosphere;

	Vector2 gravityVector;
	[SerializeField]
	float gravityMagnitude;
//	float gravityConstant = 6.6740831f * Mathf.Pow (10, -11);
	float gravityConstant = 6.6740831f * Mathf.Pow (10, -7);
	public Rigidbody[] gravityAffectedObjects;

	[SerializeField]
	float mass = Mathf.Pow(2,2);
	float radius;

	void Start () 
	{
		radius = transform.localScale.x;
	}

	void FixedUpdate () 
	{
		applyGravity ();
	}

	void applyGravity()
	{
		for (int i = 0; i < gravityAffectedObjects.Length; i++)
		{
			gravityVector = transform.position - gravityAffectedObjects [i].transform.position;
			gravityMagnitude = (gravityAffectedObjects [i].mass * mass * gravityConstant) / Mathf.Pow (gravityVector.magnitude - radius / 2, 2);
			gravityAffectedObjects[i].AddForce(gravityMagnitude * gravityVector.normalized);
		}
	}

}
