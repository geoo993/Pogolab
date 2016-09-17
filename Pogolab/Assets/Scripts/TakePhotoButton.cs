using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePhotoButton : MonoBehaviour {


	void Awake () {

		GetComponent<Button>().onClick.AddListener( () => {OnClickTakePhotoEvent();} ); 
	}

	void OnClickTakePhotoEvent()
	{

		Debug.Log ("pressed takephoto button");

		if(GameManager.sequenceEvents == "started"){
			GameManager.sequenceEvents = "takeSnap";
		}

	}

	void Update () {

		if (GameManager.sequenceEvents == "started") {

			//GetComponent<Image> ().color = GameManager.interfaceColor;

			GetComponent<Image> ().enabled = true;
			GetComponent<Button> ().enabled = true;



		} else if (GameManager.sequenceEvents == "scan" || GameManager.sequenceEvents == "showPhoto" || GameManager.sequenceEvents == "lab") {
			GetComponent<Image> ().enabled = false;
			GetComponent<Button> ().enabled = false;

		}




	}
}
