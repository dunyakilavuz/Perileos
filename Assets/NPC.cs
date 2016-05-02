using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour 
{
	void Start () 
	{
		Quest quest1 = new Quest ();
	}

	void Update () 
	{
	
	}


}


public class Quest
{
	public Quest()
	{
		
	}

	bool isFinished;
	bool requirement;
	string questDialogBox;

}