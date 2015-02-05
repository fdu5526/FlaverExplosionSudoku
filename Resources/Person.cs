using UnityEngine;
using System.Collections;

public abstract class Person : MonoBehaviour {
	
	public int x;
	public int y;

	// Use this for initialization
	void Start () {
	
	}

	public abstract void Activate ();

	// Update is called once per frame
	void Update () {
	
	}
}
