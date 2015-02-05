using UnityEngine;
using System.Collections;

public class Rumor : MonoBehaviour {

	public int x;
	public int y;
	int range = 1;

	// Use this for initialization
	void Start () {
	
	}

	public void setPosition(int r, int c){
		this.x = r;
		this.y = c;
	}

	public void Infect(){
		GameObject[,] board = GameObject.Find ("GameMaster").GetComponent<Level1>().gameObjectBoard;
		int height = GameObject.Find ("GameMaster").GetComponent<Level1>().height;
		int width = GameObject.Find ("GameMaster").GetComponent<Level1>().width;
	
		// sanity check
		if (x >= width || x < 0) {
			return;
		}
		if (y >= height || y < 0) {
			return;
		}

		if (this.y - 1 > 0) {
			if(board[this.x, this.y-1] != null && !(board[this.x, this.y-1] is EmptySpace)){
				Person p = board[this.x, this.y-1].GetComponent<Person>();
				if(!p.activated){
					p.Activate();
				}
			}
		}
		
		if (this.y + 1 < height) {
			if(board[this.x, this.y+1] != null && !(board[this.x, this.y+1] is EmptySpace)){
				Person p = board[this.x, this.y+1].GetComponent<Person>();
				if(!p.activated){
					p.Activate();
				}
			}
		}

		if(this.x - 1 > 0){

			// same y
			if(board[this.x-1, this.y] != null && !(board[this.x-1, this.y] is EmptySpace)){
				Person p = board[this.x-1, this.y].GetComponent<Person>();
				if(!p.activated){
					p.Activate();
				}
			}

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

		if(this.x + 1 < width){
			// same y
			if(board[this.x+1, this.y] != null && !(board[this.x+1, this.y] is EmptySpace)){
				Person p = board[this.x+1, this.y].GetComponent<Person>();
				if(!p.activated){
					p.Activate();
				}
			}

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


	}

}
