using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grandma : Person {

	private int range = 3;

	// not affected by dads
	void Start () {
		activated = false;
	}
	
	override public List<Person> Activate (){
		gameObject.renderer.material.color = Color.red;
		activated = true;
		List<Person> toBeActivated = new List<Person> ();

		GameObject[,] board = GameObject.Find ("GameMaster").GetComponent<Level1>().gameObjectBoard;
		int height = GameObject.Find ("GameMaster").GetComponent<Level1>().height;
		int width = GameObject.Find ("GameMaster").GetComponent<Level1>().width;


		if (this.x != null && this.y != null) {
			// (y+3, x), (y-3, x), (y, x+3), (y, x-3)

			// (y+3, x-1) (y+3, x) (y+3, x+1)
			if(this.y+range < height){
				if(board[this.y+range, this.x] != null){
					Person p = board[this.y+range, this.x].GetComponent<Person>();
					toBeActivated.Add(p);
				}
				if(this.x-1 >=0){
					if(board[this.y+range, this.x-1] != null){
						Person p = board[this.y+range, this.x-1].GetComponent<Person>();
						toBeActivated.Add(p);
					}
				}
				if(this.x+1 < width){
					if(board[this.y+range, this.x+1] != null){
						Person p = board[this.y+range, this.x+1].GetComponent<Person>();
						toBeActivated.Add(p);
					}
				}
			}
			// (y-3, x-1), (y-3, x), (y-3, x+1)
			if(this.y - range >= 0){
				if(board[this.y-range, this.x] != null){
					Person p = board[this.y-range, this.x].GetComponent<Person>();
					toBeActivated.Add(p);
				}
				if(this.x-1 >=0){
					if(board[this.y-range, this.x-1] != null){
						Person p = board[this.y-range, this.x-1].GetComponent<Person>();
						toBeActivated.Add(p);
					}
				}
				if(this.x+1 < width){
					if(board[this.y-range, this.x+1] != null){
						Person p = board[this.y-range, this.x+1].GetComponent<Person>();
						toBeActivated.Add(p);
					}
				}
			}
			// (y-1, x+3) (y, x+3) (y+1, x+3)
			if(this.x + range < width){
				if(board[this.y, this.x + range] != null){
					Person p = board[this.y, this.x + range].GetComponent<Person>();
					toBeActivated.Add(p);
				}
				if(this.y-1 >= 0){
					if(board[this.y-1, this.x + range] != null){
						Person p = board[this.y-1, this.x + range].GetComponent<Person>();
						toBeActivated.Add(p);
					}
				}
				if(this.y+1 < height){
					if(board[this.y+1, this.x + range] != null){
						Person p = board[this.y+1, this.x + range].GetComponent<Person>();
						toBeActivated.Add(p);
					}
				}
			}
			// (y-1, x-3) (y, x-3) (y+1, x-3)
			if(this.x - range >= 0){
				if(board[this.y, this.x-range] != null){
					Person p = board[this.y, this.x-range].GetComponent<Person>();
					toBeActivated.Add(p);
				}
				if(this.y-1 >= 0){
					if(board[this.y-1, this.x - range] != null){
						Person p = board[this.y-1, this.x - range].GetComponent<Person>();
						toBeActivated.Add(p);
					}
				}
				if(this.y+1 < height){
					if(board[this.y+1, this.x - range] != null){
						Person p = board[this.y+1, this.x - range].GetComponent<Person>();
						toBeActivated.Add(p);
					}
				}
			}
		}


		return toBeActivated;
	}
}
