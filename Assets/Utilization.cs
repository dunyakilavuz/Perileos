using UnityEngine;
using System.Collections;

public class Utilization : MonoBehaviour 
{
	[SerializeField]
	UnityEngine.UI.Text FPS;

	int frameCounter;
	float timeTemp = 1;

	void Update()
	{
		displayFPS ();
	}


	void displayFPS()
	{
		frameCounter++;
		if (timeTemp <= Time.time)
		{
			FPS.text = "FPS: " + frameCounter;
			frameCounter = 0;
			timeTemp = Time.time + 1;
		}
	}


}
