using UnityEngine;
using System.Collections;

public class AttachPoint : MonoBehaviour
{
	AttachPoint otherAttachPoint;
	Behaviour halo;
	GameObject AssemblyManager;
    Vector3 newPosition;

    public bool isAttached;

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
        isAttached = false;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<AttachPoint>() != null)
		{
			otherAttachPoint = other.GetComponent<AttachPoint>();
			otherAttachPoint.halo.enabled = true;	
		}

	}

	void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<AttachPoint>() != null)
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
        if (Input.GetMouseButtonUp(0) == true && transform.parent.GetComponent<ShipPart>().IsSelected() == true && otherAttachPoint != null)
        {
            if (isAttached == false)
            {
                Attach();
            }
            else if (isAttached == true && transform.parent.GetComponent<ShipPart>().IsRoot() == false && transform.parent.parent != null)
            {
                Detach();
            }
        }
	}

	void Attach()
	{
        if (otherAttachPoint.transform.position.y < otherAttachPoint.transform.parent.position.y)
        {
            newPosition = otherAttachPoint.transform.position;
            newPosition -= new Vector3(0, transform.parent.localScale.y / 2, 0);
        }
        else
        {
            newPosition = otherAttachPoint.transform.position;
            newPosition += new Vector3(0, transform.parent.localScale.y / 2, 0);
        }

        transform.parent.position = newPosition;
		transform.parent.GetComponent<ShipPart>().deselect();
		AssemblyManager.GetComponent<VehicleAssembly> ().attachingMode = false;
        transform.parent.parent = otherAttachPoint.transform.parent;
        otherAttachPoint.gameObject.GetComponent<AttachPoint>().shouldActivate(false);
        gameObject.GetComponent<AttachPoint>().shouldActivate(false);
        transform.GetComponent<AttachPoint>().isAttached = true;
        otherAttachPoint.transform.GetComponent<AttachPoint>().isAttached = true;
    }

	void Detach()
	{
        transform.parent.parent = null;
        gameObject.GetComponent<AttachPoint>().shouldActivate(true);
        otherAttachPoint.gameObject.GetComponent<AttachPoint>().shouldActivate(true);
        AssemblyManager.GetComponent<VehicleAssembly>().attachingMode = true;
        transform.GetComponent<AttachPoint>().isAttached = false;
        otherAttachPoint.transform.GetComponent<AttachPoint>().isAttached = false;
       
        Debug.Log("Detached " + transform.parent.gameObject.name + " from " + otherAttachPoint.transform.parent.gameObject.name);

    }

    public void shouldActivate(bool value)
    {
        // We cant simply use SetActive function of gameobject class simply because it disables the objects update function.
        gameObject.GetComponent<MeshRenderer>().enabled = value;
        gameObject.GetComponent<SphereCollider>().enabled = value;
        halo.enabled = value;
    }

}
