using UnityEngine;
using System.Collections;

public class Rumor : MonoBehaviour {

	public int x;
	public int y;
	int range = 1;

	// Use this for initialization
	void Start () {
	
	}

	public void setPosition(int r, int c){
		this.x = r;
		this.y = c;
	}

	public void Infect(){
		GameObject[,] board = GameObject.Find ("GameMaster").GetComponent<Level1>().gameObjectBoard;
		int height = GameObject.Find ("GameMaster").GetComponent<Level1>().height;
		int width = GameObject.Find ("GameMaster").GetComponent<Level1>().width;
	
		// sanity check
		if (x >= width || x < 0) {
			return;
		}
		if (y >= height || y < 0) {
			return;
		}

		if(this.x - 1 > 0){


			if(this.y -	1 > 0){
			}
			
			if(this.y + 1 < height){
			}
		}
		if(this.x - 2 > 0){
			if(this.y -	2 > 0){
			}
			
			if(this.y + 2 < height){
			}
		}
		if(this.x + 1 < width){
			if(this.y -	1 > 0){
			}
			
			if(this.y + 1 < height){
			}
		}
		if(this.x + 2 < width){
			if(this.y -	2 > 0){
			}
			
			if(this.y + 2 < height){
			}
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
