using UnityEngine;
using System.Collections;

public class returnBut : MonoBehaviour {
	void Start() {
		
	}

	// Use this for initialization
	public void backToMain() {
		Debug.Log("Mouse down");
		Application.LoadLevel("main");
	}
}