using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level1 : MonoBehaviour {

	public Text percentageComplete;

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
	public GameObject[,] gameObjectBoard;

	public int height, width;
	float gridWidth = 6f;

	void SetupUI(){
	}

	// Use this for initialization
	void Start () 
	{
		
		height = setupBoard.GetLength(0);
		width = setupBoard.GetLength(1);

		gameObjectBoard = new GameObject[height, width];

		this.transform.localScale = new Vector3(height*gridWidth, 1, width*gridWidth);

		// place people there
		for(int r = 0; r < width; r++)
		{
			for(int c = 0; c < height; c++)
			{
				if(setupBoard[r,c] == 1)	// normal person
				{
					GameObject g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/Person"));
					g.transform.position = new Vector3(gridWidth*r-(width/2*gridWidth), 
																						 1f, gridWidth*c-(height/2*gridWidth));
					// add position in matrix to person
					g.GetComponent<Person>().setPosition(r,c);

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
