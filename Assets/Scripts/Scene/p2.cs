using UnityEngine;
using System.Collections;

public class p2 : MonoBehaviour {
	returnBut returnButScript;
	void Awake () {
		returnButScript = (returnBut) FindObjectOfType(typeof(returnBut));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.gameObject.name == "p2") {
					returnButScript.scene = 1;
					Application.LoadLevel("replay");				
				} 
			}
		}
	}
}