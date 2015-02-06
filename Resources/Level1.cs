using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System.Linq;

using System.Collections.Generic;

public class Level1 : MonoBehaviour {

	public Text percentageComplete;
	int totalCount, partialCount = 0;
	private bool started;
	List<Person> toBeActivated;

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
	public int[] setupPlayerPieces = new int[1]{1};

	
	public GameObject[,] gameObjectBoard;
	List<int> playerPieces;
	int currentPlayerPieceIndex = 0;

	public int height, width;
	float gridWidth;

	void SetupUI(){
		Button quitButton = GameObject.Find ("Quit").GetComponent<Button>();
		Button resetButton = GameObject.Find ("Clear").GetComponent<Button>();
		//Button startButton = GameObject.Find ("Activate").GetComponent<Button>();

		//startButton.onClick.AddListener (activateRumor);
		quitButton.onClick.AddListener (endGame);
		resetButton.onClick.AddListener (resetPieces);

	}

	void endGame(){
		Application.Quit ();
	}

	void resetPieces(){
		//TODO
	}

	void activateRumor(){
		GameObject r = GameObject.FindGameObjectWithTag ("rumor");
		if (r != null) {

			r.GetComponent<Rumor>().Infect ();
		}
	}

	// Use this for initialization
	void Start () 
	{
		SetupUI ();
		started = false;
		percentageComplete = GameObject.FindGameObjectWithTag ("percentage").GetComponent<Text> ();

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
				totalCount++;
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
			g.GetComponent<Person>().setPosition(r,c);
		}


		gameObjectBoard[r,c] = g;
	}




	void CheckClick()
	{
		// update percentage covered, done quickly
		GameObject[] persons = GameObject.FindGameObjectsWithTag ("person");

		float percent = 0f;

		if (totalCount > 0) {
			percent = (partialCount/totalCount) * 100;
		} 

		percentageComplete.text = "Percent Covered: " + (int)percent + "%";


		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
		{
			// calculate this piece's index
			int r = (int)((hit.transform.position.z+(width/2*gridWidth))/gridWidth);
			int c = (int)((hit.transform.position.x+(height/2*gridWidth))/gridWidth);

			// hit empty space, place down player's piece
			if(hit.collider.tag.Equals("emptySpace"))
			{
				if(playerPieces.Count != 0)
				{
					// get and remove player's piece
					int currentPiece = playerPieces[currentPlayerPieceIndex];
					playerPieces.RemoveAt(currentPlayerPieceIndex);
					totalCount++;
					AddPieceToBoard(currentPiece, r, c);

					Destroy(hit.collider);
					Destroy(hit.transform.gameObject);
				}
			}
			// activate piece
			else
			{

				if(gameObjectBoard[r,c] != null && !started)
				{
					Person p = gameObjectBoard[r,c].GetComponent<Person>();
					if(p != null){
						//GameObject.FindGameObjectWithTag("normalButton").GetComponent<Text>().text = "Normal Person: 0";
						toBeActivated = gameObjectBoard[r,c].GetComponent<Person>().Activate();
						partialCount++;
						// sanity check
						foreach(Person per in toBeActivated){
							print ("HI");
						}
						started = true;
					}else{
						print ("null person");
					}
				}
			}
		}
	}


	void checkAndActivate(){
		print ("Hello");
		List<Person> accumulator = new List<Person> ();
		foreach (Person p in toBeActivated) {
			if(!p.activated){
				print (p.GetInstanceID());
				partialCount++;
				accumulator.AddRange(p.Activate());
			}

		}
		if (accumulator.Count == 0) {
			// no more cascade, stop
			// popup?
			started = false;
		} else {
			toBeActivated = accumulator.Distinct().ToList();
			// get rid of duplicates
		}
	}

	
	// Update is called once per frame
	void Update () 
	{
		CheckClick();
		if (started) {
			checkAndActivate();
		}
	}
}
