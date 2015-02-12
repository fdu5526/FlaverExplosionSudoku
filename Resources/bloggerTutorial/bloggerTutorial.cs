using UnityEngine;
using System.Collections;

public class bloggerTutorial : MonoBehaviour {

	private Sprite blogger0, blogger1, blogger2, blogger3;
	private int state;

	// Use this for initialization
	void Start () {
		blogger0 = Resources.Load<Sprite>("bloggerTutorial/blogger0");
		blogger1 = Resources.Load<Sprite>("bloggerTutorial/blogger1");
		blogger2 = Resources.Load<Sprite>("bloggerTutorial/blogger2");
		blogger3 = Resources.Load<Sprite>("bloggerTutorial/blogger3");

		gameObject.GetComponent<SpriteRenderer>().sprite = blogger0;
		state = 0;

		InvokeRepeating("switchFrame", 0.5f, 0.5f);
	}

	private void switchFrame(){
		if (state == 0){
			gameObject.GetComponent<SpriteRenderer>().sprite =  blogger1;
			state = 1;
		}
		else if (state == 1){
			gameObject.GetComponent<SpriteRenderer>().sprite =  blogger2;
			state = 2;
		}
		else if (state == 2){
			gameObject.GetComponent<SpriteRenderer>().sprite =  blogger3;
			state = 3;
		}
		else if (state == 3){
			gameObject.GetComponent<SpriteRenderer>().sprite =  blogger0;
			state = 0;
		}
	}

	void OnMouseDown(){
		Application.LoadLevel("level1");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
