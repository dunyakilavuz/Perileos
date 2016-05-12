using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour 
{
	bool isGivingQuest;
	bool isReturningQuest;

	Ray ray;
	RaycastHit rayCastHit;

	List<Quest> questsOffered = new List<Quest>();

	void Start () 
	{
		//Quest quest1 = new Quest ();

	}

	void Update () 
	{

	}

	void OnMouseDown()
	{
		Debug.Log ("Clicked");
		if (isGivingQuest == true) 
		{

		} 
		else if (isReturningQuest == true)
		{

		}
	}
}
