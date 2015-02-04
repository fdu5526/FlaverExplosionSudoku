using UnityEngine;
using System.Collections;

public class Level1 : MonoBehaviour {

	int rowCount = 10;
	int colCount = 6;
	GameObject[][] board;

	float gridWidth = 3f;

	// Use this for initialization
	void Start () 
	{
		


		
		board = new GameObject[rowCount][];
		for(int r = 0; r < rowCount; r++)
		{
			board[r] = new GameObject[colCount];
			for(int c = 0; c < colCount; c++)
			{
				board[r][c] = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/Person"));
				board[r][c].transform.position = new Vector3(gridWidth*r-(rowCount/2*gridWidth), 
																										 1f, 
																										 gridWidth*c-(colCount/2*gridWidth));
			}
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
