using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LevelCounter : MonoBehaviour {
	Text lvlTxt;
	int levelNum;
	// Use this for initialization
	void Start () {
		lvlTxt = GetComponent<Text>();
		//lvlTxt.text = "HELLO WORLD";
	}
	
	// Update is called once per frame
	void Update () {
		levelNum = GameObject.Find("GameMaster").GetComponent<Level1>().curLevel;
		if (levelNum == 1){
			lvlTxt.text = "Learning the Basics";
		}
		else if (levelNum == 2){
			lvlTxt.text = "Meeting the Blogger";
		}
		else if (levelNum == 3){
			lvlTxt.text = "Hello World";
		}
		else if (levelNum == 4){
			lvlTxt.text = "Visiting Grandma";
		}
		else if (levelNum == 5){
			lvlTxt.text = "Two's Company";
		}
		else if (levelNum == 6){
			lvlTxt.text = "Going Viral";
		}
		else if (levelNum == 7){
			lvlTxt.text = "Best of Friends";
		}
		else if (levelNum == 8){
			lvlTxt.text = "Dead Zones";
		}
		else if (levelNum == 9){
			lvlTxt.text = "Supply and Demand";
		}
		else if (levelNum == 10){
			lvlTxt.text = "A Job for Grandma";
		}
		else if (levelNum == 11){
			lvlTxt.text = "Log Off?";
		}

		}
	}

