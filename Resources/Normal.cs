using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Normal : Person {
	
	private int range;
	List<EmptySpace> targets;


	// Use this for initialization
	void Start () {
		activated = false;
		range = 1;

	}

	override public List<Person> Activate (){
			
		this.GetComponent<Animator>().SetBool("activated", true);
		activated = true;
		List<Person> toBeActivated = new List<Person> ();

		if (this.x != null && this.y != null) {
			// find and activate unactivated nearby persons within range and pattern
			GameObject[,] board = GameObject.Find ("GameMaster").GetComponent<Level1>().gameObjectBoard;
			GameObject[,] grid = GameObject.Find ("GameMaster").GetComponent<Level1> ().gridBoard;
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
						toBeActivated.Add(p);
					}
					cx++;
					cy++;
				}
				
				// diagonal SE (-, +)
				cx = this.x+1;
				cy = this.y-1;
				while(cx < width && cy >= 0 && cx < this.x + range + 1){
					if(board[cy, cx] != null){
						if(board[cy, cx].GetComponent<Dad>() != null){
							break;
						}
						Person p = board[cy, cx].GetComponent<Person>();
						toBeActivated.Add(p);
					}
					cx++;
					cy--;
				}
				// diagonal SW (-, -)
				cx = this.x-1;
				cy = this.y-1;
				while(cx >= 0 && cy >= 0 && cx > this.x - range - 1){
					if(board[cy, cx] != null){
						if(board[cy, cx].GetComponent<Dad>() != null){
							break;
						}
						Person p = board[cy, cx].GetComponent<Person>();
						toBeActivated.Add(p);
					}
					cx--;
					cy--;
				}
				// diagonal NW (+, -)
				cx = this.x - 1;
				cy = this.y +1;
				while(cx >= 0 && cy < height && cx > this.x - range - 1){
					if(board[cy, cx] != null){
						if(board[cy, cx].GetComponent<Dad>() != null){
							break;
						}
						Person p = board[cy, cx].GetComponent<Person>();
						toBeActivated.Add(p);
					}
					cx--;
					cy++;
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
		GameObject[,] board = GameObject.Find ("GameMaster").GetComponent<Level1>().gameObjectBoard;
		GameObject[,] grid = GameObject.Find ("GameMaster").GetComponent<Level1> ().gridBoard;
		int height = GameObject.Find ("GameMaster").GetComponent<Level1>().height;
		int width = GameObject.Find ("GameMaster").GetComponent<Level1>().width;

		// add yourself
		targets.Add(grid[this.y, this.x].GetComponent<EmptySpace>());

		if (this.x != null && this.y != null) {

			// diagonal NE (+, +)
			int cx = this.x+1;
			int cy = this.y+1;
			while(cx < width && cy < height && cx < this.x + range + 1){
				// hit a dad and break
				if(board[cy,cx] != null){
					if(board[cy,cx].GetComponent<Dad>() != null){
						break;
					}
					//EmptySpace p = grid[cy, cx].GetComponent<EmptySpace>();
					if(grid[cy, cx] != null){
						targets.Add(grid[cy, cx].GetComponent<EmptySpace>());
					}else{
						targets.Add(board[cy, cx].GetComponent<EmptySpace>());
					}
				}
				cx++;
				cy++;
			}
			
			// diagonal SE (-, +)
			cx = this.x+1;
			cy = this.y-1;
			while(cx < width && cy >= 0 && cx < this.x + range + 1){
				if(board[cy, cx] != null){
					if(board[cy, cx].GetComponent<Dad>() != null){
						break;
					}
					//EmptySpace p = grid[cy, cx].GetComponent<EmptySpace>();
					if(grid[cy, cx] != null){
						targets.Add(grid[cy, cx].GetComponent<EmptySpace>());
					}else{
						targets.Add(board[cy, cx].GetComponent<EmptySpace>());
					}
				}
				cx++;
				cy--;
			}
			// diagonal SW (-, -)
			cx = this.x-1;
			cy = this.y-1;
			while(cx >= 0 && cy >= 0 && cx > this.x - range - 1){
				if(board[cy, cx] != null){
					if(board[cy, cx].GetComponent<Dad>() != null){
						break;
					}
					if(grid[cy, cx] != null){
						targets.Add(grid[cy, cx].GetComponent<EmptySpace>());
					}else{
						targets.Add(board[cy, cx].GetComponent<EmptySpace>());
					}
				}
				cx--;
				cy--;
			}
			// diagonal NW (+, -)
			cx = this.x - 1;
			cy = this.y +1;
			while(cx >= 0 && cy < height && cx > this.x - range - 1){
				if(board[cy, cx] != null){
					if(board[cy, cx].GetComponent<Dad>() != null){
						break;
					}
					if(grid[cy, cx] != null){
						targets.Add(grid[cy, cx].GetComponent<EmptySpace>());
					}else{
						targets.Add(board[cy, cx].GetComponent<EmptySpace>());
					}
				}
				cx--;
				cy++;
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