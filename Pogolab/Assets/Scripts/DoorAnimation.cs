using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour {

	int lastAnimation = 0;

	Animation animation;
	Animator anim;

	int jumphash = Animator.StringToHash("DoorClose");

	void Start () {
		

		//animation = GetComponent<Animation>();

		anim = GetComponent<Animator>();
	}


	void Update () {

		float move = Input.GetAxis("Vertical");

		anim.SetFloat ("Speed", move);

		if (Input.GetKeyDown (KeyCode.Space)) {
//			print ("yess");

			anim.SetTrigger(jumphash);

			//anim.Play(0);

//			if (lastAnimation == 0) {
//
//
//				animation.Play("DoorOpen");
//				lastAnimation = 1;
//			}
//			if (lastAnimation == 1) {
//
//
				//animation.Play();
				//print (animation);
			//	lastAnimation = 0;
			//}
		}



	}






}
