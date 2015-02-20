using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System.Linq;

using System.Collections.Generic;
using System.IO;

public class Level1 : MonoBehaviour {

	public Text percentageComplete;
	public float percentage = 0f;
	public Text popupText;
	public Button popupReset;
	public Button popupContinue;
	public Sprite continueGraphicGray;
	public Sprite continueGraphic;
	public GameObject winPanel;
	int totalCount;
	private bool started, hasActivated;
	List<Person> toBeActivated;
	float startTime;
	float maxSec = 0.5f;
	public Dictionary <int, string> typeToNames = new Dictionary<int, string>();
	public Dictionary <string, int> namesToType = new Dictionary<string, int>();
	public DropDown dropDownMenu;
	public int curLevel;
	int maxLevel = 11;

	// for tutorials
	GameObject whiteBackground, normalTutorial, blogTutorial, granTutorial, bfTutorial, canvas, credit;
	GameObject barInside, bar2Inside;

	// for UI
	static Color blueColor = new Color(0.54f,0.61f,0.76f,1f);
	static Color redColor = new Color(0.86f,0.21f,0.14f,1f);

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

	// all
	public GameObject[,] gameObjectBoard;
	public GameObject[,] gridBoard;
	List<int> playerPieces;
	public int currentPlayerPieceIndex = 0;

	// size o the board
	public int height, width;
	float gridWidth;

	// all the audio
	AudioSource[] audios;



	// Use this for initialization
	void Start () 
	{	
		SetupUI ();

		namesToType.Add ("EmptySpace", 0);
		namesToType.Add ("Normal", 1);
		namesToType.Add ("Blogger", 2);
		namesToType.Add ("Grandma", 3);
		namesToType.Add ("Best Friend", 4);
		namesToType.Add ("Dad", 5);
		typeToNames.Add (1, "Normal");
		typeToNames.Add (2, "Blogger");
		typeToNames.Add (3, "Grandma");
		typeToNames.Add (4, "Best Friend");
		typeToNames.Add (5, "Dad");
		dropDownMenu.namesToType = namesToType;
		dropDownMenu.typeToNames = typeToNames;

		whiteBackground = GameObject.Find ("white");
		normalTutorial = GameObject.Find ("normalTutorial");
		blogTutorial = GameObject.Find ("blogTutorial");
		granTutorial = GameObject.Find ("granTutorial");
		bfTutorial = GameObject.Find ("bfTutorial");
		canvas = GameObject.Find ("Canvas");
		credit = GameObject.Find ("credit");

		audios = GetComponents<AudioSource>(); 

		curLevel = 1;
		LoadLevelNumber(curLevel);

		bar2Inside = canvas.transform.Find("WinPopup/bar2Inside").gameObject;
		barInside = canvas.transform.Find("barInside").gameObject;
	}

	void GenerateOptionsAndInventory(){
		Dictionary<string, int> count = new Dictionary<string, int> ();
		foreach (int t in playerPieces) {
			string nm = typeToNames[t];
			if(!count.ContainsKey(nm)){
				count.Add(nm, 1);
			}else{
				count[nm]++;
			}
		}
		dropDownMenu.inventory = count;
		dropDownMenu.options = count.Keys.ToArray ();
	}


	// load the next level, or the credits
	public void LoadNext(){
		winPanel.SetActive (false);
		if (curLevel == maxLevel) {

			canvas.GetComponent<Canvas>().enabled = false;
			credit.GetComponent<SpriteRenderer>().enabled = true;
			whiteBackground.GetComponent<SpriteRenderer>().enabled = true;
		} else {
			curLevel++;
			LoadLevelNumber (curLevel);
		}
		


	}

	void SetupUI()
	{
		Button quitButton = GameObject.Find ("Quit").GetComponent<Button>();
		Button resetButton = GameObject.Find ("Clear").GetComponent<Button>();
		//Button startButton = GameObject.Find ("Activate").GetComponent<Button>();

		//startButton.onClick.AddListener (ActivateRumor);
		quitButton.onClick.AddListener (EndGame);
		quitButton.onClick.AddListener (ButtonPressedSound);
		resetButton.onClick.AddListener (ResetPieces);
		resetButton.onClick.AddListener (ButtonPressedSound);

		popupReset.onClick.AddListener (ResetPieces);
		popupReset.onClick.AddListener (ButtonPressedSound);
		popupContinue.onClick.AddListener (LoadNext);
		popupContinue.onClick.AddListener (ButtonPressedSound);

		//GenerateOptionsAndInventory ();
		//dropDownMenu.createButtons ();

	}

	void EndGame()
	{

		Application.Quit ();
	}


	public void LoadLevelNumber(int l)
	{
		/*
		// changes the tutorial
		switch(l)
		{
			case 1:
				whiteBackground.GetComponent<SpriteRenderer>().enabled = true;
				normalTutorial.GetComponent<SpriteRenderer>().enabled = true;
				canvas.GetComponent<Canvas>().enabled = false;
				break;
			case 2:
				whiteBackground.GetComponent<SpriteRenderer>().enabled = true;
				granTutorial.GetComponent<SpriteRenderer>().enabled = true;
				canvas.GetComponent<Canvas>().enabled = false;
				break;
			case 3:
				audios[7].Stop();
				audios[8].Stop();
				audios[9].Stop();
				audios[10].Stop();
				audios[11].Play();
				break;
			case 4:
				whiteBackground.GetComponent<SpriteRenderer>().enabled = true;
				bfTutorial.GetComponent<SpriteRenderer>().enabled = true;
				canvas.GetComponent<Canvas>().enabled = false;
				break;
			default:
				break;
		}*/

		dropDownMenu.destroyButtons ();


		TextAsset txt = (TextAsset)Resources.Load("Levels/Level"+l.ToString(), typeof(TextAsset));
		bool isFirst = true;

		setupBoard = new List<List<int>>();
		setupPlayerPieces = new List<int>();

		string[] lines = txt.text.Split('\n');

		// read my text file
	  foreach (string line in lines)
	  {
			// get placable pieces
   		if(isFirst)
   		{
   			// add placable pieces
   			for(int i = 0; i < line.Length; i++)
   			{
   				if(line[i] == ' ' || line[i] == '\r' || line[i] == '\n')
   					continue;
   				setupPlayerPieces.Add((int)(line[i] - '0'));
   			}
   			

   			isFirst = false;
   		}
   		// get the board
   		else
   		{
   			// add board
   			List<int> row = new List<int>();
   			// add placable pieces
   			for(int i = 0; i < line.Length; i++)
   			{
   				if(line[i] == ' ' || line[i] == '\r' || line[i] == '\n')
   					continue;
   				row.Add((int)(line[i] - '0'));
   			}

   			setupBoard.Add(row);
   		}
	   }
	   //stream.Close( );

	  // after reading in data, generate the board
	  ResetPieces();
	  // GenerateOptionsAndInventory ();
		dropDownMenu.createButtons ();   	
	}

	// call if win
	void WinLevel(){
		winPanel.SetActive (true);

		// check if player has won
		if(percentage < 75f)
		{
			popupContinue.interactable = false;
			popupContinue.image.sprite = continueGraphicGray;
			audios[7].Play();
			audios[9].Stop();
			audios[10].Stop();
			audios[11].Stop();
		}
		else
		{
			popupContinue.interactable = true;
			popupContinue.image.sprite = continueGraphic;
			audios[8].Play();
			audios[9].Stop();
			audios[10].Stop();
			audios[11].Stop();
		}

		popupText.text = ((int)percentage).ToString() + "%";
	
	}


	void LevelMusicPlayer()
	{
		audios[7].Stop();
		audios[8].Stop();
		switch(curLevel)
		{
			case 1:
				audios[9].Play();
				audios[10].Stop();
				audios[11].Stop();
				break;
			case 2:
				audios[9].Stop();
				audios[10].Play();
				audios[11].Stop();
				break;
			case 3:
				audios[9].Stop();
				audios[10].Stop();
				audios[11].Play();
				break;
			case 4:
				audios[9].Play();
				audios[10].Stop();
				audios[11].Stop();
				break;
			case 5:
				audios[9].Stop();
				audios[10].Play();
				audios[11].Stop();
				break;
			case 6:
				audios[9].Stop();
				audios[10].Stop();
				audios[11].Play();
				break;
			default:
				break;
		}
	}


	void ResetPieces()
	{
		// play different music based on level
		LevelMusicPlayer();

		winPanel.SetActive (false);
		started = false;
		hasActivated = false;
		percentageComplete = GameObject.FindGameObjectWithTag ("percentage").GetComponent<Text> ();

		// recalculate the width and height
		height = setupBoard.Count;
		width = setupBoard[0].Count;
		gridWidth = 30f/(float)height;

		// delete all the old stuff
		if(gameObjectBoard != null)
		{
			for(int c = 0; c < gameObjectBoard.GetLength(1); c++)
			{
				for(int r = 0; r < gameObjectBoard.GetLength(0); r++)
				{
					if(gameObjectBoard[r,c] != null)
						Destroy(gameObjectBoard[r,c]);
					if(gridBoard[r,c] != null)
						Destroy(gridBoard[r,c]);
				}
			}
			gameObjectBoard = null;
			gridBoard = null;
		}
		playerPieces = null;


		dropDownMenu.inventory["Blogger"] = 1;
		dropDownMenu.inventory ["Grandma"] = 1;
		//dropDownMenu.resetButtons ();
		
		// make the board		
		gameObjectBoard = new GameObject[height, width];
		gridBoard = new GameObject[height, width];
		playerPieces = new List<int>(setupPlayerPieces);

		// place people there
		for(int c = 0; c < width; c++)
		{
			for(int r = 0; r < height; r++)
			{
				AddPieceToBoard(setupBoard[r][c], r, c);
			}

		}

		GenerateOptionsAndInventory ();
		dropDownMenu.resetButtons ();

	}

	void ButtonPressedSound() { audios[0].Play(); }
	void PickUpPieceSound() { audios[1].Play(); }

	void ActivateRumor(){
		GameObject r = GameObject.FindGameObjectWithTag ("rumor");
		if (r != null) {

			r.GetComponent<Rumor>().Infect ();
		}
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
				g.GetComponent<EmptySpace>().isActuallyEmpty = true;
				break;
			case 1:	// normal person
				g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/NormalPerson"));
				g.transform.position = new Vector3(gridWidth*r-(height/2*gridWidth), 
																					 2.4f, gridWidth*c-(width/2*gridWidth));
				break;
			case 2:
				g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/BloggerPerson"));
				g.transform.position = new Vector3(gridWidth*r-(height/2*gridWidth), 
																					 2.4f, gridWidth*c-(width/2*gridWidth));
				break;
			case 3:
				g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/GrandmaPerson"));
				g.transform.position = new Vector3(gridWidth*r-(height/2*gridWidth), 
																					 2.4f, gridWidth*c-(width/2*gridWidth));
				break;
			case 4:
				g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/BestFriendPerson"));
				g.transform.position = new Vector3(gridWidth*r-(height/2*gridWidth), 
																					 2.4f, gridWidth*c-(width/2*gridWidth));
				break;
			case 5:
				g = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/DadPerson"));
				g.transform.position = new Vector3(gridWidth*r-(height/2*gridWidth), 
																					 0.1f, gridWidth*c-(width/2*gridWidth));
				g.transform.localScale = new Vector3(0.95f*gridWidth, 1f, 0.95f*gridWidth);
				break;
			default:
				break;
		}

		// add the piece to the board
		if(g != null)
		{
			g.GetComponent<Person>().setPosition(r,c);
			gameObjectBoard[r,c] = g;
			
			// if this is not an empty space, place one under it
			if(i != 0)
			{
				GameObject b = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/EmptySpace"));
				b.GetComponent<Person>().setPosition(r,c);
				b.transform.position = new Vector3(gridWidth*r-(height/2*gridWidth), 
																							 0.05f, gridWidth*c-(width/2*gridWidth));
				b.transform.localScale = new Vector3(0.95f*gridWidth, 1f, 0.95f*gridWidth);
				b.GetComponent<EmptySpace>().isActuallyEmpty = false;
				gridBoard[r,c] = b;
				totalCount++;
			}
		}

	}


	// calculate percentage of completion
	void CalculatePercentage(){
		GameObject[] persons = GameObject.FindGameObjectsWithTag ("person");
		int c = 0;
		int total = persons.Length;

		// count up the people
		foreach (GameObject p in persons) {
			if(p.GetComponent<Person>().activated){
				c++;
			}
		}
		percentage = ((float)c / (float)total) * 100f;
		percentageComplete.text = ((int)percentage) + "%";
	}




	// player clicked somewhere, figure out what to do
	void CheckClick()
	{
		// it is tutorial, move past it
		if(Input.GetMouseButtonDown(0) && whiteBackground.GetComponent<SpriteRenderer>().enabled)
		{
			// if credit is showing, then end the game
			if(credit.GetComponent<SpriteRenderer>().enabled)
			{
				EndGame();
			}
			// the first tutorial
			else if(normalTutorial.GetComponent<SpriteRenderer>().enabled)
			{
				normalTutorial.GetComponent<SpriteRenderer>().enabled = false;
				blogTutorial.GetComponent<SpriteRenderer>().enabled = true;
			}
			else
			{
				// hide all tutorial things
				whiteBackground.GetComponent<SpriteRenderer>().enabled = false;
				normalTutorial.GetComponent<SpriteRenderer>().enabled = false;
				blogTutorial.GetComponent<SpriteRenderer>().enabled = false;
				granTutorial.GetComponent<SpriteRenderer>().enabled = false;
				bfTutorial.GetComponent<SpriteRenderer>().enabled = false;
				canvas.GetComponent<Canvas>().enabled = true;
			}
			return;
		}

		// update percentage covered, done quickly
		CalculatePercentage ();


		// player cannot place anymore
		if(hasActivated)
			return;

		// calculate which piece the player clicked
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
		{
			// calculate this piece's index
			int r = hit.transform.gameObject.GetComponent<Person>().y;
			int c = hit.transform.gameObject.GetComponent<Person>().x;

			// hit empty space, place down player's piece
			if(hit.collider.tag.Equals("emptySpace") && gridBoard[r,c] == null)
			{

				// place the selected piece
				currentPlayerPieceIndex = playerPieces.IndexOf(dropDownMenu.selection);

				if(playerPieces.Count != 0 && currentPlayerPieceIndex != -1)
				{
					// get and remove player's piece
					int currentPiece = playerPieces[currentPlayerPieceIndex];
					playerPieces.RemoveAt(currentPlayerPieceIndex);

					// change text on buttons
					dropDownMenu.buttonCount();

					// add the new piece to board
					AddPieceToBoard(currentPiece, r, c);

					// destroy the old white space
					Destroy(hit.collider);
					Destroy(hit.transform.gameObject);

					audios[2].Play();
				}

			}
			// activate piece
			else
			{
				if(gameObjectBoard[r,c] != null)
				{
					Person p = gameObjectBoard[r,c].GetComponent<Person>();

					// no activate no pieces
					if(p == null)
						return;

					// flash the white space
					gridBoard[r,c].GetComponent<Person>().Activate();

					// no activate dad
					if(p is Dad)
						return;

					// get everything else to be actiavted
					toBeActivated = gameObjectBoard[r,c].GetComponent<Person>().Activate();

					// restart timer, start the activation chain reaction
					startTime = Time.time;
					started = true;
					hasActivated = true;

					//play appropriate sound effect for the activated piece
					PlayPieceSound(p);
				}
			}
		}
	}

	// Play the sound to appropriate piece type
	void PlayPieceSound(Person p)
	{
		if(p is Normal) audios[3].Play();
		else if (p is Blogger) audios[4].Play();
		else if (p is Grandma) audios[5].Play();
		else if (p is BestFriend) audios[6].Play();
	}


	// take one step in the next activation wave
	void CheckAndActivate()
	{

		List<Person> accumulator = new List<Person> ();
		foreach (Person p in toBeActivated) {

			if(!p.activated){

				//ActivatioN SouNd
				PlayPieceSound(p);

				// get everyone this piece activates
				accumulator.AddRange(p.Activate());

				// activates the piece where this piece is located at
				int rg = p.GetComponent<Person>().y;
				int cg = p.GetComponent<Person>().x;
				if(gridBoard[rg,cg] != null)
					gridBoard[rg,cg].GetComponent<Person>().Activate();
			}

		}
		// no more cascade, stop
		if (accumulator.Count == 0) 
		{
			started = false;
			WinLevel();
		} 
		else 
		{
			// get rid of duplicates
			toBeActivated = accumulator.Distinct().ToList();
		}

		// reset activation timer
		startTime = Time.time;
	}

	// Update is called once per frame
	void Update () 
	{
		// check if player clicked
		CheckClick();

		// if activated, activate in waves on a timer
		if (started && Time.time >= startTime+maxSec) 
		{
			CheckAndActivate();
		}

		// update the progress bar
		barInside.GetComponent<RectTransform>().localScale = new Vector3(percentage/100f, 1f, 1f);
		bar2Inside.GetComponent<RectTransform>().localScale = new Vector3(percentage/100f, 1f, 1f);
	}
}
