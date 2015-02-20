using UnityEngine;
using System.Collections;

public class levelSelector : MonoBehaviour {

	public Sprite Activate, deActivate, Prev, transp;
	public GameObject preview;
	public int levelNum;

	void Start(){
		transp = Resources.Load<Sprite>("LevelSelect/transp");
		preview = GameObject.Find("preview");
		gameObject.GetComponent<SpriteRenderer>().sprite = deActivate;
	}

	void OnMouseEnter(){
		gameObject.GetComponent<SpriteRenderer>().sprite = Activate;
		preview.GetComponent<SpriteRenderer>().sprite = Prev;
	}

	void OnMouseExit(){
		gameObject.GetComponent<SpriteRenderer>().sprite = deActivate;
		preview.GetComponent<SpriteRenderer>().sprite = transp;
	}

	void OnMouseDown(){
		GameObject.Find("GameMaster").GetComponent<Level1>().LoadLevelNumber(levelNum);
	}
	
	
}
