using UnityEngine;
using System.Collections;

public class AttachPoint : MonoBehaviour
{
	public Vector3 myPosition;
	GameObject myParent;
	AttachPoint otherAttachPoint;
	Behaviour halo;

	public AttachPoint(Vector3 position,ShipPart parent)
	{
		myParent = parent.gameObject;
		myPosition = position;
		GameObject attachPoint = (GameObject)Instantiate(Resources.Load("attachPoint"));
		attachPoint.transform.position = myPosition;
		attachPoint.transform.localScale = 
			new Vector3(myParent.transform.localScale.x / 1.5f,myParent.transform.localScale.x / 1.5f,myParent.transform.localScale.x / 1.5f);
		attachPoint.transform.parent = myParent.transform;
	}

	void Start()
	{
		halo = (Behaviour)GetComponent("Halo");
		if (myParent == null) 
		{
			Debug.Log("Fuck logic.");
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.name == "attachPoint(Clone)")
		{
			if(otherAttachPoint != null)
			{
				otherAttachPoint.halo.enabled = false; 
			}
			otherAttachPoint = other.GetComponent<AttachPoint>();
			otherAttachPoint.halo.enabled = true;
			
		}
	}

	void Update()
	{
		myPosition = transform.position;
		if (otherAttachPoint != null)
		{
			if((otherAttachPoint.transform.position - myPosition).magnitude < 1)
			{
				myParent.transform.position = otherAttachPoint.transform.position;
				Debug.Log ("1");
			}
		}
	}


}
