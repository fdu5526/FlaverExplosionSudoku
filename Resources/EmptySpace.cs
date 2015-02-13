using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmptySpace : Person {

	static Color blueColor = new Color(0.54f,0.61f,0.76f,1f);
	static Color redColor = new Color(0.86f,0.21f,0.14f,1f);

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
	}

	void OnMouseOver()
 	{
 		if(isActivated)
 			return;

 		if (isActuallyEmpty)
			gameObject.renderer.material.color = Color.green;
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
		}
 	}

	public void TurnOn(){
		gameObject.renderer.material.color = blueColor;
	}

	public void TurnOff(){
		gameObject.renderer.material.color = Color.white;
	
	}

	override public void Highlight(){
	}
	override public void Unhighlight(){
	}

 	override public List<Person> Activate (){
 		
 		if(isActuallyEmpty)
		{
			gameObject.renderer.material.color = redColor;
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
