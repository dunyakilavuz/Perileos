using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour
{
	QuestType questType;
	bool isSatisfied;
	bool isFinished;
	string questGivingDialog;
	string questReturningDialog;

	GameObject planetToLand;
	GameObject focusedShip;


	public Quest(QuestType type)
	{
		questType = type;
	}

	public enum QuestType
	{
		LandingMission = 1,
		OrbitingMission = 2,
		SurfaceAnalyzingMission = 3,
	};


	void Start () 
	{

	}

	void Update ()
	{
		if (questType == QuestType.LandingMission)
		{
			
		}
	}
}
	