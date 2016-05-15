using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NPC : MonoBehaviour 
{
	enum QuestStates
	{
		questNotTaken = 1,
		questOffering = 2,
		questTakenButNotSatisfied = 3,
		questTakenAndSatisfied = 4,
		questTurnedIn = 5,
		questsAreFinished = 6
	};

	QuestStates currentState; 

	[SerializeField]
	bool isGivingQuest;
	[SerializeField]
	bool isReturningQuest;
	[SerializeField]
	bool isNeedsSatisfied;
	[SerializeField]
	bool isFinished;
	[SerializeField]
	bool isQuestAccepted;
	[SerializeField]
	bool allQuestsFinished;
	[SerializeField]
	bool currentQuestContinues;
	[SerializeField]
	bool clickedOnNPC;
	[SerializeField]
	string name;

	Ray ray;
	RaycastHit rayCastHit;

	[SerializeField]
	List<Quest> questsOffered = new List<Quest>();

	public GameObject focusedShip;
	GameManager gameManager;

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
		gameManager = GameObject.Find ("Game Manager").GetComponent<GameManager>();
		focusedShip = gameManager.loadedShip;
		if (name == "Micah") 
		{
			questMicah1 = new Quest(
				Quest.QuestType.LandingMission,
				"Moon Landing!",
				"Welcome back Astronaut X!\n" +
				"So I have a quest for you.It's pretty basic,\n" +
				"but we need you to do it for essentials.\n" +
				"I just want you to go to the moon.\n" +
				"You can go there anytime you want to,\n" +
				"your basic gear will be sufficient enough too.\n" +
				"When you land on the moon come back to me and\n" +
				" I'll reward you handsomely.",

				"Hey there! Still no moon-landing? I thought It was rather easy.",

				"Hey there! Oh you went to the moon and came back? Psh, I knew it was nothing for you. But a word is a word, so here's your reward.",
				gameObject,
				focusedShip,
				Moon);

			questMicah2 = new Quest (
				Quest.QuestType.OrbitingMission,
				"Orbit the Moon!",
				"Hello Astronaut X! I know its gonna be some fatigue duty,\n" +
				" but I have another basic quest for you. \n" +
				"You realized that you can hang on the orbits of the planets and stars didint you? \n" +
				"If you haven't go try it fast, because this workload is about that this time. \n" +
				"If you go to the moon and orbit around it, whole Project contributors will be pleased.\n " +
				"I know, its super easy, but we need to show something to people, and its easy money.\n",

				"Welcome back sport! Oh you haven't finished the orbiting thing yet.\n " +
				"It's fine, just come back when you're done so I'll tell everyone about it.",

				"My hello. And here's your reward. \n"+
				"We kinda all watched how you orbited around the moon. \n" +
				"Don't worry, we won't do it everytime, it was more like ''Hey look benefactors.\n" +
				" Your money is going to someplace good so don't worry.'' thing.\n" +
				" By the way, you may want to visit Ivan, I heard he needs something from you.\n",
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
		isQuestAccepted = questWeAreOn.isQuestAccepted;
		CheckQuestState ();


		if(currentState == QuestStates.questNotTaken) // Quest not taken yet.
		{
			questionMark.SetActive (false);
			exclamationMark.SetActive (true);
			exclamationMark.GetComponent<Renderer> ().material = questYellow;
			isReturningQuest = false;
			isGivingQuest = true;
			questWeAreOn.isActive = false;
			questWeAreOn.isQuestAccepted = false;
			gameManager.questInfoPanel.SetActive (false);
			gameManager.questNamePanel.SetActive (false);
			gameManager.inputPanel.SetActive (false);
			Debug.Log ("Quest not taken yet.");
		}
		else if (currentState == QuestStates.questOffering) // Quest is not taken, but clicked.
		{
			questionMark.SetActive (false);
			exclamationMark.SetActive (true);
			exclamationMark.GetComponent<Renderer> ().material = questBlue;
			isReturningQuest = false;
			isGivingQuest = false;
			if (gameManager.userInput == 1) 
			{
				questWeAreOn.isQuestAccepted = true;
				currentQuestContinues = true;
				gameManager.userInput = 0;
			}
			else if (gameManager.userInput == 2) 
			{
				questWeAreOn.isQuestAccepted = false;
				isGivingQuest = true;
				clickedOnNPC = false;
				currentQuestContinues = false;
				gameManager.userInput = 0;
			}
			questWeAreOn.isActive = true;
			gameManager.questInfoPanel.SetActive (true);
			gameManager.questNamePanel.SetActive (true);
			gameManager.inputPanel.SetActive (true);
			gameManager.questNameText.GetComponent<Text> ().text = questWeAreOn.questName;
			gameManager.questInfoText.GetComponent<Text> ().text = questWeAreOn.questGivingDialog;
			Debug.Log ("Quest needs are not satisfied.");
		}
		else if (currentState == QuestStates.questTakenButNotSatisfied) // Quest is taken but needs are not satisfied, and not ready to turn in.
		{
			questionMark.SetActive (false);
			exclamationMark.SetActive (true);
			exclamationMark.GetComponent<Renderer> ().material = questBlue;
			isReturningQuest = false;
			isGivingQuest = false;
			questWeAreOn.isActive = true;
			currentQuestContinues = true;
			gameManager.questInfoPanel.SetActive (true);
			gameManager.questNamePanel.SetActive (true);
			gameManager.inputPanel.SetActive (false);
			gameManager.questNameText.GetComponent<Text> ().text = questWeAreOn.questName;
			gameManager.questInfoText.GetComponent<Text> ().text = questWeAreOn.questOnDialog;
			Debug.Log ("Quest needs are not satisfied.");
		}
		else if (currentState == QuestStates.questTakenAndSatisfied) // Quests needs are satisfied, but not turned in yet.
		{
			questionMark.SetActive (true);
			exclamationMark.SetActive (false);
			isReturningQuest = true;
			isGivingQuest = false;
			gameManager.questInfoPanel.SetActive (true);
			gameManager.questNamePanel.SetActive (true);
			gameManager.questNameText.GetComponent<Text> ().text = questWeAreOn.questName;
			gameManager.questInfoText.GetComponent<Text> ().text = "Quest requirement is done, you can turn in the quest.";
			gameManager.inputPanel.SetActive (false);
			Debug.Log ("Quest needs are satisfied, not turned in yet.");
		}
		else if(currentState == QuestStates.questTurnedIn) // Quests needs are satisfied, and turned in.
		{
			exclamationMark.SetActive (true);
			questionMark.SetActive (false);
			exclamationMark.GetComponent<Renderer> ().material = questYellow;
			isGivingQuest = true;
			questWeAreOn.isActive = false;
			isReturningQuest = false;
			gameManager.questInfoPanel.SetActive (true);
			gameManager.questNamePanel.SetActive (true);
			gameManager.inputPanel.SetActive (false);
			gameManager.questNameText.GetComponent<Text> ().text = questWeAreOn.questName;
			gameManager.questInfoText.GetComponent<Text> ().text = questWeAreOn.questReturningDialog;
			Debug.Log ("Quest finished.");
		}
		else if(currentState == QuestStates.questsAreFinished) // All quests given are finished.
		{
			exclamationMark.SetActive (false);
			questionMark.SetActive (false);
			isGivingQuest = false;
			isReturningQuest  = false;
			Debug.Log ("All quests finished.");
			gameManager.questNameText.GetComponent<Text> ().text = "No more quests!";
			gameManager.questInfoText.GetComponent<Text> ().text = "You've completed all the quests! Enjoy your day!";
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
				clickedOnNPC = true;
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

	void CheckQuestState()
	{

		if(currentQuestContinues == false && clickedOnNPC == false && questWeAreOn.isQuestAccepted == false && questWeAreOn.isNeedsSatisfied == false && questWeAreOn.isFinished == false && allQuestsFinished == false) // Quest not taken yet.
		{
			currentState = QuestStates.questNotTaken;
		}
		else if(currentQuestContinues == false && clickedOnNPC == true && questWeAreOn.isQuestAccepted == false && questWeAreOn.isNeedsSatisfied == false && questWeAreOn.isFinished == false && allQuestsFinished == false) // Quest not taken, yet clicked.
		{
			currentState = QuestStates.questOffering;
		}
		else if (currentQuestContinues == true && questWeAreOn.isQuestAccepted == true && questWeAreOn.isNeedsSatisfied == false && questWeAreOn.isFinished == false && allQuestsFinished == false) // Quest is taken but needs are not satisfied, and not ready to turn in.
		{
			currentState = QuestStates.questTakenButNotSatisfied;
		}
		else if (currentQuestContinues == true && questWeAreOn.isQuestAccepted == true && questWeAreOn.isNeedsSatisfied == true && questWeAreOn.isFinished == false && allQuestsFinished == false) // Quests needs are satisfied, but not turned in yet.
		{
			currentState = QuestStates.questTakenAndSatisfied;
		}
		else if(currentQuestContinues == false && questWeAreOn.isQuestAccepted == true && questWeAreOn.isNeedsSatisfied == true && questWeAreOn.isFinished == true && allQuestsFinished == false) // Quests needs are satisfied, and turned in.
		{
			currentState = QuestStates.questTurnedIn;
		}
		else if(allQuestsFinished == true) // All quests given are finished.
		{
			currentState = QuestStates.questsAreFinished;
		}

	}
}
