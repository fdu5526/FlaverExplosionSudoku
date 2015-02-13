using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blogger : Person {

	private int range = 3;
	List<EmptySpace> targets;
	GameObject[,] board;
	GameObject[,] grid;
	int height;
	int width;

	// Use this for initialization
	void Start () {
		activated = false;
		board = GameObject.Find ("GameMaster").GetComponent<Level1>().gameObjectBoard;
		grid = GameObject.Find ("GameMaster").GetComponent<Level1> ().gridBoard;
		height = GameObject.Find ("GameMaster").GetComponent<Level1>().height;
		width = GameObject.Find ("GameMaster").GetComponent<Level1>().width;
	}
	
	override public List<Person> Activate (){
		this.GetComponent<Animator>().SetBool("activated", true);
		activated = true;
		List<Person> toBeActivated = new List<Person> ();
		/*
		GameObject[,] board = GameObject.Find ("GameMaster").GetComponent<Level1>().gameObjectBoard;
		int height = GameObject.Find ("GameMaster").GetComponent<Level1>().height;
		int width = GameObject.Find ("GameMaster").GetComponent<Level1>().width;
*/
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
					toBeActivated.Add(p);
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
					toBeActivated.Add(p);
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
					toBeActivated.Add(p);
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
					toBeActivated.Add(p);
				}
				ct--;
			}

		}

		return toBeActivated;

	}

	
	void OnMouseOver(){
		Highlight ();
	}
	
	void OnMouseExit(){
		Unhighlight ();
	}

	override public void Highlight(){
		targets = new List<EmptySpace> ();

		// add yourself
		targets.Add(grid[this.y, this.x].GetComponent<EmptySpace>());

		if(this.x != null && this.y != null){
			// down, different y, same x
			int ct = this.y + 1;
			while(ct < height && ct < this.y + range + 1){
				if(board[ct, this.x] != null){
					if(board[ct, this.x].GetComponent<Dad>() != null){
						break;
					}

					if(grid[ct, this.x] != null){
						targets.Add(grid[ct, this.x].GetComponent<EmptySpace>());
					}else{
						targets.Add(board[ct, this.x].GetComponent<EmptySpace>());
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
					if(grid[ct, this.x] != null){
						targets.Add(grid[ct, this.x].GetComponent<EmptySpace>());
					}else{
						targets.Add(board[ct, this.x].GetComponent<EmptySpace>());
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
					if(grid[this.y, ct] != null){
						targets.Add(grid[this.y, ct].GetComponent<EmptySpace>());
					}else{
						targets.Add(board[this.y, ct].GetComponent<EmptySpace>());
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
					if(grid[this.y, ct] != null){
						targets.Add(grid[this.y, ct].GetComponent<EmptySpace>());
					}else{
						targets.Add(board[this.y, ct].GetComponent<EmptySpace>());
					}
				}
				ct--;
			}	
		}
		foreach (EmptySpace e in targets) {
			e.TurnOn();
		}
	}

	override public void Unhighlight(){
		foreach (EmptySpace e in targets) {
			e.TurnOff();
		}
	}

}
