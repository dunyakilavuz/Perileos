using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public bool isLaunching = true;
	GameObject loadedShip;
	GameObject launchPad;
	[SerializeField]
	Planet[] planets;
	public Planet planetInTerritory;

	void Start () 
	{
		launchPad = GameObject.Find ("LaunchPad");
		loadedShip = GameObject.Find ("loadedShip");

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

		loadedShip.transform.parent = planetInTerritory.transform;
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
}
