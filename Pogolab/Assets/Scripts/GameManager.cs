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
	private List<Texture2D> snaps = new List<Texture2D>();


	private Vector3 startPos;
	private Vector3 targetPos;

	public Animator[] animator;

	public Image logo = null;

	public Image[] scanningImages = null;
	public static string getImageButton = "setupCam";

	public Texture2D scannerBlankImage = null;
	private int scanningCount;

	public GameObject mirrorTarget = null;
	public GameObject scannerMirror = null;
	public Image scannerMirrorImage = null;
	private Sprite OtherSprite;

	public Material mat = null;
	private int numberOfTouthPasteLiquid = 0;
	private List<GameObject> touthPasteLiquidObjects = new List<GameObject>();

	public Button takePhotoButton;
	public Button restartButton;
	public Button continueButton;

	public Text headingtext = null;

	public Color interfaceColor = new Color();
	public Color backgroundColor = new Color();

	void Awake () {

		takePhotoButton.GetComponent<Button>().onClick.AddListener( () => {OnClickTakePhotoEvent();} ); 
		restartButton.GetComponent<Button>().onClick.AddListener( () => {OnClickRestartEvent();} ); 
		continueButton.GetComponent<Button>().onClick.AddListener( () => {OnClickContinueEvent();} ); 
	}


	void OnClickTakePhotoEvent()
	{

		Debug.Log ("pressed takephoto button");

		if(getImageButton == "started"){
			getImageButton = "takeSnap";
		}

	}
	void OnClickRestartEvent()
	{

		Debug.Log ("pressed restart button");
		getImageButton = "setupCam";
	}
	void OnClickContinueEvent()
	{

		Debug.Log ("pressed continue button");
		getImageButton = "lab";
	}
		

	void SetupCameraOnDevice(){

		devices = WebCamTexture.devices;

		deviceName = devices[CAMERADEVICENUMBER].name;
		//print (deviceName);

		//camBackTex = new WebCamTexture();
		//camBackTex = new WebCamTexture("FaceTime HD Camera", 512, 512);

		camBackTex = new WebCamTexture(deviceName, 840, 720);
		//camBackTex = new WebCamTexture(deviceName, 640, 480, 30);
		//camBackTex  =  new WebCamTexture(deviceName, Screen.width, Screen.height,30);

	}


	void Update(){


		Camera.main.gameObject.GetComponent<Skybox> ().topColor = backgroundColor;
		Camera.main.gameObject.GetComponent<Skybox> ().midColor = backgroundColor;
		Camera.main.gameObject.GetComponent<Skybox> ().bottomColor = (backgroundColor/1.5f);

		if (getImageButton == "setupCam") {
			
			SetupCameraOnDevice ();
			getImageButton = "started";
			logo.gameObject.SetActive (true);

			//interfaceColor = ExtensionMethods.RandomColor ();
			headingtext.color = interfaceColor;
			continueButton.image.color = interfaceColor;
			restartButton.image.color = interfaceColor;
			takePhotoButton.image.color = interfaceColor;
			foreach (Image img in scanningImages) {
				img.color = interfaceColor;
			}

		}else if (getImageButton == "started") {

			headingtext.GetComponent<Text>().text = "";
			snaps.Clear ();
			scanningCount = 0;
			numberOfTouthPasteLiquid = 0;
			touthPasteLiquidObjects.Clear ();
			startPos = new Vector3 (0f, 0f, 5f);

			Camera.main.gameObject.transform.position = startPos;

			continueButton.gameObject.SetActive (false);
			restartButton.gameObject.SetActive (false);
			takePhotoButton.gameObject.SetActive (true);

			scannerMirror.SetActive (false);
			mirrorTarget.SetActive (true);

			camBackTex.Play ();
			mirror.GetComponent<MeshRenderer> ().material.mainTexture = camBackTex;
			scannerMirrorImage.overrideSprite = Sprite.Create (scannerBlankImage, new Rect (0, 0, 512, 512), new Vector2 (0.5f, 0.5f));

		} else if (getImageButton == "takeSnap") {


			Texture2D snap = new Texture2D (camBackTex.width, camBackTex.height);
			snap.SetPixels (camBackTex.GetPixels ());
			snap.Apply ();

			mirror.GetComponent<MeshRenderer> ().material.mainTexture = snap;
			snaps.Add (snap);
			camBackTex.Stop ();

			getImageButton = "scan";


		} else if (getImageButton == "scan") {

			headingtext.GetComponent<Text>().text = "Scanning";
			scannerMirrorImage.overrideSprite = Sprite.Create (snaps [0], new Rect (0, 0, 840, 720), new Vector2 (0.5f, 0.5f));
			takePhotoButton.gameObject.SetActive (false);
			logo.gameObject.SetActive (false);

			mirrorTarget.SetActive (false);
			scannerMirror.SetActive (true);

			scanningCount++;
			if (scanningCount > 200) {
				getImageButton = "showPhoto";
			}
			
		} else if (getImageButton == "showPhoto") {

			headingtext.GetComponent<Text>().text = "Confirm Photo";
			scannerMirror.SetActive (false);

			restartButton.gameObject.SetActive (true);
			continueButton.gameObject.SetActive (true);

		}else if (getImageButton == "lab") {

			headingtext.GetComponent<Text>().text = "Add Tooth Paste";

			restartButton.gameObject.SetActive (false);
			continueButton.gameObject.SetActive (false);
			scannerMirror.SetActive(false);

			camBackTex = null;

			mirror.GetComponent<MeshRenderer> ().material.mainTexture = snaps[0];



			float speed = 2.5f;
			float smooth = 1.0f - Mathf.Pow(0.5f, Time.deltaTime * speed);

			targetPos = new Vector3 (-25.5f,0,5f);
			Camera.main.gameObject.transform.position = Vector3.Lerp(Camera.main.gameObject.transform.position, targetPos, smooth);


			AnimatingToothMech ();

			float toothPastePackY = GameObject.Find ("toothPastePack").transform.position.y;
			if (toothPastePackY < -0.1f) {

				numberOfTouthPasteLiquid++;
				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.transform.position = new Vector3(-25f, -0.6f, 0.3f);
				sphere.transform.localScale = new Vector3 (0.15f, 0.15f, 0.15f);
				sphere.name = "touthPasteLiquid"+numberOfTouthPasteLiquid;
				sphere.GetComponent<MeshRenderer> ().material = mat;

				sphere.AddComponent<Rigidbody> (); 
				touthPasteLiquidObjects.Add (sphere);

				Debug.Log ("number of ToothPasteLiquids: "+ touthPasteLiquidObjects.Count); 
			}


			for(int i = 0; i< touthPasteLiquidObjects.Count;i++){

				if (touthPasteLiquidObjects[i].transform.position.y < -5.0f) {
					DestroyImmediate (touthPasteLiquidObjects [i]);
					touthPasteLiquidObjects.RemoveAt (i);
				}
			}
		}

	}



	void AnimatingToothMech(){

		Transform moverObject = GameObject.Find ("controlBoxBar").transform;
		float move = valueBetweenZeroAndOne (moverObject.position.y, 0.2f, 1.8f);
		//float move = Input.GetAxis("Vertical");

		foreach (Animator anim in animator) {

			anim.SetFloat ("Speed", move);
		}

		Vector3 toothPastePack = GameObject.Find ("toothPastePack").transform.position;
		//Debug.Log ("Local Position: "+ moverObject.localPosition + "   Position: "+ moverObject.position + "   move float: "+move +" Tooth Paste Pack Pos: "+toothPastePack);
			
	}

	float valueBetweenZeroAndOne(float value, float min, float max) 
	{
		float myPercent;
		float difference = max - min;
		myPercent = ((value - min) / difference);
		return myPercent;
	}
		
}
