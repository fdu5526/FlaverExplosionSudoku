using UnityEngine;
using System.Collections;

public class Normal : Person {

	private bool activated;
	private int range;

	// Use this for initialization
	void Start () {
		activated = false;
		range = 2;
	}

	override public void Activate (){
		activated = true;

		if (this.x != null && this.y != null) {
			// find and activate unactivated nearby persons within range and pattern

		
		}

		print ("HEllo world");
		// grab diagonals
	}


	// Update is called once per frame
	void Update () {
	
	}
}
