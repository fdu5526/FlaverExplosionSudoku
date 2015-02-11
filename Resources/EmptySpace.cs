using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmptySpace : Person {

	public bool isActuallyEmpty;
	public bool isActivated;

	// Use this for initialization
	void Start () {
		isActivated = false;
	}
	
	// Update is called once per frame
	void Update () {
	
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
 		gameObject.renderer.material.color = Color.red;
 		isActivated = true;
 		return new List<Person>();
 	}

}
