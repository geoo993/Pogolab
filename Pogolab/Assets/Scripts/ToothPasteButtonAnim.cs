using UnityEngine;
using System.Collections;

public class ToothPasteButtonAnim : MonoBehaviour {

	Animator anim;

	private GameObject hitObject = null;
	private Vector3 screenPoint;
	private Vector3 offset;

	void Start () {
		//anim = GetComponent<Animator>();
	}

	void Update () {


//
//		if ( Input.GetMouseButtonDown(0))
//		{
//			RaycastHit hit;
//			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//			if (Physics.Raycast(ray, out hit)){
//
//				hitObject = hit.collider.gameObject;
//
//				//this.transform.localPosition = new Vector3 (0.0f, hit.collider.gameObject.transform.localPosition.y, 0.01f);
////				if(hit.transform.gameObject.name == "controlBoxBar" )
////				{
////					anim.Play(0);
////				}
//				
//			}
//		}


	}


	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);

		offset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(screenPoint.x, Input.mousePosition.y, screenPoint.z));

	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(screenPoint.x, Input.mousePosition.y, screenPoint.z);

		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;

	}




}
