using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
	public GameObject mainCamera;
	public GameObject cameraHelper;
	public GameObject planet;
	public GameObject focusedObject;

	public int cameraMaxDistance = 15;
	public int cameraMinDistance = 2;
	int rotateSpeed = 50;

	Vector3 mousePosBefore;

	void Start () 
	{
	
	}

	void Update ()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0 && mainCamera.GetComponent<Camera> ().orthographicSize >= cameraMinDistance)
		{
			mainCamera.GetComponent<Camera> ().orthographicSize -= 1;
			Debug.Log ("Forward");
			Debug.Log (mainCamera.GetComponent<Camera> ().orthographicSize);
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0 && mainCamera.GetComponent<Camera> ().orthographicSize <= cameraMaxDistance)
		{
			mainCamera.GetComponent<Camera> ().orthographicSize += 1;
			Debug.Log ("Backward");
			Debug.Log (mainCamera.GetComponent<Camera> ().orthographicSize);
		}

		if (Input.GetMouseButtonDown (1)) 
		{
			Debug.Log ("true down");
			mousePosBefore = Input.mousePosition;
		}

		if (Input.GetMouseButton (1)) 
		{
			Debug.Log ("true up");
			cameraHelper.transform.RotateAround (planet.transform.position, Vector3.forward, (mousePosBefore.x - Input.mousePosition.x) / rotateSpeed);
		}
	}
}
