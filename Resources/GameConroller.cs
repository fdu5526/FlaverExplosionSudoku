using UnityEngine;
using System.Collections;

public class GameConroller : MonoBehaviour {

	int rowCount = 10;
	int colCount = 6;
	GameObject[][] board;

	// Use this for initialization
	void Start () 
	{
		board = new GameObject[rowCount][];
		for(int r = 0; r < rowCount; r++)
		{
			board[r] = new GameObject[colCount];
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
