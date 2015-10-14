using UnityEngine;
using System.Collections;

public class ShipPart : MonoBehaviour 
{
	public CommandModule commandModule = new CommandModule();
	public FuelTank fuelTank = new FuelTank ();
	public RocketEngine rocketEngine = new RocketEngine();
	public Decoupler decoupler = new Decoupler();
	public UtilityPart utilityPart = new UtilityPart();

	float mass;
	public Vector2 attachmentPointUp;
	public Vector2 attachmentPointDown;

	public class CommandModule
	{
		int crewCapacity;
		float electricCharge;
	}

	public class FuelTank
	{
		float fuelCapacity;
	}

	public class RocketEngine
	{
		float thrust;
		float fuelPerThrust;
	}

	public class Decoupler
	{
		bool decouple;
	}

	public class UtilityPart
	{

	}
	
	void Update()
	{
		attachmentPointUp.x = transform.position.x;
		attachmentPointUp.y = transform.position.y + transform.localScale.y / 2;
		attachmentPointDown.x = transform.position.x;
		attachmentPointDown.y = transform.position.y - transform.localScale.y / 2;
	}

}
