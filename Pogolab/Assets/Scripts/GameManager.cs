using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {


	public Slider[] sliderBars;
	public Text[] mainTexts = null;
	public Image[] scoreIcons = null;
	public GameObject[] radarImages = null;
	public Color interfaceColor = Color.cyan;



	private bool startGame = false;
	public static bool resetGame = false;
	public static string gameOver = "idle";

	private int flashCount = 0;


	Image healthFlashImage;

	public Camera mainCamera;
	private Ray ray;
	private RaycastHit hit;
	private GameObject hitObject = null;

	void Start () {

		//Instantiate( Resources.Load ("CircularGround") );

		StartCoroutine (StartGame ());

	}
	void Update () {

		if (Input.GetMouseButton (0)) {
			HandleInput ();
		}
	}

	void HandleInput () 
	{
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(inputRay, out hit)) {

			hitObject = hit.collider.gameObject;
			hitObject.GetComponent<Renderer> ().material.color = ExtensionMethods.RandomColor ();
			Debug.Log("object: "+ hit.collider.gameObject.name);
		}




	}


	private IEnumerator StartGame () 
	{
		WaitForSeconds wait = new WaitForSeconds (0.01f);


		//Instantiate( Resources.Load ("ArrowLocator") );
	
		//Camera.main.gameObject.GetComponent<CameraTracker> ().enabled = true;

		yield return wait;

		//startGame = false;
	}

	void Game(){



	}


	public void GameOver(){

	
//		if (Input.GetKeyDown ("space") ) 
//		{
//			Restart ();
//		}

	}
	void Restart(){

		resetGame = true;

		if (resetGame){

			resetGame = false;
		}

	}
		


	private void FlashHealth(){


		if (flashCount < 100) {

			healthFlashImage.color = new Color (interfaceColor.r, interfaceColor.g, interfaceColor.b, flashing (0.5f));

			flashCount += 1;
		} else {
			healthFlashImage.color = Color.clear;
		}

	}


	public float percentageValue( float value, float min, float max) 
	{
		float difference = max - min;
		float myPercent = ((value - min) / difference);
		return Mathf.Round(100.0f * myPercent);
	}

	public float flashing( float duration)
	{
		float phi = Time.time / duration * 2 * Mathf.PI;
		float amplitude = Mathf.Cos(phi) * 0.5F + 0.5F;
			
		return amplitude;
	}


}
