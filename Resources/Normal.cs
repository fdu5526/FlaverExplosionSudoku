using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Normal : Person {
	
	private int range;

	// Use this for initialization
	void Start () {
		activated = false;
		range = 2;
	}

	override public List<Person> Activate (){
			
		gameObject.renderer.material.color = Color.red;
		activated = true;
		List<Person> toBeActivated = new List<Person> ();

		if (this.x != null && this.y != null) {
			// find and activate unactivated nearby persons within range and pattern
			GameObject[,] board = GameObject.Find ("GameMaster").GetComponent<Level1>().gameObjectBoard;
			int height = GameObject.Find ("GameMaster").GetComponent<Level1>().height;
			int width = GameObject.Find ("GameMaster").GetComponent<Level1>().width;
			// too tired to be clever about this
				// diagonal NE (+, +)
				int cx = this.x+1;
				int cy = this.y+1;
				while(cx < width && cy < height && cx < this.x + range + 1){
					// hit a dad and break
					if(board[cy,cx] != null){
						if(board[cy,cx].GetComponent<Dad>() != null){
							break;
						}
						Person p = board[cy, cx].GetComponent<Person>();
						if(!(p is EmptySpace)){
							toBeActivated.Add(p);
						}
					}
					cx++;
					cy++;
				}
				
				// diagonal SE (-, +)
				cx = this.x+1;
				cy = this.y-1;
				while(cx < width && cy > 0 && cx < this.x + range + 1){
					if(board[cy, cx] != null){
						if(board[cy, cx].GetComponent<Dad>() != null){
							break;
						}
						Person p = board[cy, cx].GetComponent<Person>();
						if(!(p is EmptySpace)){
							toBeActivated.Add(p);
						}
					}
					cx++;
					cy--;
				}
				// diagonal SW (-, -)
				cx = this.x-1;
				cy = this.y-1;
				while(cx > 0 && cy > 0 && cx > this.x - range - 1){
					if(board[cy, cx] != null){
						if(board[cy, cx].GetComponent<Dad>() != null){
							break;
						}
						Person p = board[cy, cx].GetComponent<Person>();
						if(!(p is EmptySpace)){
							toBeActivated.Add(p);
						}
					}
					cx--;
					cy--;
				}
				// diagonal NW (+, -)
				cx = this.x - 1;
				cy = this.y +1;
				while(cx > 0 && cy < height && cx > this.x - range - 1){
					if(board[cy, cx] != null){
						if(board[cy, cx].GetComponent<Dad>() != null){
							break;
						}
						Person p = board[cy, cx].GetComponent<Person>();
						if(!(p is EmptySpace)){
							toBeActivated.Add(p);
						}
					}
					cx--;
					cy++;
				}
			

			/*
			if(this.y - 1 > 0){
				if(this.x -	1 > 0){
					if(board[this.y-1, this.x-1] != null && !(board[this.y-1, this.x-1] is EmptySpace)){
						Person p = board[this.y-1, this.x-1].GetComponent<Person>();
						if(p != null){
							toBeActivated.Add(p);
							print (p.GetInstanceID());
						}

					}
				}
				if(this.x + 1 < height){
					if(board[this.y-1, this.x+1] != null && !(board[this.y-1, this.x+1] is EmptySpace)){
						Person p = board[this.y-1, this.x+1].GetComponent<Person>();
						if(p != null){
							toBeActivated.Add(p);
							print (p.GetInstanceID());
						}
					}
				}
			}
			if(this.y - 2 > 0){
				if(this.x -	2 > 0){
					if(board[this.y-2, this.x-2] != null && !(board[this.y-2, this.x-2] is EmptySpace)){
						Person p = board[this.y-2, this.x-2].GetComponent<Person>();
						if(p != null){
							toBeActivated.Add(p);
							print (p.GetInstanceID());
						}
						
					}
				}
				
				if(this.x + 2 < height){
					if(board[this.y-2, this.x+2] != null && !(board[this.y-2, this.x+2] is EmptySpace)){
						Person p = board[this.y-2, this.x+2].GetComponent<Person>();
						if(p != null){
							toBeActivated.Add(p);
							print (p.GetInstanceID());
						}
					}
				}
			}
			if(this.y + 1 < width){
				if(this.x -	1 > 0){
					if(board[this.y+1, this.x-1] != null && !(board[this.y+1, this.x-1] is EmptySpace)){
						Person p = board[this.y+1, this.x-1].GetComponent<Person>();
						if(p != null){
							toBeActivated.Add(p);
							print (p.GetInstanceID());
						}
					}
				}
				
				if(this.x + 1 < height){
					if(board[this.y+1, this.x+1] != null && !(board[this.y+1, this.x+1] is EmptySpace)){
						Person p = board[this.y+1, this.x+1].GetComponent<Person>();
						// check for null
						if(p != null){
							toBeActivated.Add(p);
							print (p.GetInstanceID());
						}
					}
				}
			}
			if(this.y + 2 < width){
				if(this.x -	2 > 0){
					if(board[this.y+2, this.x-2] != null && !(board[this.y+2, this.x-2] is EmptySpace)){
						Person p = board[this.y+2, this.x-2].GetComponent<Person>();
						if(p != null){
							toBeActivated.Add(p);
							print (p.GetInstanceID());
						}
					}
				}
				
				if(this.x + 2 < height){
					if(board[this.y+2, this.x+2] != null && !(board[this.y+2, this.x+2] is EmptySpace)){
						Person p = board[this.y+2, this.x+2].GetComponent<Person>();
						if(p != null){
							toBeActivated.Add(p);
							print (p.GetInstanceID());
						}
					}
				}
			}
			*/
		}

		return toBeActivated;
	}
	
}