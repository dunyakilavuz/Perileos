using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleAssembly : MonoBehaviour 
{
	public ShipPart focusedPart;
	public ShipPart rootPart;
	Ray ray;
	RaycastHit rayCastHit;
	public bool attachingMode;
	public SpaceShip spaceShip;

	void Start () 
	{
		spaceShip = new SpaceShip();
	}

	void Update ()
	{
		PartSelection ();
		DeleteParts ();
	}


	void PartSelection()
	{
		if (Input.GetMouseButtonUp (0) == true && attachingMode == false)
		{	
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if(Physics.Raycast(ray, out rayCastHit) == true)
			{
                if (rayCastHit.collider.gameObject.GetComponent<ShipPart>() != null)
                {
				    focusedPart = rayCastHit.collider.gameObject.GetComponent<ShipPart>();
				    focusedPart.select();
				    attachingMode = true;
                }
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

	void DeleteParts()
	{
		if (attachingMode == true && Input.GetKeyDown (KeyCode.Delete)) 
		{
			Transform[] allChildren = focusedPart.GetComponentsInChildren<Transform>();
			foreach (Transform child in allChildren) 
			{
				spaceShip.shipParts.Remove(child.GetComponent<ShipPart>());
			}
			Destroy(focusedPart.gameObject);

			if(spaceShip.shipParts.Count == 0)
			{
				GameObject.Find ("PartsMenu").GetComponent<PartsMenu> ().isFirstPartSelection = true;
			}
		}
	}

	public void Save()
	{
		Debug.Log ("Save button clicked.");
		
	}

	public void Load()
	{
		Debug.Log ("Load button clicked.");
	}
}
