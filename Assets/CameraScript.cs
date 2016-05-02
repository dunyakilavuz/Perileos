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

	Vector2 cameraHelperOriginalPos;

	Vector3 mousePosBefore;

	void Start () 
	{
		cameraHelperOriginalPos = cameraHelper.transform.position;
	}

	void Update ()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0 && mainCamera.GetComponent<Camera> ().orthographicSize > cameraMinDistance)
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

		cameraHelper.transform.position = new Vector3 (
			Mathf.Lerp (focusedObject.transform.position.x, cameraHelperOriginalPos.x, mainCamera.GetComponent<Camera> ().orthographicSize / (cameraMaxDistance - cameraMinDistance)),
			Mathf.Lerp (focusedObject.transform.position.y, cameraHelperOriginalPos.y, mainCamera.GetComponent<Camera> ().orthographicSize / (cameraMaxDistance - cameraMinDistance)),
			cameraHelper.transform.position.z);

	}
}
