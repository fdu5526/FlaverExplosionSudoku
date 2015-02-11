using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blogger : Person {

	private int range = 3;

	// Use this for initialization
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

		// recall we access (y,x)
		// account for dads by break when a space we hit is a dad
		if(this.x != null && this.y != null){
			// down, different y, same x
			int ct = this.y + 1;
			while(ct < height && ct < this.y + range + 1){
				if(board[ct, this.x] != null){
					if(board[ct, this.x].GetComponent<Dad>() != null){
						break;
					}
					Person p = board[ct, this.x].GetComponent<Person>();
					if(!(p is EmptySpace)){
						toBeActivated.Add(p);
					}
				}
				ct++;
			}
			// up, different y, same x
			ct = this.y - 1;
			while(ct >= 0 && ct > this.y - range - 1){
				if(board[ct, this.x] != null){
					if(board[ct, this.x].GetComponent<Dad>() != null){
						break;
					}
					Person p = board[ct, this.x].GetComponent<Person>();
					if(!(p is EmptySpace)){
						toBeActivated.Add(p);
					}
				}
				ct--;
			}
			// right, same y, different x
			ct = this.x + 1;
			while(ct < width && ct < this.x + range + 1){
				if(board[this.y, ct] != null){
					if(board[this.y, ct].GetComponent<Dad>() != null){
						break;
					}
					Person p = board[this.y, ct].GetComponent<Person>();
					if(!(p is EmptySpace)){
						toBeActivated.Add(p);
					}
				}
				ct++;
			}
			// left, same y, different x
			ct = this.x - 1;
			while(ct >= 0 && ct > this.x - range - 1){
				if(board[this.y, ct] != null){
					if(board[this.y, ct].GetComponent<Dad>() != null){
						break;
					}
					Person p = board[this.y, ct].GetComponent<Person>();
					if(!(p is EmptySpace)){
						toBeActivated.Add(p);
					}
				}
				ct--;
			}

		}

		return toBeActivated;

	}

}
