using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System.Linq;

using System.Collections.Generic;
using System.IO;

public class Level1 : MonoBehaviour {

	public Text percentageComplete;
	int totalCount, partialCount = 0;
	private bool started;
	List<Person> toBeActivated;

	public Dictionary <string, int> namesToType = new Dictionary<string, int>();
	public DropDown dropDownMenu;

	/*
	 	0: empty square
	 	1: normal person
	 	2: blogger
	 	3: grandma
	 	4: best friend
	 	5: dad
	 */
	// these are for serialization and resetting the board
	List<List<int>> setupBoard;
	List<int> setupPlayerPieces;

	
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

		dropDownMenu.options = new string[] {"Blogger", "Grandma"};
		dropDownMenu.inventory.Add("Blogger", 1);
		dropDownMenu.inventory.Add ("Grandma", 1);
		dropDownMenu.initialized = true;

	}

	void endGame(){
		Application.Quit ();
	}


	void LoadLevelFromTextFile(string filename)
	{
		
		 StreamReader stream = new StreamReader(filename);
		 bool isFirst = true;

		 setupBoard = new List<List<int>>();
		 setupPlayerPieces = new List<int>();

	   while(!stream.EndOfStream)
	   {
	   		string line = stream.ReadLine( );
	   		print(line + "\n");

	   		if(isFirst)
	   		{
	   			// add placable pieces
	   			for(int i = 0; i < line.Length; i++)
	   			{
	   				if(line[i] == ' ')
	   					continue;
	   				setupPlayerPieces.Add((int)(line[i] - '0'));
	   			}
	   			

	   			isFirst = false;
	   		}
	   		else
	   		{
	   			// add board
	   			List<int> row = new List<int>();
	   			// add placable pieces
	   			for(int i = 0; i < line.Length; i++)
	   			{
	   				if(line[i] == ' ')
	   					continue;
	   				row.Add((int)(line[i] - '0'));
	   			}

	   			setupBoard.Add(row);
	   		}
	   }

	   stream.Close( );  
   	
	}

	void resetPieces(){

		started = false;
		percentageComplete = GameObject.FindGameObjectWithTag ("percentage").GetComponent<Text> ();

		height = setupBoard.Count;
		width = setupBoard[0].Count;
		gridWidth = 30f/(float)height;

		// delete all the old stuff
		if(gameObjectBoard != null)
		{
			for(int c = 0; c < width; c++)
			{
				for(int r = 0; r < height; r++)
				{
					if(gameObjectBoard[r,c] != null)
						Destroy(gameObjectBoard[r,c]);
				}
			}
			gameObjectBoard = null;
		}
		playerPieces = null;
		dropDownMenu.inventory["Blogger"] = 1;
		dropDownMenu.inventory ["Grandma"] = 1;
		

		//dropDownMenu.resetButtons ();
		// make the board
		
		gameObjectBoard = new GameObject[height, width];
		playerPieces = new List<int>(setupPlayerPieces);

		// place people there
		for(int c = 0; c < width; c++)
		{
			for(int r = 0; r < height; r++)
			{
				AddPieceToBoard(setupBoard[r][c], r, c);
				totalCount++;
			}
		}
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
		LoadLevelFromTextFile("Assets/Resources/Levels/Level1.txt");
		SetupUI ();

		namesToType.Add ("EmptySpace", 0);
		namesToType.Add ("Normal", 1);
		namesToType.Add ("Blogger", 2);
		namesToType.Add ("Grandma", 3);
		namesToType.Add ("Best Friend", 4);
		namesToType.Add ("Dad", 5);

		resetPieces();
	}


	// add a single piece with ID i to gameObjectBoard[r,c]
	void AddPieceToBoard(int i, int r, int c)
	{

		GameObject g = null;

		switch (i)
		{
			case 0:	// empty square
				g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/EmptySpace"));
				g.transform.position = new Vector3(gridWidth*r-(height/2*gridWidth), 
																					 0.05f, gridWidth*c-(width/2*gridWidth));
				g.transform.localScale = new Vector3(0.95f*gridWidth, 1f, 0.95f*gridWidth);
				break;
			case 1:	// normal person
				g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/NormalPerson"));
				g.transform.position = new Vector3(gridWidth*r-(height/2*gridWidth), 
																					 1f, gridWidth*c-(width/2*gridWidth));
				break;
			case 2:
				g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/BloggerPerson"));
				g.transform.position = new Vector3(gridWidth*r-(height/2*gridWidth), 
																					 1f, gridWidth*c-(width/2*gridWidth));
				break;
			case 3:
				g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/GrandmaPerson"));
				g.transform.position = new Vector3(gridWidth*r-(height/2*gridWidth), 
																					 1f, gridWidth*c-(width/2*gridWidth));
				break;
			case 4:
				g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/BestFriendPerson"));
				g.transform.position = new Vector3(gridWidth*r-(height/2*gridWidth), 
																					 1f, gridWidth*c-(width/2*gridWidth));
				break;
			case 5:
				g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/DadPerson"));
				g.transform.position = new Vector3(gridWidth*r-(height/2*gridWidth), 
																					 1f, gridWidth*c-(width/2*gridWidth));
				break;
			default:
				break;
		}

		// add the piece to the board
		if(g != null)
			g.GetComponent<Person>().setPosition(r,c);
		gameObjectBoard[r,c] = g;
	}



	// player clicked somewhere, figure out what to do
	void CheckClick()
	{
		// update percentage covered, done quickly
		GameObject[] persons = GameObject.FindGameObjectsWithTag ("person");

		float percent = 0f;

		if (totalCount > 0) {
			percent = ((float)partialCount/(float)totalCount) * 100f;
		} 

		percentageComplete.text = "Percent Covered: " + (int)percent + "%";


		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
		{
			// calculate this piece's index
			int r = hit.transform.gameObject.GetComponent<Person>().y;
			int c = hit.transform.gameObject.GetComponent<Person>().x;

			// hit empty space, place down player's piece
			if(hit.collider.tag.Equals("emptySpace"))
			{
				// from the current selection determine the current type of piece to place
				// and if you have more than 0 of it to place
				currentPlayerPieceIndex = playerPieces.IndexOf(dropDownMenu.selection);
				print (dropDownMenu.selection);
				print (currentPlayerPieceIndex);

				if(playerPieces.Count != 0 && currentPlayerPieceIndex != -1)
				{

					// get and remove player's piece
					int currentPiece = playerPieces[currentPlayerPieceIndex];
					playerPieces.RemoveAt(currentPlayerPieceIndex);
					totalCount++;

					// change text on buttons
					dropDownMenu.buttonCount();

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
						//	print ("HI");
						}
						started = true;
					}else{
						//print ("null person");
					}
				}
			}
		}
	}


	void checkAndActivate(){
		//print ("Hello");
		List<Person> accumulator = new List<Person> ();
		foreach (Person p in toBeActivated) {
			if(!p.activated){
				//print (p.GetInstanceID());
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
