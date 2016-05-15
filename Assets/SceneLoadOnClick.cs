using UnityEngine;
using System.Collections;

public class SceneLoadOnClick : MonoBehaviour 
{
	void OnMouseDown()
	{
		Application.LoadLevel ("Vehicle Assembly Scene");
	}

}
