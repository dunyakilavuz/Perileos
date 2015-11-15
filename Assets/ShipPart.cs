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

	AttachPoint down;
	AttachPoint up;

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
		up = new AttachPoint(new Vector3(transform.position.x,transform.position.y + transform.localScale.y / 2),this);
		down = new AttachPoint (new Vector3(transform.position.x,transform.position.y - transform.localScale.y / 2),this);
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
