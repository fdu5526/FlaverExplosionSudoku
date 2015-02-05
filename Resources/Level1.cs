using UnityEngine;
using System.Collections;

public class Level1 : MonoBehaviour {

	/**
	 *
	 -1: dead space, cannot place anything
	 	0: empty square
	 	1: normal person
	 	2: blogger
	 	3: grandma
	 */
	int[,] board = new int[,]
	{
		{0, 0, 1, 0, 0},
		{0, 1, 0, 1, 0},
		{0, 0, 1, 0, 0},
		{0, 0, 0, 0, 1},
		{0, 0, 0, 1, 0},
	};

	int colCount, rowCount;
	float gridWidth = 6f;

	// Use this for initialization
	void Start () 
	{
		
		colCount = board.GetLength(0);
		rowCount = board.GetLength(1);

		print(colCount);
		print(board.Length);

		this.transform.localScale = new Vector3(colCount*gridWidth, 1, rowCount*gridWidth);

		// place people there
		for(int r = 0; r < rowCount; r++)
		{
			for(int c = 0; c < colCount; c++)
			{
				if(board[r,c] == 1)
				{
					GameObject g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/Person"));
					g.transform.position = new Vector3(gridWidth*r-(rowCount/2*gridWidth), 
																						 1f, gridWidth*c-(colCount/2*gridWidth));
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
