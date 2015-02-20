using UnityEngine;
using System.Collections;

public class exit : MonoBehaviour {

	Sprite Activate, deActivate;

	// Use this for initialization
	void Start () {
		Activate = Resources.Load<Sprite>("sudokuTitle/exitActivate");
		deActivate = Resources.Load<Sprite>("sudokuTitle/exitUnactivate");
	}

	void OnMouseEnter(){
		gameObject.GetComponent<SpriteRenderer>().sprite = Activate;
	}

	void OnMouseExit(){
		gameObject.GetComponent<SpriteRenderer>().sprite = deActivate;
	}

	void OnMouseDown(){
		Application.Quit();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
