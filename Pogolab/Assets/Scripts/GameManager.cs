﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {


	public GameObject mirror = null;
	private Texture texture = null;
	Texture2D galleryImage = null;
	bool isGalleryimageLoaded = false;
	private WebCamDevice[] devices;
	private const int CAMERADEVICENUMBER = 0;
	private string deviceName;
	private WebCamTexture camBackTex;
	private List<Texture2D> snaps=new List<Texture2D>();



	//WWW www; //www object to download


	public Animator[] animator;
	public Animation[] animation;
	MeshRenderer renderer = null;

	public static string getImageButton = "idle";


	public Camera mainCamera;
	private Ray ray;
	private RaycastHit hit;
	private GameObject hitObject = null;


	void Start () {




		SetupCameraOnDevice ();
		renderer = mirror.GetComponent<MeshRenderer> ();


		//StopAnimation ();

	}

	void Update () {


		Animating ();



	}




	void SetupCameraOnDevice(){

		devices = WebCamTexture.devices;
		deviceName = devices[CAMERADEVICENUMBER].name;
		//print (deviceName);

		//camBackTex = new WebCamTexture();
		//camBackTex = new WebCamTexture("FaceTime HD Camera", 512, 512);
		camBackTex = new WebCamTexture(deviceName, 640, 480, 30);
		//camBackTex  =  new WebCamTexture(deviceName, Screen.width, Screen.height,30);

	}
	void CameraSnapShot(){
		if (getImageButton == "idle") {

			GameObject.Find ("button").GetComponent<MeshRenderer> ().material.color = Color.green;


			camBackTex.Play ();
			renderer.material.mainTexture = camBackTex;


		} else if (getImageButton == "takeSnap") {

			GameObject.Find ("button").GetComponent<MeshRenderer> ().material.color = Color.red;
			GameObject.Find ("desk").GetComponent<MeshRenderer> ().material.color = ExtensionMethods.RandomColor ();


			Texture2D snap = new Texture2D (camBackTex.width, camBackTex.height);
			snap.SetPixels (camBackTex.GetPixels ());
			snap.Apply ();

			renderer.material.mainTexture = snap;
			snaps.Add (snap);
			camBackTex.Stop ();

			getImageButton = "photos";


		} else if (getImageButton == "photos") {

			camBackTex = null;

			GameObject.Find ("desk").GetComponent<MeshRenderer> ().material.mainTexture = snaps [0];
			renderer.material.mainTexture = snaps[0];

		}

	}
		




	void StopAnimation(){
		for (int i = 0; i < animator.Length; i++) {
			animator [i].speed = 0;
			//anim [i].Stop ();
		}
	}


	void Animating(){

		float move = Input.GetAxis("Vertical");

		foreach (Animator anim in animator) {

			anim.SetFloat ("Speed", move);

		}

//		for (int i = 0; i < animator.Length; i++) {
//			//Debug.Log(anim [i].layerCount);
//			//anim [i].Play (anim [i].name, 0, 0.5f);
//
//		}
//
//		if (Input.GetKeyDown (KeyCode.Space)) {
//			//Debug.Log ("yess");
//
//			for (int i = 0; i < animator.Length; i++) {
//
//				//float desired_play_time = 10f;
//
//
//				animator[i].speed = 5f;
//				//anim [i].Play (0);
//
//				//animator[i].Stop();
//				//animator[i].Play (anim[i].name, 0, (1f / 30f) * 30f);
//				//Debug.Log("Animation namd: "+animator[i].name + "Animation length: "+animator[i].runtimeAnimatorController);
//			}
//		}

		if (Input.GetMouseButton (0)) {
			HandleInput ();
		}


	}
	void HandleInput () 
	{
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(inputRay, out hit)) {

			hitObject = hit.collider.gameObject;
			hitObject.GetComponent<Renderer> ().material.color = ExtensionMethods.RandomColor ();
			Debug.Log("object: "+ hit.collider.gameObject.name);
		}



	}


	//	public void OnPickPhoto(string filePath){
	//
	//		Debug.Log (filePath);
	//
	//		www = new WWW ("file://" + filePath);  // start downloading that image
	//
	//	}
	//
	void attemptAtAccessingPhotGallery(){

		//if (getImageButton) {

			//isGalleryimageLoaded = false;
			//AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.unityPlayer");
			//AndroidJavaObject ajo = new AndroidJavaObject ("com.photogallerytest.gallery.UnityBinder");

			// open gallery
			//ajo.CallStatic("OpenGallery",ajc.GetStatic<AndroidJavaObject>("currentActivity"));



		//}


		//		if (www != null && www.isDone) {
		//			galleryImage = new Texture2D (www.texture.width, www.texture.height);
		//			galleryImage.SetPixels32 (www.texture.GetPixels32 ()); //copy pixel;
		//			galleryImage.Apply();
		//			www = null;
		//
		//			isGalleryimageLoaded = true;
		//			getImageButton = false;
		//		}
		//
		//		if (isGalleryimageLoaded) {
		//
		//			renderer.material.mainTexture = galleryImage;
		//		}


	}
}
