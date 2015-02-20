using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dad : Person {

	// No infection
	void Start () {
		activated = false;
		Init();
	}

	override public List<Person> Activate (){
		return new List<Person> ();
	}

	override public void Highlight(){
	}
	override public void Unhighlight(){
	}
}
