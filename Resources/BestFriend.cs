using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BestFriend : Person {

	public BestFriend bestFriend;
	List<EmptySpace> targets;
	GameObject[,] board;
	GameObject[,] grid;
	int height;
	int width;
	private int range = 2;

	void Start () {
		activated = false;
	}

	public void SetFriend(BestFriend p){
		this.bestFriend = p;
	}

	override public List<Person> Activate (){
		this.GetComponent<Animator>().SetBool("activated", true);
		activated = true;
		List<Person> toBeActivated = new List<Person> ();
		
		board = GameObject.Find ("GameMaster").GetComponent<Level1> ().gameObjectBoard;
		grid = GameObject.Find ("GameMaster").GetComponent<Level1> ().gridBoard;
		height = GameObject.Find ("GameMaster").GetComponent<Level1> ().height;
		width = GameObject.Find ("GameMaster").GetComponent<Level1> ().width;

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
		} else {
			if(bestFriend != null){
				toBeActivated.Add(bestFriend);
			}
		}


		if (this.x != null && this.y != null) {
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
		// grab best friend and its activation squares

		// add yourself
		targets.Add(grid[this.y, this.x].GetComponent<EmptySpace>());

		if (bestFriend != null) {
			targets.Add(grid[bestFriend.y, bestFriend.x].GetComponent<EmptySpace>());
			if (bestFriend.x != null && bestFriend.y != null) {
				// too tired to be clever about this
				// diagonal NE (+, +)
				int cx = bestFriend.x+1;
				int cy = bestFriend.y+1;
				while(cx < width && cy < height && cx < this.x + range + 1){
					// hit a dad and break
					if(board[cy,cx] != null){
						if(board[cy,cx].GetComponent<Dad>() != null){
							break;
						}
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
				cx = bestFriend.x+1;
				cy = bestFriend.y-1;
				while(cx < width && cy >= 0 && cx < this.x + range + 1){
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
					cx++;
					cy--;
				}
				// diagonal SW (-, -)
				cx = bestFriend.x-1;
				cy = bestFriend.y-1;
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
				cx = bestFriend.x - 1;
				cy = bestFriend.y +1;
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
		}
		// grab your own

		if (this.x != null && this.y != null) {
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

		// light them up
		foreach (EmptySpace e in targets) {
			e.TurnOn();
		}
	
	}

	override public void Unhighlight(){
		foreach (EmptySpace e in targets) {
			e.TurnOff();
		}
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
