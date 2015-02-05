using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
	int[] setupPlayerPieces = new int[1]{1};

	

	// contains all the pieces, in the right coordinates
	GameObject[,] gameObjectBoard;
	List<int> playerPieces;
	int currentPlayerPieceIndex = 0;

	int height, width;
	float gridWidth;

	// Use this for initialization
	void Start () 
	{
		
		height = setupBoard.GetLength(0);
		width = setupBoard.GetLength(1);
		gridWidth = 30f/(float)height;

		gameObjectBoard = new GameObject[height, width];
		playerPieces = new List<int>(setupPlayerPieces);

		//this.transform.localScale = new Vector3(height*gridWidth, 1, width*gridWidth);

		// place people there
		for(int c = 0; c < width; c++)
		{
			for(int r = 0; r < height; r++)
			{

				AddPieceToBoard(setupBoard[r,c], r, c);
			}
		}

	}



	void AddPieceToBoard(int i, int r, int c)
	{
		GameObject g = null;
		if(i == 0)	// normal person
		{

			g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/EmptySpace"));
			g.transform.position = new Vector3(gridWidth*c-(height/2*gridWidth), 
																				 0.05f, gridWidth*r-(width/2*gridWidth));
			g.transform.localScale = new Vector3(0.95f*gridWidth, 1f, 0.95f*gridWidth);
			
		}
		else if(i == 1)	// normal person
		{
			g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/Person"));
			g.transform.position = new Vector3(gridWidth*c-(height/2*gridWidth), 
																				 1f, gridWidth*r-(width/2*gridWidth));
		}

		gameObjectBoard[r,c] = g;
	}

	
	// Update is called once per frame
	void Update () 
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
		{
			if(hit.collider.tag.Equals("emptySpace") && playerPieces.Count != 0)
			{
				// get and remove player's piece
				int currentPiece = playerPieces[currentPlayerPieceIndex];
				playerPieces.RemoveAt(currentPlayerPieceIndex);

				// calculate this piece's index
				int r = (int)((hit.transform.position.x+(height/2*gridWidth))/gridWidth);
				int c = (int)((hit.transform.position.z+(width/2*gridWidth))/gridWidth);

				AddPieceToBoard(currentPiece, r, c);
			}
		}
	}
}
