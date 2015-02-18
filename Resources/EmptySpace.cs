using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmptySpace : Person {

	static Color blueColor = new Color(0.54f,0.61f,0.76f,1f);
	static Color redColor = new Color(0.86f,0.21f,0.14f,1f);

	int currentSelect = -1;
	
	//string currentSelectionType = "";
	List<EmptySpace> targets = new List<EmptySpace>();

	public bool isActuallyEmpty;
	public bool isActivated, isFlashing;
	float maxSec = 0.3f;
	float startTime;

	// Use this for initialization
	void Start () {
		isActivated = false;
		isFlashing = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - startTime > maxSec && isFlashing)
		{
			gameObject.renderer.material.color = Color.white;
			isFlashing = false;
		}

		currentSelect = GameObject.Find ("GameMaster").GetComponent<Level1> ().dropDownMenu.selection;
		// currentSelectionType = GameObject.Find("GameMaster").GetComponent<Level1>().getCurrentSelection();
	}

	void OnMouseOver()
 	{
 		if(isActivated)
 			return;

 		if (isActuallyEmpty && currentSelect < 0)
			gameObject.renderer.material.color = Color.green;
		else if (isActuallyEmpty && currentSelect >= 0) {
			GameObject[,] board = GameObject.Find ("GameMaster").GetComponent<Level1> ().gameObjectBoard;
			this.Highlight();
		}
		else {
			gameObject.renderer.material.color = blueColor;
			GameObject[,] board = GameObject.Find ("GameMaster").GetComponent<Level1> ().gameObjectBoard;
			if(board[this.y, this.x] != null){
				board[this.y, this.x].GetComponent<Person>().Highlight();
			}
		}
 	}

 	void OnMouseExit()
 	{
 		if(isActivated)
 			return;

 		gameObject.renderer.material.color = Color.white;

		if (!isActuallyEmpty) {
			GameObject[,] board = GameObject.Find ("GameMaster").GetComponent<Level1> ().gameObjectBoard;
			if(board[this.y, this.x] != null){
				board[this.y, this.x].GetComponent<Person>().Unhighlight();
			}
		}else if (isActuallyEmpty && currentSelect >= 0) {
			GameObject[,] board = GameObject.Find ("GameMaster").GetComponent<Level1> ().gameObjectBoard;
			this.Unhighlight();
		}
 	}

	public void TurnOn(){
		gameObject.renderer.material.color = blueColor;
	}

	public void TurnOff(){
		gameObject.renderer.material.color = Color.white;
	
	}
	
	BestFriend existsBestFriend(){
		GameObject[] objects = GameObject.FindGameObjectsWithTag("person");
		foreach(GameObject g in objects){
			if(g.GetComponent<BestFriend>() != null){
				return g.GetComponent<BestFriend>();
			}
		}
		return null;
	}

	override public void Highlight(){
		targets = new List<EmptySpace> ();
		GameObject[,] board = GameObject.Find ("GameMaster").GetComponent<Level1>().gameObjectBoard;
		GameObject[,] grid = GameObject.Find ("GameMaster").GetComponent<Level1> ().gridBoard;
		int height = GameObject.Find ("GameMaster").GetComponent<Level1>().height;
		int width = GameObject.Find ("GameMaster").GetComponent<Level1>().width;
		
		//targets.Add(board[this.y, this.x].GetComponent<EmptySpace>());
		this.TurnOn();
		print (currentSelect);
		
		switch (currentSelect) 
		{
			case 4: //bestfriend
				// cascade into normal I think? Unless another best friend is on the field? Think about it
				// how to check if one is on the field?
				BestFriend friend = existsBestFriend();
				if(friend != null){
					friend.Highlight();
				}
				goto case 1;
			case 1: //normal
				if (this.x != null && this.y != null) {
					
					// diagonal NE (+, +)
					int cx = this.x+1;
					int cy = this.y+1;
					while(cx < width && cy < height && cx < this.x + 2){
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
					while(cx < width && cy >= 0 && cx < this.x + 2){
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
					while(cx >= 0 && cy >= 0 && cx > this.x - 2){
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
					while(cx >= 0 && cy < height && cx > this.x - 2){
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
				break;	
			case 3: // grandma
				if (this.x != null && this.y != null) {
					
					// (y+3, x-1) (y+3, x) (y+3, x+1)
					if(this.y+3 < height){
						if(board[this.y+3, this.x] != null){
							if(grid[this.y+3, this.x] != null){
								targets.Add(grid[this.y+3, this.x].GetComponent<EmptySpace>());
							}else{
								targets.Add(board[this.y+3, this.x].GetComponent<EmptySpace>());
							}
						}
						if(this.x-1 >=0){
							if(board[this.y+3, this.x-1] != null){
								if(grid[this.y+3, this.x-1] != null){
									targets.Add(grid[this.y+3, this.x-1].GetComponent<EmptySpace>());
								}else{
									targets.Add(board[this.y+3, this.x-1].GetComponent<EmptySpace>());
								}
							}
						}
						if(this.x+1 < width){
							if(board[this.y+3, this.x+1] != null){
								if(grid[this.y+3, this.x+1] != null){
									targets.Add(grid[this.y+3, this.x+1].GetComponent<EmptySpace>());
								}else{
									targets.Add(board[this.y+3, this.x+1].GetComponent<EmptySpace>());
								}
							}
						}
					}
					// (y-3, x-1), (y-3, x), (y-3, x+1)
					if(this.y - 3 >= 0){
						if(board[this.y-3, this.x] != null){
							if(grid[this.y-3, this.x] != null){
								targets.Add(grid[this.y-3, this.x].GetComponent<EmptySpace>());
							}else{
								targets.Add(board[this.y-3, this.x].GetComponent<EmptySpace>());
							}
						}
						if(this.x-1 >=0){
							if(board[this.y-3, this.x-1] != null){
								if(grid[this.y-3, this.x-1] != null){
									targets.Add(grid[this.y-3, this.x-1].GetComponent<EmptySpace>());
								}else{
									targets.Add(board[this.y-3, this.x-1].GetComponent<EmptySpace>());
								}
							}
						}
						if(this.x+1 < width){
							if(board[this.y-3, this.x+1] != null){
								if(grid[this.y-3, this.x+1] != null){
									targets.Add(grid[this.y-3, this.x+1].GetComponent<EmptySpace>());
								}else{	
									targets.Add(board[this.y-3, this.x+1].GetComponent<EmptySpace>());
								}
							}
						}
					}
					// (y-1, x+3) (y, x+3) (y+1, x+3)
					if(this.x + 3 < width){
						if(board[this.y, this.x + 3] != null){
							if(grid[this.y, this.x + 3] != null){
								targets.Add(grid[this.y, this.x + 3].GetComponent<EmptySpace>());
							}else{
								targets.Add(board[this.y, this.x + 3].GetComponent<EmptySpace>());
							}
						}
						if(this.y-1 >= 0){
							if(board[this.y-1, this.x + 3] != null){
								if(grid[this.y-1, this.x + 3] != null){
									targets.Add(grid[this.y-1, this.x + 3].GetComponent<EmptySpace>());
								}else{
									targets.Add(board[this.y-1, this.x + 3].GetComponent<EmptySpace>());
								}
							}
						}
						if(this.y+1 < height){
							if(board[this.y+1, this.x + 3] != null){
								if(grid[this.y+1, this.x + 3] != null){
									targets.Add(grid[this.y+1, this.x + 3].GetComponent<EmptySpace>());
								}else{
									targets.Add(board[this.y+1, this.x + 3].GetComponent<EmptySpace>());
								}
							}
						}
					}
					// (y-1, x-3) (y, x-3) (y+1, x-3)
					if(this.x - 3 >= 0){
						if(board[this.y, this.x-3] != null){
							if(grid[this.y, this.x-3] != null){
								targets.Add(grid[this.y, this.x-3].GetComponent<EmptySpace>());
							}else{
								targets.Add(board[this.y, this.x-3].GetComponent<EmptySpace>());
							}
						}
						if(this.y-1 >= 0){
							if(board[this.y-1, this.x - 3] != null){
								if(grid[this.y-1, this.x - 3] != null){
									targets.Add(grid[this.y-1, this.x - 3].GetComponent<EmptySpace>());
								}else{
									targets.Add(board[this.y-1, this.x - 3].GetComponent<EmptySpace>());
								}
							}
						}
						if(this.y+1 < height){
							if(board[this.y+1, this.x - 3] != null){
								if(grid[this.y+1, this.x - 3] != null){
									targets.Add(grid[this.y+1, this.x - 3].GetComponent<EmptySpace>());
								}else{
									targets.Add(board[this.y+1, this.x - 3].GetComponent<EmptySpace>());
								}
							}
						}
					}
				}
			
				break;
			case 2: // blogger
				if(this.x != null && this.y != null){
					// down, different y, same x
					int ct = this.y + 1;
					while(ct < height && ct < this.y + 3 + 1){
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
					while(ct >= 0 && ct > this.y - 3 - 1){
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
					while(ct < width && ct < this.x + 3 + 1){
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
					while(ct >= 0 && ct > this.x - 3 - 1){
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
				break;
			case 5: // dad
				//nada
				break;
			default:
				break;
		}

		foreach (EmptySpace e in targets) {
			e.TurnOn();
		}
	}
	override public void Unhighlight(){
		BestFriend friend = existsBestFriend();
		if(friend != null){
			friend.Unhighlight();
		}
		this.TurnOff();
		foreach (EmptySpace e in targets) {
			e.TurnOff();
		}
	}

 	override public List<Person> Activate (){
 		
 		if(isActuallyEmpty)
		{
			isFlashing = true;
			startTime = Time.time;
		}
		else
		{
			gameObject.renderer.material.color = blueColor;
			isActivated = true;

			//TODO do i need this below?
			isFlashing = true;
			startTime = Time.time;
		}
 		

 		return new List<Person>();
 	}

}
