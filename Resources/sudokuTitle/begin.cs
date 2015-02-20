using UnityEngine;
using System.Collections;

public class begin : MonoBehaviour {

	Sprite Activate, deActivate;
	// Use this for initialization
	void Start () {
		Activate = Resources.Load<Sprite>("sudokuTitle/beginGameActivate");
		deActivate = Resources.Load<Sprite>("sudokuTitle/beginGameUnactivate");

	}

	void OnMouseDown(){
		Application.LoadLevel ("Level1");
	}
	
	// Update is called once per frame
	void Update () {

	}
}
