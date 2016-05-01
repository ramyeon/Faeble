using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Vuforia;

public class PlayRecording : MonoBehaviour {
	public GameObject pikachu;
	public GameObject articuno;
	public GameObject ninetales;
	private string infile;
	public Button play;
	public Button exit;
	public StreamReader sr;
	public bool isPlaying = false;
	public string line = "";
	public string[] currentFrame;
	public string[] currentModel;
	public GameObject modelToShift;
	public bool pikActive = false;
	public bool artActive = false;
	public bool ninActive = false;
	public int count = 1;
	public AudioClip clip;
	public AudioSource audio;
	public SoundManager soundmanager;
	public returnBut returnButScript;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		
		returnButScript = (returnBut) FindObjectOfType(typeof(returnBut));
		if (returnButScript.scene == 0) {
			infile =  Application.persistentDataPath + "out0.txt";
			Debug.Log("Loading from 0");
		} else {
			Debug.Log("Loading from 1");
			infile =  Application.persistentDataPath + "out1.txt";
		}
		
		//infile =  Application.persistentDataPath + "out0.txt";
		play.onClick.AddListener (beginAnimation);
		exit.onClick.AddListener (exitScene);
	}
	
	void exitScene () {
		Application.LoadLevel("scenes");
	}

	Vector3 Vector3FromString(string s) {
		//get first number (x)
		string[] holder;
		string temp = s.Substring (1, s.Length - 2);
		holder = temp.Split (',');
		float returnx = (float)System.Convert.ToDecimal(holder[0]);
		float returny = (float)System.Convert.ToDecimal(holder[1]);
		float returnz = (float)System.Convert.ToDecimal(holder[2]);
		//build a new vector3 object using the values we've parsed
		Vector3 returnvector3 = new Vector3(returnx,returny,returnz);
		//pass back a vector3 type
		return returnvector3;
	}

	Quaternion QuatFromString(string s) {
		//get first number (x)
		string[] holder;
		string temp = s.Substring (1, s.Length - 2);
		holder = temp.Split (',');
		float returnx = (float)System.Convert.ToDecimal(holder[0]);
		float returny = (float)System.Convert.ToDecimal(holder[1]);
		float returnz = (float)System.Convert.ToDecimal(holder[2]);
		float returni = (float)System.Convert.ToDecimal(holder[3]);
		//build a new vector3 object using the values we've parsed
		Quaternion returnQuat = new Quaternion(returnx,returny,returnz,returni);
		//pass back a vector3 type
		return returnQuat;
	}

	GameObject getModel(string s){
		if (s.Equals ("Ninetales")) {
			return ninetales;
		} else if (s.Equals ("BR_Articuno")) {
			return articuno;
		} else {
			return pikachu;
		}
	}

	void beginAnimation(){
		SoundManager soundmanager = GameObject.FindGameObjectWithTag("persistent").GetComponent<SoundManager>();
		clip = soundmanager.audiop;
		audio.clip = clip;
		if (!isPlaying) {
			sr = new StreamReader (infile);
			isPlaying = true;
			audio.Play();
			if (!sr.EndOfStream) {
				line = sr.ReadLine ();
				currentFrame = line.Split (';');
				foreach (string s in currentFrame){
					if(s.Equals("Ninetales")){
						ninActive = true;
					}
					if(s.Equals("BR_Articuno")){
						artActive = true;
					}
					if(s.Equals("Pikachu")){
						pikActive = true;
					}
					currentModel = s.Split ('/');
					modelToShift = getModel (currentModel[0]);
					modelToShift.transform.position = Vector3FromString(currentModel[1]);
					modelToShift.transform.rotation = QuatFromString(currentModel[2]);
				}
			}
			InvokeRepeating ("readFrames", (float)0.01, (float)0.05);
		}
	}

	void readFrames(){
		Debug.Log ("Playing frame: " + count.ToString ());
		count += 1;
		if (!sr.EndOfStream) {
			line = sr.ReadLine ();
			currentFrame = line.Split(';');
			foreach(string s in currentFrame){
				currentModel = s.Split ('/');
				if(currentModel[0].Equals("Ninetales")){
					ninActive = true;
				}
				if(currentModel[0].Equals("BR_Articuno")){
					artActive = true;
				}
				if(currentModel[0].Equals("Pikachu")){
					pikActive = true;
				}

				modelToShift = getModel (currentModel[0]);
				modelToShift.transform.position = Vector3FromString(currentModel[1]);
				Debug.Log (Vector3FromString(currentModel[1]).ToString());
				modelToShift.transform.rotation = QuatFromString(currentModel[2]);
				Debug.Log (QuatFromString(currentModel[2]).ToString());
			}
		} else {
			audio.Stop();
			Debug.Log("null");
			isPlaying = false;
			sr.Close ();
			CancelInvoke("readFrames");
		}
		if (pikActive == false) {
			pikachu.transform.position = new Vector3 (-1000, -1000, -1000);
		} else {
			pikActive = false;
		}
		if (ninActive == false) {
			ninetales.transform.position = new Vector3 (-1000, -1000, -1000);
		} else {
			ninActive = false;
		}
		if (artActive == false) {
			articuno.transform.position = new Vector3 (-1000, -1000, -1000);
		} else {
			artActive = false;
		}

	}
	// Update is called once per frame
	void Update () {
	
	}
}
