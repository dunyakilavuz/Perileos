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
	Vector3 attachmentPoint;

	public class CommandModule
	{
		int crewCapacity;
		public float electricCharge;
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



}
