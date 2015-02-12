using UnityEngine;
using System.Collections;

public class grandmaTutorial : MonoBehaviour {

	private Sprite gma0, gma1, gma2, gma3;
	private int state;

	// Use this for initialization
	void Start () {
		gma0 = Resources.Load<Sprite>("Levels/grandmaTutorial/gma0");
		gma1 = Resources.Load<Sprite>("Levels/grandmaTutorial/gma1");
		gma2 = Resources.Load<Sprite>("Levels/grandmaTutorial/gma2");

		gameObject.GetComponent<SpriteRenderer>().sprite = gma0;
		state = 0;

		InvokeRepeating("switchFrame", 0.5f, 0.5f);
	}

	private void switchFrame(){
		if (state == 0){
			gameObject.GetComponent<SpriteRenderer>().sprite =  gma1;
			state = 1;
		}
		else if (state == 1){
			gameObject.GetComponent<SpriteRenderer>().sprite =  gma2;
			state = 2;
		}
		else if (state == 2){
			gameObject.GetComponent<SpriteRenderer>().sprite =  gma0;
			state = 0;
		}
	}

	void OnMouseDown(){
		Application.LoadLevel("level2");
	}
	
	// Update is called once per frame
	void Update () {
	}
}
