using UnityEngine;
using System.Collections;

public class returnBut : MonoBehaviour {

	// Use this for initialization
	public void onMouseDown() {
		Debug.Log("Mouse down");
		Application.LoadLevel("main");
	}
}