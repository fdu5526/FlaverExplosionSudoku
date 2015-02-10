using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BestFriend : Person {

	public BestFriend bestFriend;
	private int range = 2;

	void Start () {
		activated = false;
	}

	public void SetFriend(BestFriend p){
		this.bestFriend = p;
	}

	override public List<Person> Activate (){
		gameObject.renderer.material.color = Color.red;
		activated = true;
		List<Person> toBeActivated = new List<Person> ();
		
		GameObject[,] board = GameObject.Find ("GameMaster").GetComponent<Level1> ().gameObjectBoard;
		int height = GameObject.Find ("GameMaster").GetComponent<Level1> ().height;
		int width = GameObject.Find ("GameMaster").GetComponent<Level1> ().width;

		// if the best friend is activated then assume that they are the one who activated you and infect
		// the surrounding squares diagonally
		// i may have gotten this incorrect but the thing is that best friends infect each other until the last in the chain,
		// which infects like a normal person
		if (bestFriend != null && bestFriend.activated) {
			if(this.x != null && this.y != null){
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
			}
		} else {
			if(bestFriend != null){
				toBeActivated.Add(bestFriend);
			}
		}

		return toBeActivated;
	}

	void Update(){
		if (bestFriend == null) {
			// search for other bestfriend
			GameObject[] objects = GameObject.FindGameObjectsWithTag("person");
			foreach (GameObject o in objects)
			{
				BestFriend b = o.GetComponent<BestFriend>();
				if(b != null && b != this){
					this.SetFriend(b);
					b.SetFriend(this);
				}

			}
		}
	}
}
