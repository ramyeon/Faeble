using UnityEngine;
using System.Collections;

public class BackgroundTrans : MonoBehaviour {
	Camera camera;
	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
		{
			Debug.Log("Press left key.");
			Transition();
		}
		else {
			TransitionBack();
		}
	}
	
	void Transition () {
		Color color = GetComponent<Renderer>().material.color;
		if (color.a < 1f) {
			color.a = color.a + Time.deltaTime;
		}
		GetComponent<Renderer>().material.color = color;
	}

	void TransitionBack () {
		Color color = GetComponent<Renderer>().material.color;
		if (color.a >0.3f) {
			color.a = color.a - Time.deltaTime;
		}
		GetComponent<Renderer>().material.color = color;
	}
}
