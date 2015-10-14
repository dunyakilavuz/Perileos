using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleAssembly : MonoBehaviour 
{
	[SerializeField]
	ShipPart root;
	[SerializeField]
	ShipPart newPart;
	[SerializeField]
	//List<ShipPart> shipParts;
	float attachDistance = 1;
	Vector2 attachPoint;

	void Start () 
	{
		//shipParts.Add (root);
	}

	void Update () 
	{
		assembleShipParts ();
	}

	void assembleShipParts()
	{
		if ((root.attachmentPointDown - newPart.attachmentPointUp).magnitude < attachDistance) 
		{
			attachPoint = new Vector2(root.transform.position.x,root.attachmentPointDown.y - newPart.transform.localScale.y / 2);
			newPart.transform.position = attachPoint;
		}
	}
}
