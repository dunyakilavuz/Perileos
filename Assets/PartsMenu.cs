using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartsMenu : MonoBehaviour 
{
	[SerializeField]
	public List<ShipPart> partList = new List<ShipPart>();
	ShipPart temp;
	[SerializeField]
	GameObject menuItemFrame;
	float scaleItemFrame = 3;
	GameObject temp2;
	public bool isFirstPartSelection  = true;



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
				temp2 = (GameObject)Instantiate(menuItemFrame, transform.position + (new Vector3(-0.7f,4-i ,0)), Quaternion.identity);
			}
			else 
			{
				temp2 = (GameObject)Instantiate(menuItemFrame,transform.position + (new Vector3(0.7f,4,0)),Quaternion.identity);
			}

			temp = (ShipPart)Instantiate (partList [i], new Vector3 (0, 0, 0), Quaternion.identity);
			temp.name = i.ToString();
			Destroy(temp.GetComponent<ShipPart>());
			temp.gameObject.AddComponent<MenuClick>();

			temp.transform.localScale = temp.transform.localScale / scaleItemFrame;
			temp.transform.parent = temp2.transform;
			temp.transform.localPosition = new Vector3(0,0,-2);
			i++;
		}
	}

}
