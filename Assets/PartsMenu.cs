using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartsMenu : MonoBehaviour 
{
	[SerializeField]
	List<ShipPart> partList = new List<ShipPart>();
	ShipPart temp;
	[SerializeField]
	GameObject menuItemFrame;
	float scaleItemFrame = 3;
	GameObject temp2;



	void Start () 
	{
		IconizeAndPlaceParts ();
	}

	void Update () 
	{
	
	}

	void IconizeAndPlaceParts()
	{
		int i = 0;
		while (i < partList.Count)
		{
			if (i % 2 == 0) 
			{
				temp2 = (GameObject)Instantiate (menuItemFrame, new Vector3 (-5, -i + 9, -1), Quaternion.identity);
			}
			else 
			{
				temp2 = (GameObject)Instantiate(menuItemFrame,new Vector3(-3.5f,-i + 10,-1),Quaternion.identity);
			}
			
			temp = (ShipPart)Instantiate (partList [i], new Vector3 (0, 0, 0), Quaternion.identity);

			temp.transform.localScale = temp.transform.localScale / scaleItemFrame;
			temp.transform.parent = temp2.transform;
			temp.transform.localPosition = new Vector3(0,0,-2);
			i++;
		}
	}
}
