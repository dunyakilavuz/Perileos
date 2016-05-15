using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour 
{
	[SerializeField]
	bool isGivingQuest;
	[SerializeField]
	bool isReturningQuest;
	[SerializeField]
	bool isNeedsSatisfied;
	[SerializeField]
	bool isFinished;
	[SerializeField]
	bool allQuestsFinished;
	[SerializeField]
	bool currentQuestContinues;
	[SerializeField]
	string name;

	Ray ray;
	RaycastHit rayCastHit;

	[SerializeField]
	List<Quest> questsOffered = new List<Quest>();

	public GameObject focusedShip;

	Quest questWeAreOn;

	Quest questMicah1;
	Quest questMicah2;
	Quest questMicah3;
	Quest questMicah4;
	Quest questIvan1;
	Quest questIvan2;

	[SerializeField]
	GameObject questionMark;
	[SerializeField]
	GameObject exclamationMark;
	[SerializeField]
	Material questBlue;
	[SerializeField]
	Material questYellow;
	[SerializeField]
	GameObject Moon;

	void Start () 
	{
		focusedShip = GameObject.Find ("Game Manager").GetComponent<GameManager> ().loadedShip;
		if (name == "Micah") 
		{
			questMicah1 = new Quest(
				Quest.QuestType.LandingMission,
				"",
				"Welcome back Astronaut X! \n" +
				"So I have a quest for you.It's pretty basic, \n" +
				"but we need you to do it for essentials. \n" +
				"I just want you to go to the moon. \n" +
				"You can go there anytime you want to, \n" +
				"your basic gear will be sufficient enough too. \n" +
				"When you land on the moon come back to me and \n" +
				" I'll reward you handsomely.",
				"",
				"",
				gameObject,
				focusedShip,
				Moon);

			questMicah2 = new Quest (
				Quest.QuestType.OrbitingMission,
				"",
				"",
				"",
				"",
				gameObject,
				focusedShip,
				Moon);

			//questMicah3 = new Quest (); Requires New planet Agrann.
			//questMicah4 = new Quest (); Requires New planet Agrann.
			questsOffered.Add (questMicah1);
			questsOffered.Add (questMicah2);
				
		}
		else if(name == "Ivan")
		{
			questIvan1 = new Quest (
				Quest.QuestType.GatheringMaterialMission,
				"",
				"",
				"",
				"",
				gameObject,
				focusedShip,
				Moon);

			//questIvan2 = new Quest (); Requires New planet Agrann.

			questsOffered.Add (questIvan1);
		}
		else
		{
			Debug.Log ("Npc name not defined.");
		}

		questWeAreOn = questMicah1;
		isGivingQuest = true;

	}

	void Update () 
	{
		isNeedsSatisfied = questWeAreOn.isNeedsSatisfied;
		isFinished = questWeAreOn.isFinished;
		if(currentQuestContinues == false && questWeAreOn.isNeedsSatisfied == false && questWeAreOn.isFinished == false && allQuestsFinished == false) // Quest not taken yet.
		{
			questionMark.SetActive (false);
			exclamationMark.SetActive (true);
			exclamationMark.GetComponent<Renderer> ().material = questYellow;
			isReturningQuest = false;
			isGivingQuest = true;
			questWeAreOn.isActive = false;
			Debug.Log ("Quest not taken yet.");
		}
		else if (currentQuestContinues == true && questWeAreOn.isNeedsSatisfied == false && questWeAreOn.isFinished == false && allQuestsFinished == false) // Quest is taken but needs are not satisfied, and not ready to turn in.
		{
			questionMark.SetActive (false);
			exclamationMark.SetActive (true);
			exclamationMark.GetComponent<Renderer> ().material = questBlue;
			isReturningQuest = false;
			isGivingQuest = false;
			questWeAreOn.isActive = true;
			Debug.Log ("Quest needs are not satisfied.");
		}
		else if (currentQuestContinues == true && questWeAreOn.isNeedsSatisfied == true && questWeAreOn.isFinished == false && allQuestsFinished == false) // Quests needs are satisfied, but not turned in yet.
		{
			questionMark.SetActive (true);
			exclamationMark.SetActive (false);
			isReturningQuest = true;
			isGivingQuest = false;
			Debug.Log ("Quest needs are satisfied, not turned in yet.");
		}
		else if(currentQuestContinues == false && questWeAreOn.isNeedsSatisfied == true && questWeAreOn.isFinished == true && allQuestsFinished == false) // Quests needs are satisfied, and turned in.
		{
			exclamationMark.SetActive (true);
			questionMark.SetActive (false);
			exclamationMark.GetComponent<Renderer> ().material = questYellow;
			isGivingQuest = true;
			questWeAreOn.isActive = false;
			isReturningQuest = false;
			Debug.Log ("Quest finished.");
		}
		else if(allQuestsFinished == true) // All quests given are finished.
		{
			exclamationMark.SetActive (false);
			questionMark.SetActive (false);
			isGivingQuest = false;
			isReturningQuest  = false;
			Debug.Log ("All quests finished.");
		}

		if (questWeAreOn.isActive == true) 
		{
			questWeAreOn.CheckQuestProgress ();
		}
			
	}

	void OnMouseDown()
	{
		Debug.Log ("Clicked");
		if (allQuestsFinished == false)
		{
			if (isGivingQuest == true) 
			{
				for (int i = 0; i < questsOffered.Count; i++) 
				{
					if (questsOffered [i].isFinished != true) 
					{
						questWeAreOn = questsOffered [i];
						Debug.Log ("Quest taken.");
						break;
					}
				}
				currentQuestContinues = true;
			} 
			else if (isReturningQuest == true)
			{
				questWeAreOn.isFinished = true;
				allQuestsFinished = true;
				
				for (int i = 0; i < questsOffered.Count; i++) 
				{
					if (questsOffered [i].isFinished != true) 
					{
						allQuestsFinished = false;
						break;
					}
				}
				isReturningQuest = false;
				currentQuestContinues = false;
			}
		}
	}
}
