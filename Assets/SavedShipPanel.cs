using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SavedShipPanel : MonoBehaviour 
{
	public void OnClicked(Button button)
	{
		/*
		for(int i = 0; i < GameObject.Find("Assembly Manager").GetComponent<VehicleAssembly>().buttonList.Count; i++)
		{
			GameObject.Find("Assembly Manager").GetComponent<VehicleAssembly>().buttonList[i].GetComponent<Image> ().color = new Color (1, 1, 1);
		}
		button.GetComponent<Image> ().color = new Color (0, 0.5f, 1);
		*/
		GameObject.Find("Assembly Manager").GetComponent<VehicleAssembly>().savedSpaceShip.shipName = button.transform.GetChild(0).GetComponent<Text>().text;

	}

}
