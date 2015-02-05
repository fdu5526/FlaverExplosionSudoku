using UnityEngine;
using System.Collections;

public class EmptySpace : MonoBehaviour {

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

}
