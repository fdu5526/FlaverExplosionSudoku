using UnityEngine;
using System.Collections;

public abstract class Person : MonoBehaviour {
	
	public int x;
	public int y;
	public bool activated;

	public void setPosition(int r, int c){
		this.x = r;
		this.y = c;
	}

	public abstract void Activate ();
	
}
