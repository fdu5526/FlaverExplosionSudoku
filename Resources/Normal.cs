using UnityEngine;
using System.Collections;

public class Normal : Person {
	
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
					if(board[this.x-1, this.y-1] != null && !(board[this.x-1, this.y-1] is EmptySpace)){
						Person p = board[this.x-1, this.y-1].GetComponent<Person>();
						if(!p.activated){
							p.Activate();
						}
					}
				}
				if(this.y + 1 < height){
					if(board[this.x-1, this.y+1] != null && !(board[this.x-1, this.y+1] is EmptySpace)){
						Person p = board[this.x-1, this.y+1].GetComponent<Person>();
						if(!p.activated){
							p.Activate();
						}
					}
				}
			}
			if(this.x - 2 > 0){
				if(this.y -	2 > 0){
					if(board[this.x-2, this.y-2] != null && !(board[this.x-2, this.y-2] is EmptySpace)){
						Person p = board[this.x-2, this.y-2].GetComponent<Person>();
						if(!p.activated){
							p.Activate();
						}
					}
				}
				
				if(this.y + 2 < height){
					if(board[this.x-2, this.y+2] != null && !(board[this.x-2, this.y+2] is EmptySpace)){
						Person p = board[this.x-2, this.y+2].GetComponent<Person>();
						if(!p.activated){
							p.Activate();
						}
					}
				}
			}
			if(this.x + 1 < width){
				if(this.y -	1 > 0){
					if(board[this.x+1, this.y-1] != null && !(board[this.x+1, this.y-1] is EmptySpace)){
						Person p = board[this.x+1, this.y-1].GetComponent<Person>();
						if(!p.activated){
							p.Activate();
						}
					}
				}
				
				if(this.y + 1 < height){
					if(board[this.x+1, this.y+1] != null && !(board[this.x+1, this.y+1] is EmptySpace)){
						Person p = board[this.x+1, this.y+1].GetComponent<Person>();
						if(!p.activated){
							p.Activate();
						}
					}
				}
			}
			if(this.x + 2 < width){
				if(this.y -	2 > 0){
					if(board[this.x+2, this.y-2] != null && !(board[this.x+2, this.y-2] is EmptySpace)){
						Person p = board[this.x+2, this.y-2].GetComponent<Person>();
						if(!p.activated){
							p.Activate();
						}
					}
				}
				
				if(this.y + 2 < height){
					if(board[this.x+2, this.y+2] != null && !(board[this.x+2, this.y+2] is EmptySpace)){
						Person p = board[this.x+2, this.y+2].GetComponent<Person>();
						if(!p.activated){
							p.Activate();
						}
					}
				}
			}
		
		}
	}
	
}