using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public bool isLaunching = true;
	public GameObject loadedShip;
	GameObject launchPad;
	[SerializeField]
	Planet[] planets;
	public Planet planetInTerritory;
	public bool oldUserInput;
	public int userInput = 0;

	int frameCounter;
	float timeTemp = 1;

	public GameObject questNamePanel;
	public GameObject questNameText;
	public GameObject questInfoPanel;
	public GameObject questInfoText;
	public GameObject inputPanel;
	public GameObject acceptButton;
	public GameObject declineButton;

	[SerializeField]
	Text FPS;
	[SerializeField]
	Text Altitude;
	[SerializeField]
	Text Throttle;
	[SerializeField]
	Text InTerritory;


	void Awake()
	{
		loadedShip = GameObject.Find ("loadedShip");
		if (loadedShip == null) 
		{
			Altitude.text = "";
			Throttle.text = "";
			InTerritory.text = "";
			GameObject.Find ("NPC").SetActive (false);
			questNameText.GetComponent<Text> ().text = "Hello Astronaut X!";
			questInfoText.GetComponent<Text> ().text = "You can go and create a ship from via clicking on the Vehicle Assembly Building!";
			inputPanel.SetActive (false);
		}
	}

	void Start () 
	{
		launchPad = GameObject.Find ("LaunchPad");


		if (isLaunching == true && loadedShip != null) 
		{
			loadedShip.transform.position = 
				launchPad.transform.position + new Vector3 (0, loadedShip.GetComponent<ShipController>().verticalScaleOfShip + launchPad.transform.localScale.y, 0);
			loadedShip.GetComponent<Rigidbody> ().isKinematic = false;
			isLaunching = false;
		}
	}

	void Update () 
	{
		planetTerritory ();
		displayTexts ();

		if(loadedShip != null)
		{
			loadedShip.transform.parent = planetInTerritory.transform;
		}
	}

	void planetTerritory()
	{
		float max = 0;
		int maxPos = 0;

		for (int i = 0; i < planets.Length; i++)
		{
			if (planets [i].gravityMagnitude > max) 
			{
				max = planets [i].gravityMagnitude;
				maxPos = i;
			}
			planetInTerritory = planets [maxPos];
		}
	}

	void displayTexts()
	{
		if (loadedShip != null)
		{
			Altitude.text = ((loadedShip.transform.position - planetInTerritory.transform.position).magnitude).ToString() + "meters.";
			Throttle.text = "Throttle: " + (loadedShip.GetComponent<ShipController>().throttle * 100).ToString() + "%";
			InTerritory.text = "We're in territory of " + planetInTerritory.name;
		}

		frameCounter++;
		if (timeTemp <= Time.time)
		{
			FPS.text = "FPS: " + frameCounter;
			frameCounter = 0;
			timeTemp = Time.time + 1;
		}
	}


	public void Accept()
	{
		userInput = 1;
	}
	public void Decline()
	{
		userInput = 2;
	}
}
