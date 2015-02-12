using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmptySpace : Person {

	static Color blueColor = new Color(0.54f,0.61f,0.76f,1f);
	public bool isActuallyEmpty;
	public bool isActivated;
	float maxSec = 0.1f;
	float startTime;

	// Use this for initialization
	void Start () {
		isActivated = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(isActuallyEmpty && Time.time - startTime > maxSec)
			gameObject.renderer.material.color = Color.white;
	}

	void OnMouseOver()
 	{
 		if(isActivated)
 			return;

 		if(isActuallyEmpty)
   		gameObject.renderer.material.color = Color.green;
   	else
   		gameObject.renderer.material.color = Color.red;
 	}

 	void OnMouseExit()
 	{
 		if(isActivated)
 			return;

 		gameObject.renderer.material.color = Color.white;
 	}

 	override public List<Person> Activate (){
 		
 		if(isActuallyEmpty)
 			gameObject.renderer.material.color = Color.red;
		else
			gameObject.renderer.material.color = blueColor;
 		isActivated = true;
 		startTime = Time.time;

 		return new List<Person>();
 	}

}
