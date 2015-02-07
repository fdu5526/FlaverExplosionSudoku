using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmptySpace : Person {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver()
 	{
   	gameObject.renderer.material.color = Color.green;
 	}

 	void OnMouseExit()
 	{
   	gameObject.renderer.material.color = Color.white;
 	}

 	override public List<Person> Activate (){
 		return new List<Person>();
 	}

}
