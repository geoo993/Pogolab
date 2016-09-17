using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {


	private WebCamDevice[] devices;
	private const int CAMERADEVICENUMBER = 1;
	private string deviceName;
	private WebCamTexture camBackTex;
	public static List<Texture2D> snaps = new List<Texture2D>();


	void SetupCameraOnDevice(){

		devices = WebCamTexture.devices;

		deviceName = devices[CAMERADEVICENUMBER].name;
		//print (deviceName);

		//camBackTex = new WebCamTexture();
		//camBackTex = new WebCamTexture("FaceTime HD Camera", 512, 512);

		//camBackTex = new WebCamTexture(deviceName, 840, 720);
		camBackTex = new WebCamTexture(deviceName, 512, 512);
		//camBackTex = new WebCamTexture(deviceName, 640, 480, 30);
		//camBackTex  =  new WebCamTexture(deviceName, Screen.width, Screen.height,30);

	}


	// Update is called once per frame
	void Update () {

		if (GameManager.sequenceEvents == "setupCam") {

			SetupCameraOnDevice ();

		} else if (GameManager.sequenceEvents == "started") {

			snaps.Clear ();

			camBackTex.Play ();
			GetComponent<MeshRenderer> ().material.mainTexture = camBackTex;

		} else if (GameManager.sequenceEvents == "takeSnap") {

			Texture2D snap = new Texture2D (camBackTex.width, camBackTex.height);
			snap.SetPixels (camBackTex.GetPixels ());
			snap.Apply ();

			GetComponent<MeshRenderer> ().material.mainTexture = snap;
			snaps.Add (snap);
			camBackTex.Stop ();

			GameManager.sequenceEvents = "showPhoto";///remove when reset sequences

		} else if (GameManager.sequenceEvents == "lab") {
			
			camBackTex = null;
			GetComponent<MeshRenderer> ().material.mainTexture = snaps[0];
		}




	}
}
