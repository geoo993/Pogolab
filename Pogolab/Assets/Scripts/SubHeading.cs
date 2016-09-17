using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubHeading : MonoBehaviour {


	void Update () {


		if (GameManager.sequenceEvents == "setupCam") {

			GetComponent<Text> ().text = "";


		} else if (GameManager.sequenceEvents == "started") {
			GetComponent<Text> ().color = GameManager.interfaceColor;

		}
		else if (GameManager.sequenceEvents == "scan") {
			GetComponent<Text>().color = GameManager.interfaceColor;
			GetComponent<Text>().text = "Scanning";

		} else if (GameManager.sequenceEvents == "showPhoto") {

			GetComponent<Text>().text = "Confirm Photo";

		}else if (GameManager.sequenceEvents == "lab") {

			GetComponent<Text>().text = "Add ToothPaste";

		}


		
	}
}
