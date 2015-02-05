using UnityEngine;
using System.Collections;

public class Normal : Person {

	private bool activated;
	private int range;

	// Use this for initialization
	void Start () {
		activated = false;
		range = 2;
	}

	override public void Activate (){
		activated = true;

		if (this.x != null && this.y != null) {
			// find and activate unactivated nearby persons within range and pattern
			GameObject[,] board = GameObject.Find ("GameMaster").GetComponent<Level1>().gameObjectBoard;
			int height = GameObject.Find ("GameMaster").GetComponent<Level1>().height;
			int width = GameObject.Find ("GameMaster").GetComponent<Level1>().width;
			// too tired to be clever about this
			if(this.x - 1 > 0){
				if(this.y -	1 > 0){
					if(board[this.x-1, this.y-1] != null){
					
					}
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
	}

}