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
	int[,] setupBoard = new int[,]
	{
		{0, 0, 1, 0, 0},
		{0, 1, 0, 1, 0},
		{0, 0, 1, 0, 0},
		{0, 0, 0, 0, 1},
		{0, 0, 0, 1, 0},
	};

	// contains all the pieces, in the right coordinates
	GameObject[,] gameObjectBoard;

	int height, width;
	float gridWidth;

	// Use this for initialization
	void Start () 
	{
		
		height = setupBoard.GetLength(0);
		width = setupBoard.GetLength(1);
		gridWidth = this.transform.localScale.x/(float)height;

		print(gridWidth);

		gameObjectBoard = new GameObject[height, width];

		this.transform.localScale = new Vector3(height*gridWidth, 1, width*gridWidth);

		// place people there
		for(int c = 0; c < width; c++)
		{
			for(int r = 0; r < height; r++)
			{
				if(setupBoard[r,c] == 0)	// normal person
				{

					GameObject g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/EmptySpace"));
					g.transform.position = new Vector3(gridWidth*c-(height/2*gridWidth), 
																						 0.05f, gridWidth*r-(width/2*gridWidth));
					g.transform.localScale = new Vector3(0.95f*gridWidth, 1f, 0.95f*gridWidth);
					gameObjectBoard[r,c] = g;
				}
				else if(setupBoard[r,c] == 1)	// normal person
				{
					GameObject g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/Person"));
					g.transform.position = new Vector3(gridWidth*c-(height/2*gridWidth), 
																						 1f, gridWidth*r-(width/2*gridWidth));
					gameObjectBoard[r,c] = g;
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
