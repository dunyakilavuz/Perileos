using UnityEngine;
using System.Collections;

public class ShipPart : MonoBehaviour 
{

	string partName;
	float mass;
	AttachPoint down;
	AttachPoint up;
	[SerializeField]
    bool isRoot = false;
	bool isSelected = false;
	public int attachedToIndex;

	void Start()
	{
		up = new AttachPoint(new Vector3(transform.position.x,transform.position.y + transform.localScale.y / 2),this,"up");
		down = new AttachPoint(new Vector3(transform.position.x,transform.position.y - transform.localScale.y / 2),this,"down");
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
