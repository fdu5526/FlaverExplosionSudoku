using UnityEngine;
using System.Collections;

public class maintutorial : MonoBehaviour {

	private Sprite mt1, mt2, mt3, mt4, mt5, mt6, mt7;
	private int state;

	// Use this for initialization
	void Start () {
		mt1 = Resources.Load<Sprite>("maintutorial/mt1");
		mt2 = Resources.Load<Sprite>("maintutorial/mt2");
		mt3 = Resources.Load<Sprite>("maintutorial/mt3");
		mt4 = Resources.Load<Sprite>("maintutorial/mt4");
		mt5 = Resources.Load<Sprite>("maintutorial/mt5");
		mt6 = Resources.Load<Sprite>("maintutorial/mt6");
		mt7 = Resources.Load<Sprite>("maintutorial/mt7");


		gameObject.GetComponent<SpriteRenderer>().sprite = mt1;
		state = 0;

		InvokeRepeating("switchFrame", 0.5f, 0.5f);
	}

	private void switchFrame(){
		if (state == 0){
			gameObject.GetComponent<SpriteRenderer>().sprite =  mt2;
			state = 1;
		}
		else if (state == 1){
			gameObject.GetComponent<SpriteRenderer>().sprite =  mt3;
			state = 2;
		}
		else if (state == 2){
			gameObject.GetComponent<SpriteRenderer>().sprite =  mt4;
			state = 3;
		}
		else if (state == 3){
			gameObject.GetComponent<SpriteRenderer>().sprite =  mt5;
			state = 4;
		}
		else if (state == 4){
			gameObject.GetComponent<SpriteRenderer>().sprite =  mt6;
			state = 5;
		}
		else if (state == 5){
			gameObject.GetComponent<SpriteRenderer>().sprite =  mt7;
			state = 6;
		}
		else if (state == 6){
			gameObject.GetComponent<SpriteRenderer>().sprite =  mt1;
			state = 0;
		}
	}

	void OnMouseDown(){
		Application.LoadLevel("bloggerTutorial");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
