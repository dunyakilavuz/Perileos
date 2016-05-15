using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour
{
	QuestType questType;
	public bool isQuestAccepted = false;
	public bool isActive = false;
	public bool isFinished;
	public bool isNeedsSatisfied;

	public string questName;
	public string questGivingDialog;
	public string questOnDialog;
	public string questReturningDialog;

	public NPC takenFromNPC;
	GameObject planetOfAction;
	GameObject focusedShip;


	public Quest(QuestType type,string questname ,string qGivingDialog, string onQuestDialog, string qReturningDialog, GameObject takenFrom, GameObject ship, GameObject planet)
	{
		questType = type;
		questName = questname;
		questGivingDialog = qGivingDialog;
		questOnDialog = onQuestDialog;
		questReturningDialog = qReturningDialog;
		takenFromNPC = takenFrom.GetComponent<NPC> ();
		focusedShip = ship;
		planetOfAction = planet;
	}

	public enum QuestType
	{
		LandingMission = 1,
		OrbitingMission = 2,
		SurfaceAnalyzingMission = 3,
		GatheringMaterialMission = 4
	};
			
	public void CheckQuestProgress ()
	{
		if (questType == QuestType.LandingMission) 
		{
			if (focusedShip != null) 
			{
				if (focusedShip.GetComponent<ShipController> ().shipTouchingWith.gameObject == planetOfAction) 
				{
					isNeedsSatisfied = true;
					Debug.Log ("Quest Finished");
				}
			} 
			else 
			{
				Debug.Log ("Focused Ship is null.");
			}

				
		} 
		else if (questType == QuestType.OrbitingMission) 
		{
			if (focusedShip != null) 
			{
				if (GameObject.Find("Game Manager").GetComponent<GameManager>().planetInTerritory.gameObject == planetOfAction) 
				{
					isNeedsSatisfied = true;
					Debug.Log ("Quest Finished");
				}
			} 
			else 
			{
				Debug.Log ("Focused Ship is null.");
			}
		} 
		else if (questType == QuestType.SurfaceAnalyzingMission) 
		{
			if (focusedShip != null) 
			{
				if (focusedShip.GetComponent<ShipController> ().shipTouchingWith.gameObject == planetOfAction) 
				{
					isNeedsSatisfied = true;
					Debug.Log ("Quest Finished");
				}
			} 
			else 
			{
				Debug.Log ("Focused Ship is null.");
			}
		}
		else if (questType == QuestType.GatheringMaterialMission) 
		{
			if (focusedShip != null) 
			{
				if (focusedShip.GetComponent<ShipController> ().shipTouchingWith.gameObject == planetOfAction) 
				{
					isNeedsSatisfied = true;
					Debug.Log ("Quest Finished");
				}
			} 
			else 
			{
				Debug.Log ("Focused Ship is null.");
			}
		}
		else 
		{
			Debug.Log ("Quest type not identified.");
		}


	}
}
	