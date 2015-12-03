using UnityEngine;
using System.Collections;

public class ShipPart : MonoBehaviour 
{

	string partName;
	float mass;

	AttachPoint down;
	AttachPoint up;
	[SerializeField]
	bool isSelected = false;
    bool isAttached = false;

	void Start()
	{
		up = new AttachPoint(new Vector3(transform.position.x,transform.position.y + transform.localScale.y / 2),this);
		down = new AttachPoint(new Vector3(transform.position.x,transform.position.y - transform.localScale.y / 2),this);
	}

	void Update()
	{

	}

	public bool IsSelected()
	{
		return isSelected;
	}

    public bool IsAttached()
    {
        return isAttached;
    }

    public void select()
	{
		isSelected = true;
	}

	public void deselect()
	{
		isSelected = false;
	}

    public void attach()
    {
        isAttached = true;
    }
    public void detach()
    {
        isAttached = false;
    }

	
}
