using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {


	private Vector3 startPos;
	private Vector3 targetPos;

	public Animator[] animator;


	public Image[] scanningImages = null;
	public static string sequenceEvents = "setupCam";

	public Texture2D scannerBlankImage = null;
	private int scanningCount;

	public GameObject scannerMirror = null;
	public Image scannerMirrorImage = null;

	public Material mat = null;
	private int numberOfTouthPasteLiquid = 1;
	private List<GameObject> touthPasteLiquidObjects = new List<GameObject>();

	public static Color interfaceColor = Color.blue;
	public static Color backgroundColor = Color.cyan;



	void Update(){


		Camera.main.gameObject.GetComponent<Skybox> ().topColor = backgroundColor;
		Camera.main.gameObject.GetComponent<Skybox> ().midColor = backgroundColor;
		Camera.main.gameObject.GetComponent<Skybox> ().bottomColor = (backgroundColor/1.5f);

		if (sequenceEvents == "setupCam") {
			
			sequenceEvents = "started";

			interfaceColor = ExtensionMethods.RandomColor ();

			foreach (Image img in scanningImages) {
				img.color = interfaceColor;
			}

		}else if (sequenceEvents == "started") {
			
			scanningCount = 0;
			numberOfTouthPasteLiquid = 0;
			touthPasteLiquidObjects.Clear ();
			startPos = new Vector3 (0f, 0f, 5f);

			Camera.main.gameObject.transform.position = startPos;

			scannerMirror.SetActive (false);

			scannerMirrorImage.overrideSprite = Sprite.Create (scannerBlankImage, new Rect (0, 0, 512, 512), new Vector2 (0.5f, 0.5f));

		} else if (sequenceEvents == "takeSnap") {

			//sequenceEvents = "scan";

			sequenceEvents = "lab";
		} else if (sequenceEvents == "scan") {

			scannerMirrorImage.overrideSprite = Sprite.Create (Mirror.snaps [0], new Rect (0, 0, 840, 720), new Vector2 (0.5f, 0.5f));
		

			scannerMirror.SetActive (true);

			//scanningCount++;
			//if (scanningCount > 200) {
				//sequenceEvents = "showPhoto";
			//}
			
		} else if (sequenceEvents == "showPhoto") {

			scannerMirror.SetActive (false);

		}else if (sequenceEvents == "lab") {

			float speed = 2.5f;
			float smooth = 1.0f - Mathf.Pow(0.5f, Time.deltaTime * speed);

			targetPos = new Vector3 (-25.5f,0,5f);
			Camera.main.gameObject.transform.position = Vector3.Lerp(Camera.main.gameObject.transform.position, targetPos, smooth);

			AnimatingToothMech ();
			ToothPasteLiquids ();

		}

	}



	void ToothPasteLiquids(){
		
		float toothPastePackY = GameObject.Find ("toothPastePack").transform.position.y;
		if (toothPastePackY < -0.1f) {

			numberOfTouthPasteLiquid++;
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.transform.position = new Vector3(-25.6f, -0.6f, 0.3f);
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
