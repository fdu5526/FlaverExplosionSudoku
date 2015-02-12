using UnityEngine;
using System.Collections;

public class bfTutorial : MonoBehaviour {

	private Sprite bf0, bf1, bf2, bf3, bf4, bf5, bf6;
	private int state;

	// Use this for initialization
	void Start () {
		bf0 = Resources.Load<Sprite>("bestfriendsTutorial/bf0");
		bf1 = Resources.Load<Sprite>("bestfriendsTutorial/bf1");
		bf2 = Resources.Load<Sprite>("bestfriendsTutorial/bf2");
		bf3 = Resources.Load<Sprite>("bestfriendsTutorial/bf3");
		bf4 = Resources.Load<Sprite>("bestfriendsTutorial/bf4");
		bf5 = Resources.Load<Sprite>("bestfriendsTutorial/bf5");
		bf6 = Resources.Load<Sprite>("bestfriendsTutorial/bf6");

		gameObject.GetComponent<SpriteRenderer>().sprite = bf0;
		state = 0;

		InvokeRepeating("switchFrame", 0.5f, 0.5f);
	}

	private void switchFrame(){
		if (state == 0){
			gameObject.GetComponent<SpriteRenderer>().sprite =  bf1;
			state = 1;
		}
		else if (state == 1){
			gameObject.GetComponent<SpriteRenderer>().sprite =  bf2;
			state = 2;
		}
		else if (state == 2){
			gameObject.GetComponent<SpriteRenderer>().sprite =  bf3;
			state = 3;
		}
		else if (state == 3){
			gameObject.GetComponent<SpriteRenderer>().sprite =  bf4;
			state = 4;
		}
		else if (state == 4){
			gameObject.GetComponent<SpriteRenderer>().sprite =  bf5;
			state = 5;
		}
		else if (state == 5){
			gameObject.GetComponent<SpriteRenderer>().sprite =  bf6;
			state = 6;
		}
		else if (state == 6){
			gameObject.GetComponent<SpriteRenderer>().sprite =  bf0;
			state = 0;
		}
	}

	void OnMouseDown(){
		Application.LoadLevel("level4");
	}
	
	// Update is called once per frame
	void Update () {
	}
}
