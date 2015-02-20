using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Person : MonoBehaviour {
	
	public int x;
	public int y;
	public bool activated;
	public bool isPlaced;
	public List<EmptySpace> targets;

	public void Init()
	{
		this.targets = new List<EmptySpace> ();
	}

	public void setPosition(int r, int c){
		this.x = c;
		this.y = r;
	}

	public abstract void Highlight();
	public abstract void Unhighlight();
	public abstract List<Person> Activate ();
	
}
