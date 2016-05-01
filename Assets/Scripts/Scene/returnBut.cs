using UnityEngine;
using System.Collections;

public class returnBut : MonoBehaviour {
	public int scene = 0;
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Use this for initialization
	public void backToMain() {
		Debug.Log("Going back to main scene");
		Application.LoadLevel("main");
	}
}