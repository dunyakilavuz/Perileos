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
	Vector3 locatedInShip;

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

    public bool IsRoot()
    {
        return isRoot;
    }

    public void select()
	{
		isSelected = true;
	}

	public void deselect()
	{
		isSelected = false;
	}

    public void makeRoot()
    {
        isRoot = true;
    }
    public void revokeRoot()
    {
        isRoot = false;
    }

	
}
