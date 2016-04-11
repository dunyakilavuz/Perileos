using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PartInstantiator : MonoBehaviour 
{
	int value;
	public List<ShipPart> partList = new List<ShipPart>();
	public GameObject AssemblyManager;

	public void OnClicked(Button button)
	{
		int.TryParse (button.name, out value);
		AssemblyManager.GetComponent<VehicleAssembly>().focusedPart = (ShipPart)Instantiate (partList[value],(Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition)), Quaternion.identity);
		AssemblyManager.GetComponent<VehicleAssembly> ().focusedPart.select ();
		AssemblyManager.GetComponent<VehicleAssembly> ().attachingMode = true;
		AssemblyManager.GetComponent<VehicleAssembly> ().focusedPart.name = value.ToString();
		
		if (AssemblyManager.GetComponent<VehicleAssembly> ().isFirstPartSelection == true) 
		{
			AssemblyManager.GetComponent<VehicleAssembly>().spaceShip.shipParts.Add(AssemblyManager.GetComponent<VehicleAssembly>().focusedPart);
			AssemblyManager.GetComponent<VehicleAssembly>().isFirstPartSelection = false;
			AssemblyManager.GetComponent<VehicleAssembly>().inputField.GetComponent<InputField>().interactable = true;
		}
	}

}
