using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dad : Person {

	// No infection
	void Start () {
		activated = false;
	}

	override public List<Person> Activate (){
		return new List<Person> ();
	}

}
