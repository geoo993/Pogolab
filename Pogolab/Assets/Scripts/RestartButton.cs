using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour {

	void Awake () {

		GetComponent<Button>().onClick.AddListener( () => {OnClickRestartEvent();} ); 
	}
	void OnClickRestartEvent()
	{

		Debug.Log ("pressed restart button");
		GameManager.sequenceEvents = "setupCam";
	}

	void Update () {

		if (GameManager.sequenceEvents == "started") {

			//GetComponent<Image> ().color = GameManager.interfaceColor;
			GetComponent<Image> ().enabled = false;
			GetComponent<Button> ().enabled = false;
			GetComponent<RectTransform> ().localPosition = new Vector3 (-315.0f, -950.0f, 0); 
			GetComponent<RectTransform> ().sizeDelta = new Vector2(500.0f, 500.0f);

		} else if (GameManager.sequenceEvents == "showPhoto") {

			GetComponent<Image> ().enabled = true;
			GetComponent<Button> ().enabled = true;
		
				
		} else if (GameManager.sequenceEvents == "lab") {

//			GetComponent<Image> ().enabled = false;
//			GetComponent<Button> ().enabled = false;

			GetComponent<RectTransform> ().localPosition = new Vector3 (0f, -1200.0f, 0); 
			GetComponent<RectTransform> ().sizeDelta = new Vector2(300.0f, 300.0f);

		}
	}
}
