using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorTarget : MonoBehaviour {


	void Update () {

		if (GameManager.sequenceEvents == "setupCam") {
			GetComponent<Image> ().enabled = true;
		} else if (GameManager.sequenceEvents == "started") {
			GetComponent<Image> ().color = GameManager.interfaceColor;
		} else if (GameManager.sequenceEvents == "scan" || GameManager.sequenceEvents == "showPhoto" || GameManager.sequenceEvents == "lab") {
			GetComponent<Image> ().enabled = false;
		}

	}
}
