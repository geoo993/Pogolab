using UnityEngine;
using System.Collections;

public class ButtonAnim : MonoBehaviour {

	Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update () {

		if ( Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast(ray, out hit)){
				
				if(hit.transform.gameObject.name == "button")
				{
					anim.Play(0);
				}
				
			}
		}
	}
}
