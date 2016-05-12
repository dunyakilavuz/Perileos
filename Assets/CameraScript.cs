using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
	public GameObject mainCamera;
	public GameObject cameraHelper;
	public GameObject planet;
	public GameObject focusedObject;

	int cameraMaxDistance = 50;
	int cameraMinDistance = 4;
	int rotateSpeed = 50;

	Vector3 mousePosBefore;

	void Start () 
	{
		focusedObject = GameObject.Find ("loadedShip");
		cameraHelper.transform.position = new Vector3 (0, 0, -60);

		if (focusedObject != null) 
		{
			mainCamera.GetComponent<Camera> ().orthographicSize = 15;
		} 
		else
		{
			mainCamera.GetComponent<Camera> ().orthographicSize = 50;
		}
	}

	void Update ()
	{
		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && mainCamera.GetComponent<Camera> ().orthographicSize > cameraMinDistance) 
		{
			mainCamera.GetComponent<Camera> ().orthographicSize -= 1;
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0 && mainCamera.GetComponent<Camera> ().orthographicSize < cameraMaxDistance) 
		{
			mainCamera.GetComponent<Camera> ().orthographicSize += 1;
		}
		if (Input.GetMouseButtonDown (1)) 
		{
			mousePosBefore = Input.mousePosition;
		}
		if (Input.GetMouseButton (1))
		{
			cameraHelper.transform.RotateAround (planet.transform.position, Vector3.forward, (mousePosBefore.x - Input.mousePosition.x) / rotateSpeed);
		}

		if (focusedObject != null) 
		{
			cameraHelper.transform.position = new Vector3 (focusedObject.transform.position.x, focusedObject.transform.position.y, cameraHelper.transform.position.z);
		} 

	}
}
