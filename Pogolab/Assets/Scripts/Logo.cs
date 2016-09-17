using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour {


	void Update () {


		if (GameManager.sequenceEvents == "setupCam") {

			GetComponent<Image> ().enabled = true;
		
		}else if (GameManager.sequenceEvents == "scan" || GameManager.sequenceEvents == "showPhoto" || GameManager.sequenceEvents == "lab") {
			
			GetComponent<Image> ().enabled = false;

		}
	}
}
