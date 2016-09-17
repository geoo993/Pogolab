using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConitnueButton : MonoBehaviour {

	void Awake () {

		GetComponent<Button>().onClick.AddListener( () => {OnClickContinueEvent();} ); 
	}

	void OnClickContinueEvent()
	{

		Debug.Log ("pressed continue button");
		GameManager.sequenceEvents = "lab";
	}

	void Update () {

		if (GameManager.sequenceEvents == "started") {

			//GetComponent<Image>().color = GameManager.interfaceColor;
			GetComponent<Image> ().enabled = false;
			GetComponent<Button> ().enabled = false;


		} else if (GameManager.sequenceEvents == "showPhoto") {

			GetComponent<Image> ().enabled = true;
			GetComponent<Button> ().enabled = true;


		}else if (GameManager.sequenceEvents == "lab") {

			GetComponent<Image> ().enabled = false;
			GetComponent<Button> ().enabled = false;

		}


	}
}
