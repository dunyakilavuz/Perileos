using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
	public GameObject mainCamera;
	public GameObject minimapCamera;
	public GameObject flock;
	public GameObject cameraHelper;
	public GameObject planet;
	public GameObject focusedObject;

	int cameraMaxDistance = 50;
	int cameraMinDistance = 4;
	int rotateSpeed = 50;

	bool mapMode;

	Vector3 mousePosBefore;

	void Start () 
	{
		focusedObject = GameObject.Find ("loadedShip");
		cameraHelper.transform.position = new Vector3 (0, 0, -60);

		if (focusedObject != null) 
		{
			mainCamera.GetComponent<Camera> ().orthographicSize = 15;
			flock.transform.position = new Vector3 (focusedObject.transform.position.x, focusedObject.transform.position.y, flock.transform.position.z);
			flock.transform.parent = focusedObject.transform;
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
		if (Input.GetKeyDown (KeyCode.M))
		{
			mapMode = !mapMode;
		}

		if (mapMode == true) 
		{
			minimapCamera.GetComponent<Camera> ().rect = new Rect (0, 0, 1, 1);
		}
		else 
		{
			minimapCamera.GetComponent<Camera> ().rect = new Rect (0.7f, 0.7f, 1, 1);
		}

		if (focusedObject != null) 
		{
			cameraHelper.transform.position = new Vector3 (focusedObject.transform.position.x, focusedObject.transform.position.y, cameraHelper.transform.position.z);
		} 
		else
		{
			minimapCamera.SetActive (false);
		}

	}
}
