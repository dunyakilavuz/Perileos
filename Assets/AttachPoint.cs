using UnityEngine;
using System.Collections;

public class AttachPoint : MonoBehaviour
{
	AttachPoint otherAttachPoint;
	Behaviour halo;
	GameObject AssemblyManager;

	public AttachPoint(Vector3 position,ShipPart myParent)
	{
		GameObject attachPoint = (GameObject)Instantiate(Resources.Load("attachPoint"));
		attachPoint.transform.position = position;
		attachPoint.transform.localScale = 
			new Vector3(myParent.transform.localScale.x / 1.5f,myParent.transform.localScale.x / 1.5f,myParent.transform.localScale.x / 1.5f);
		attachPoint.transform.parent = myParent.transform;
	}

	void Start()
	{
		halo = (Behaviour)GetComponent("Halo");
		AssemblyManager = GameObject.Find ("Assembly Manager");
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.name == "attachPoint(Clone)")
		{
			otherAttachPoint = other.GetComponent<AttachPoint>();
			otherAttachPoint.halo.enabled = true;	
		}

	}

	void OnTriggerExit(Collider other)
	{
		if (other.name == "attachPoint(Clone)")
		{
			if(otherAttachPoint != null)
			{
				otherAttachPoint.halo.enabled = false; 
				otherAttachPoint = null;
			}
		}
	}

	void Update()
	{
		if (otherAttachPoint != null)
		{
			if(Input.GetMouseButtonUp(0) == true && transform.parent.GetComponent<ShipPart>().IsSelected() == true)
			{
				Attach();
			}
		}
	}

	void Attach()
	{
        transform.parent.position = otherAttachPoint.transform.position + ((otherAttachPoint.transform.localPosition - transform.localPosition));
		transform.parent.GetComponent<ShipPart>().deselect();
		AssemblyManager.GetComponent<VehicleAssembly> ().attachingMode = false;
        transform.parent.parent = otherAttachPoint.transform.parent;
        otherAttachPoint.gameObject.SetActive(false);
        gameObject.SetActive(false);
	}

	void Detach()
	{

	}

}
