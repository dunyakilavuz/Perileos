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
	public SpaceShip savedSpaceShip;
	public GameObject partsMenu;
	int[] myAPoints;
	int[] targetAPoints;

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
			attachingMode = false;
			if(spaceShip.shipParts.Count == 0)
			{
				partsMenu.GetComponent<PartsMenu> ().isFirstPartSelection = true;
			}
		}
	}

	public void Save()
	{
		Debug.Log ("Save button clicked.");
		savedSpaceShip = new SpaceShip ();
		savedSpaceShip.partIndex = new int[spaceShip.shipParts.Count];
		int value;
		for (int i = 0; i < spaceShip.shipParts.Count; i++) 
		{
			int.TryParse (spaceShip.shipParts[i].name, out value);
			savedSpaceShip.partIndex[i] = value;
		}
		myAPoints = new int[savedSpaceShip.myAttachPoint.Count];
		targetAPoints = new int[savedSpaceShip.myAttachPoint.Count];

		myAPoints = spaceShip.myAttachPoint.ToArray();
		targetAPoints = spaceShip.targetAttachPoint.ToArray();
	}

	public IEnumerator Loader()
	{
		Debug.Log ("Load button clicked.");
		int[] serialID = new int[savedSpaceShip.partIndex.Length];
		AttachPoint thisPartAttachPoint = null;
		AttachPoint otherPartAttachPoint = null;
		ShipPart[] thisParts = new ShipPart[savedSpaceShip.partIndex.Length];
		ShipPart[] otherParts = new ShipPart[savedSpaceShip.partIndex.Length];

		ShipPart root;
		ShipPart thisPart = null;
		ShipPart otherPart = null;
		int value;

		root = (ShipPart)Instantiate(partsMenu.GetComponent<PartsMenu>().partList[savedSpaceShip.partIndex[0]], (Vector2)(Camera.main.transform.position), Quaternion.identity);
		int.TryParse (root.name, out value);
		root.name = (value + 100).ToString ();
		int.TryParse (root.name, out value);
		serialID [0] = value;

		for (int i = 1; i < savedSpaceShip.partIndex.Length; i++) 
		{
			thisPart = (ShipPart)Instantiate(partsMenu.GetComponent<PartsMenu>().partList[savedSpaceShip.partIndex[i]], (Vector2)(Camera.main.transform.position), Quaternion.identity);
			int.TryParse (thisPart.name, out value);
			thisPart.name = (i + 100 + value).ToString();
			int.TryParse (thisPart.tag, out value);
			serialID [i] = value;
			otherPart = GameObject.Find(serialID[thisPart.attachedToIndex].ToString()).GetComponent<ShipPart>();
			thisParts[i] = thisPart;
			otherParts[i] = otherPart;
		}
			
		yield return new WaitForEndOfFrame();

		for (int i = 0; i < myAPoints.Length; i++) 
		{
			Transform[] children = thisParts[i+1].GetComponentsInChildren<Transform>();
			
			foreach(Transform child in children)
			{
				if ((child.name == "attachPoint(up)" && myAPoints[i] == 1) || (child.name == "attachPoint(down)" && myAPoints[i] == 2))
				{
					thisPartAttachPoint = child.GetComponent<AttachPoint>();
				}
			}

			children = otherParts[i+1].GetComponentsInChildren<Transform>();
			foreach(Transform child in children)
			{
				if ((child.name == "attachPoint(up)" && targetAPoints[i] == 1) || (child.name == "attachPoint(down)" && targetAPoints[i] == 2))
				{
					otherPartAttachPoint = child.GetComponent<AttachPoint>();
				}
			}


			Debug.Log("i: " + i + "/" + myAPoints.Length);
			thisPartAttachPoint.otherAttachPoint = otherPartAttachPoint;
			thisPartAttachPoint.Attach();
		}
	}

	public void Load()
	{
		StartCoroutine(Loader());
	}
}
