using UnityEngine;
using System.Collections;

public class ShipPart : MonoBehaviour 
{
	public CommandModule commandModule = new CommandModule();
	public FuelTank fuelTank = new FuelTank ();
	public RocketEngine rocketEngine = new RocketEngine();
	public Decoupler decoupler = new Decoupler();
	public UtilityPart utilityPart = new UtilityPart();

	[SerializeField]
	string partName;
	float mass;
	public Vector2 attachPointUp;
	public Vector2 attachPointDown;
	public Vector2 attachPointRight;
	public Vector2 attachPointLeft;
	public bool canAttachUp = true;
	public bool canAttachDown = true;
	bool isSelected = false;

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

	void Start()
	{

	}

	void Update()
	{

	}

	public bool IsSelected()
	{
		return isSelected;
	}

	public void select()
	{
		isSelected = true;
	}

	public void deselect()
	{
		isSelected = false;
	}
	
}
