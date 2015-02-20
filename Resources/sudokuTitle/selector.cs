using UnityEngine;
using System.Collections;

public class selector : MonoBehaviour {

	Sprite Activate, deActivate;

	// Use this for initialization
	void Start () {
		Activate = Resources.Load<Sprite>("sudokuTitle/levelSelectActivate");
		deActivate = Resources.Load<Sprite>("sudokuTitle/levelSelectUnactivate");
	}

	void OnMouseEnter(){
		gameObject.GetComponent<SpriteRenderer>().sprite = Activate;
	}

	void OnMouseExit(){
		gameObject.GetComponent<SpriteRenderer>().sprite = deActivate;
	}

	void OnMouseDown(){
		Application.LoadLevel("levelSelect");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
