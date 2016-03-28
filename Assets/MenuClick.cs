using UnityEngine;
using System.Collections;

public class MenuClick : MonoBehaviour 
{
	int value;

	void OnMouseDown()
	{
		int.TryParse (gameObject.name, out value);
		GameObject.Find("Assembly Manager").GetComponent<VehicleAssembly>().focusedPart = (ShipPart)Instantiate (GameObject.Find("PartsMenu").GetComponent<PartsMenu>().partList[value], 
		                                                                                               (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition)), Quaternion.identity);
		GameObject.Find ("Assembly Manager").GetComponent<VehicleAssembly> ().focusedPart.select ();
		GameObject.Find ("Assembly Manager").GetComponent<VehicleAssembly> ().attachingMode = true;


	}

}
