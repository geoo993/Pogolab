using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {


	public GameObject mirror = null;

	private WebCamDevice[] devices;
	private const int CAMERADEVICENUMBER = 0;
	private string deviceName;
	private WebCamTexture camBackTex;
	private List<Texture2D> snaps=new List<Texture2D>();


	private Vector3 startPos;
	private Vector3 targetPos;

	public Animator[] animator;
	MeshRenderer renderer = null;

	public static string getImageButton = "idle";

	private int scanningCount;

	public GameObject scannerMirror = null;
	public Image scannerMirrorImage = null;
	private Sprite OtherSprite;

	public Button takePhotoButton;
	public Button restartButton;


	void Awake () {

		takePhotoButton.GetComponent<Button>().onClick.AddListener( () => {OnClickTakePhotoEvent();} ); 
		restartButton.GetComponent<Button>().onClick.AddListener( () => {OnClickRestartEvent();} ); 
	}

	void OnClickTakePhotoEvent()
	{

		Debug.Log ("pressed takephoto button");

		if(getImageButton == "idle"){

			getImageButton = "takeSnap";
		}

	}
	void OnClickRestartEvent()
	{

		Debug.Log ("pressed restart button");
		getImageButton = "idle";
	}


	void Start () {


		SetupCameraOnDevice ();
		renderer = mirror.GetComponent<MeshRenderer> ();



	}

	void Update () {


		Animating ();
		CameraSnapShot ();


	}




	void SetupCameraOnDevice(){

		devices = WebCamTexture.devices;

		deviceName = devices[CAMERADEVICENUMBER].name;
		//print (deviceName);

		//camBackTex = new WebCamTexture();
		//camBackTex = new WebCamTexture("FaceTime HD Camera", 512, 512);

		camBackTex = new WebCamTexture(deviceName, 512, 512);
		//camBackTex = new WebCamTexture(deviceName, 640, 480, 30);
		//camBackTex  =  new WebCamTexture(deviceName, Screen.width, Screen.height,30);

	}


	void CameraSnapShot(){
		
		if (getImageButton == "idle") {

			scanningCount = 0;
			startPos = new Vector3 (0f,0f,5f);
			Camera.main.gameObject.transform.position = startPos;
			scannerMirror.SetActive (false);
			camBackTex.Play ();
			renderer.material.mainTexture = camBackTex;


		} else if (getImageButton == "takeSnap") {

			Texture2D snap = new Texture2D (camBackTex.width, camBackTex.height);
			snap.SetPixels (camBackTex.GetPixels ());
			snap.Apply ();

			renderer.material.mainTexture = snap;
			snaps.Add (snap);
			camBackTex.Stop ();

			getImageButton = "scan";


		} else if (getImageButton == "scan") {


			scannerMirror.SetActive (true);
			scannerMirrorImage.overrideSprite = Sprite.Create(snaps[0], new Rect(0, 0, 512, 512), new Vector2(0.5f, 0.5f));

			scanningCount++;
			if (scanningCount > 100) {
				getImageButton = "photos";
			}
			
		}else if (getImageButton == "photos") {

			camBackTex = null;

			renderer.material.mainTexture = snaps[0];



			float speed = 2.5f;
			float smooth = 1.0f - Mathf.Pow(0.5f, Time.deltaTime * speed);

			targetPos = new Vector3 (-25f,0,5f);
			//Camera.main.gameObject.transform.position = Vector3.Lerp(Camera.main.gameObject.transform.position, targetPos, Time.time);
			Camera.main.gameObject.transform.position = Vector3.Lerp(Camera.main.gameObject.transform.position, targetPos, smooth);

			//OtherSprite = new Rect(0, 0, snaps[0].width, snaps[0].height);
			//OtherSprite = Sprite.Create(snaps[0], new Rect(0, 0, 900, 360), new Vector2(-1f, -1f));
			//scannerMirror.GetComponent<Image> ().sprite = OtherSprite;

			scannerMirror.SetActive(false);
		}

	}



	void Animating(){

		float move = Input.GetAxis("Vertical");

		foreach (Animator anim in animator) {

			anim.SetFloat ("Speed", move);

		}
			


	}
		
}
