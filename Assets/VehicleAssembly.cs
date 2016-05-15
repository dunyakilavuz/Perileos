using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class VehicleAssembly : MonoBehaviour 
{
	public ShipPart focusedPart;
	public ShipPart rootPart;
	Ray ray;
	RaycastHit rayCastHit;
	public bool attachingMode;
	public SpaceShip spaceShip;
	public SpaceShip savedSpaceShip;
	int[] myAPoints;
	int[] targetAPoints;
	List<string> shipNamesAsList = new List<string>();
	string[] shipNamesAsArray;
	public GameObject inputField;
	public GameObject partInstantiator;
	public GameObject savedShipPanel;
	ShipPart[] thisParts; 
	ShipPart[] otherParts;
	AttachPoint thisPartAttachPoint;
	AttachPoint otherPartAttachPoint;
	public List<GameObject> buttonList;
	public GameObject button;
	public bool isFirstPartSelection = true;
	GameObject loadedShip = null;
	float shipScaleVertical = 0;
	SphereCollider shipTrigger;

	void Start () 
	{
		spaceShip = new SpaceShip();
		savedSpaceShip = new SpaceShip ();
		shipNamesAsArray = PlayerPrefsX.GetStringArray ("shipNamesAsArray");
		buttonList = new List<GameObject> ();
		for (int i = 0; i < shipNamesAsArray.Length; i++)
		{
			shipNamesAsList.Add(shipNamesAsArray[i]);
			GameObject newButton = Instantiate(button) as GameObject;
			newButton.transform.SetParent(savedShipPanel.transform);
			newButton.transform.GetChild(0).GetComponent<Text>().text = shipNamesAsList[i];
			buttonList.Add(newButton);
		}


		//PlayerPrefs.DeleteAll ();
	}

	void Update ()
	{
		PartSelection ();
		DeleteParts ();

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			Debug.Log(inputField.GetComponent<InputField>().text);
		}
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
				isFirstPartSelection = true;
				inputField.GetComponent<InputField>().text = "";
				inputField.GetComponent<InputField>().interactable = false;
			}
		}
	}

	public void Save()
	{
		if(inputField.GetComponent<InputField>().text != "" && shipNamesAsList.Contains(inputField.GetComponent<InputField>().text) == false)
		{

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

			savedSpaceShip.shipName = inputField.GetComponent<InputField>().text;

			shipNamesAsList.Add(savedSpaceShip.shipName);
			shipNamesAsArray = shipNamesAsList.ToArray();
			PlayerPrefsX.SetStringArray("shipNamesAsArray",shipNamesAsArray);
			PlayerPrefs.SetString("savedSpaceShip.shipName",savedSpaceShip.shipName);
			PlayerPrefsX.SetIntArray (savedSpaceShip.shipName + ".savedSpaceShip.partIndex", savedSpaceShip.partIndex);
			PlayerPrefsX.SetIntArray (savedSpaceShip.shipName + ".myAPoints", myAPoints);
			PlayerPrefsX.SetIntArray (savedSpaceShip.shipName + ".targetAPoints", targetAPoints);
			Debug.Log("Saved.");

			savedSpaceShip.shipName = "";
		}
		else
		{
			Debug.Log("Save failed.");
		}
		
		RefreshButtonList();
	}

	public IEnumerator Loader()
	{
		if (savedSpaceShip.shipName != "")
		{
			savedSpaceShip.partIndex = PlayerPrefsX.GetIntArray (savedSpaceShip.shipName + ".savedSpaceShip.partIndex");
			myAPoints = PlayerPrefsX.GetIntArray (savedSpaceShip.shipName + ".myAPoints");
			targetAPoints = PlayerPrefsX.GetIntArray(savedSpaceShip.shipName + ".targetAPoints");
			
			int[] serialID = new int[savedSpaceShip.partIndex.Length];
			thisPartAttachPoint = null;
			otherPartAttachPoint = null;
			thisParts = new ShipPart[savedSpaceShip.partIndex.Length];
			otherParts = new ShipPart[savedSpaceShip.partIndex.Length];
			
			ShipPart thisPart = null;
			ShipPart otherPart = null;
			int value;
			
			for (int i = 0; i < savedSpaceShip.partIndex.Length; i++) 
			{
				thisPart = (ShipPart)Instantiate(partInstantiator.GetComponent<PartInstantiator>().partList[savedSpaceShip.partIndex[i]], (Vector2)(Camera.main.transform.position), Quaternion.Euler(-90,180,0));
				int.TryParse (thisPart.name, out value);
				thisPart.name = (i + 100 + value).ToString();
				int.TryParse (thisPart.name, out value);
				serialID [i] = value;
				otherPart = GameObject.Find(serialID[thisPart.attachedToIndex].ToString()).GetComponent<ShipPart>();
				thisParts[i] = thisPart;
				otherParts[i] = otherPart;

				if (i == 0) 
				{
					loadedShip = thisPart.gameObject;
				}

				shipScaleVertical += thisPart.GetComponent<BoxCollider>().size.y;
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

				thisPartAttachPoint.otherAttachPoint = otherPartAttachPoint;
				thisPartAttachPoint.Attach ();
			}
		}
		else
		{
			Debug.Log("Load failed.");
		}

	}

	public void DeleteKey()
	{
		if (savedSpaceShip.shipName != "" || savedSpaceShip.shipName != null) 
		{
			shipNamesAsList.Remove(savedSpaceShip.shipName);
			shipNamesAsArray = shipNamesAsList.ToArray();
			PlayerPrefsX.SetStringArray("shipNamesAsArray",shipNamesAsArray);
			PlayerPrefs.DeleteKey (savedSpaceShip.shipName + ".savedSpaceShip.partIndex");
			PlayerPrefs.DeleteKey (savedSpaceShip.shipName + ".myAPoints");
			PlayerPrefs.DeleteKey (savedSpaceShip.shipName + ".targetAPoints");
		}
		else
		{
			Debug.Log("Delete Failed.");
		}
		RefreshButtonList();
	}

	void RefreshButtonList()
	{
		for(int i = 0; i < buttonList.Count; i++)
		{
			Destroy(buttonList[i]);
		}
		shipNamesAsList = null;
		shipNamesAsList = new List<string> ();

		for (int i = 0; i < shipNamesAsArray.Length; i++)
		{
			shipNamesAsList.Add(shipNamesAsArray[i]);
			GameObject newButton = Instantiate(button) as GameObject;
			newButton.transform.parent = savedShipPanel.transform;
			newButton.transform.GetChild(0).GetComponent<Text>().text = shipNamesAsList[i];
		}
	}

	public void Load()
	{
		StartCoroutine(Loader());
	}

	public void Run()
	{
		if (loadedShip != null) 
		{

			for (int i = 0; i < thisParts.Length; i++) 
			{
				if (thisParts [i] != loadedShip.GetComponent<ShipPart> ())
				{
					Destroy (thisParts [i].GetComponent<Rigidbody> ());
				}
			}
			for (int i = 0; i < otherParts.Length; i++) 
			{
				if (otherParts [i] != loadedShip.GetComponent<ShipPart> ())
				{
					Destroy (otherParts [i].GetComponent<Rigidbody> ());
				}
			}

			GameObject[] attachPoints = GameObject.FindGameObjectsWithTag ("attachPoint");
			foreach(GameObject attachPoint in attachPoints)
			{
				Destroy (attachPoint);
			}
				


			GameObject.DontDestroyOnLoad (loadedShip);
			loadedShip.name = "loadedShip";
			loadedShip.AddComponent<ShipController> ();
			loadedShip.GetComponent<ShipController> ().verticalScaleOfShip = shipScaleVertical;
			shipTrigger = loadedShip.AddComponent<SphereCollider> ();
			shipTrigger.isTrigger = true;
			shipTrigger.radius = shipScaleVertical;
			shipTrigger.center -= new Vector3 (0, 0, shipScaleVertical / 2);

			loadedShip.GetComponent<ShipController> ().shipTrigger = shipTrigger;

			Application.LoadLevel ("Launch Scene");

		}
		else 
		{
			Debug.Log ("Run failed.");
		}
	}
}
