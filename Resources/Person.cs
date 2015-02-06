using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Person : MonoBehaviour {
	
	public int x;
	public int y;
	public bool activated;

	public void setPosition(int r, int c){
		this.x = r;
		this.y = c;
	}

	public abstract List<Person> Activate ();
	
}
