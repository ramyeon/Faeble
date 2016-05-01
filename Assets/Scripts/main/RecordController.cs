using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Vuforia;

public class RecordController : MonoBehaviour {
	//Buttons
	public Button recordButton;
	public Button sceneButton;
	public Sprite stop;
	public Sprite play;
	//3D Models
	public GameObject pikachu;
	public GameObject articuno;
	public GameObject ninetales;
	//IO Stream
	public StreamWriter sw;
	public bool isRecording = false;
	public bool changeSc = false;
	public IEnumerable<TrackableBehaviour> activeTrackables;
	public string outstr = "";
	public AudioClip myAudioClip;
	public SoundManager soundmanager;
	private int count = 1;
	public GameObject persistent;
	
	
	// Use this for initialization
	void Start () {
		recordButton.onClick.AddListener(toggleRecord);
		sceneButton.onClick.AddListener(sceneChange);
	}
	
	void toggleRecord(){
		isRecording = !isRecording;
		if (isRecording) {
			recordButton.image.sprite = stop;
			Debug.Log(Application.persistentDataPath);
			myAudioClip = Microphone.Start(null,false,60,44100);
			if (count == 0) {
				count = 1;
			} else {
				count = 0;
			}
			sw = new StreamWriter (Application.persistentDataPath + "out" + count.ToString() + ".txt", false);
			InvokeRepeating ("record", (float)0.01, (float)0.05);
		}
		else {
			Microphone.End(null);
			soundmanager = persistent.GetComponent<SoundManager>();
			soundmanager.audiop = myAudioClip;
			recordButton.image.sprite = play;
			sw.Close ();
			CancelInvoke("record");
		}
	}

	void record(){
		foreach (TrackableBehaviour tb in activeTrackables) {
			if(outstr != ""){
				outstr += ";";
			}
			GameObject currRec = GameObject.FindGameObjectWithTag(tb.TrackableName);
			outstr = outstr + currRec.name + "/" + currRec.transform.position.ToString()+ "/"+ currRec.transform.rotation.ToString();
		}
		sw.Write (outstr + "\n");
		sw.Flush ();
		outstr = "";
	}
	
	void sceneChange(){
		Application.LoadLevel("scenes");	
	}
	
	// Update is called once per frame
	void Update () {
		// Get the StateManager
		StateManager sm = TrackerManager.Instance.GetStateManager ();
		
		// Query the StateManager to retrieve the list of
		// currently 'active' trackables 
		//(i.e. the ones currently being tracked by Vuforia)
		activeTrackables = sm.GetActiveTrackableBehaviours ();
	}
}
