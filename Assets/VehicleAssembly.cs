using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleAssembly : MonoBehaviour 
{
	ShipPart focusedPart;
	[SerializeField]
	float attachDistance = 1;
	Vector2 attachPoint;
	Ray ray;
	RaycastHit rayCastHit;
	[SerializeField]
	bool attachingMode;

	void Start () 
	{

	}

	void Update ()
	{
		partSelection ();
	}

	void partSelection()
	{
		if (Input.GetMouseButtonUp (0) == true && attachingMode == false)
		{	
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if(Physics.Raycast(ray, out rayCastHit) == true)
			{
				focusedPart = rayCastHit.collider.gameObject.GetComponent<ShipPart>();
				focusedPart.select();
				attachingMode = true;

			}
		}

		if (focusedPart != null && focusedPart.IsSelected() == true)
		{
			focusedPart.transform.position = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));

			if (Input.GetMouseButtonUp (1) == true)
			{
				focusedPart.deselect();
				attachingMode = false;
			}
		}
	}


}
