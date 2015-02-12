﻿using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System.Linq;

using System.Collections.Generic;
using System.IO;

public class Level1 : MonoBehaviour {

	public Text percentageComplete;
	public int percentage = 0;
	public Text popupText;
	public Button popupReset;
	public Button popupContinue;
	public GameObject winPanel;
	int totalCount, partialCount = 0;
	private bool started, hasActivated;
	List<Person> toBeActivated;
	float startTime;
	float maxSec = 0.5f;
	public Dictionary <int, string> typeToNames = new Dictionary<int, string>();
	public Dictionary <string, int> namesToType = new Dictionary<string, int>();
	public DropDown dropDownMenu;
	int curLevel;
	int maxLevel = 6;

	// for tutorials
	GameObject whiteBackground, normalTutorial, blogTutorial, granTutorial, bfTutorial, canvas, credit;

	// for UI
	private Rect barPosition, maxBarPosition;
	private float barWidth;
	private const float maxBarWidth = 195f;
	private const float barHeight = 16f;
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

	
	public GameObject[,] gameObjectBoard;
	public GameObject[,] gridBoard;
	List<int> playerPieces;
	int currentPlayerPieceIndex = 0;

	public int height, width;
	float gridWidth;

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
		
		barWidth = percentage;
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

	void LoadNext(){
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


	void LoadLevelNumber(int l)
	{
		
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
			case 4:
				whiteBackground.GetComponent<SpriteRenderer>().enabled = true;
				bfTutorial.GetComponent<SpriteRenderer>().enabled = true;
				canvas.GetComponent<Canvas>().enabled = false;
				break;
			default:
				break;
		}

		dropDownMenu.destroyButtons ();
		string filename = "Assets/Resources/Levels/Level" + l.ToString() + ".txt";
		StreamReader stream = new StreamReader(filename);
		bool isFirst = true;

		setupBoard = new List<List<int>>();
		setupPlayerPieces = new List<int>();

		// read my text file
	  while(!stream.EndOfStream)
	  {
			string line = stream.ReadLine( );

			// get placable pieces
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
   		// get the board
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

	   // after reading in data, generate the board
	   ResetPieces();
	  // GenerateOptionsAndInventory ();
		dropDownMenu.createButtons ();
	   
   	
	}

	// call if win
	void WinLevel(){
		winPanel.SetActive (true);

		// check if player has won
		if(percentage < 1)
		{
			popupContinue.interactable = false;
			audios[7].Play();
			audios[9].Stop();
			audios[10].Stop();
			audios[11].Stop();
		}
		else
		{
			popupContinue.interactable = true;
			audios[8].Play();
			audios[9].Stop();
			audios[10].Stop();
			audios[11].Stop();
		}

		popupText.text = percentage.ToString() + "%";
	
	}


	void ResetPieces()
	{

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
				totalCount++;
			}

		}

		GenerateOptionsAndInventory ();
		dropDownMenu.resetButtons ();

	}

	void ButtonPressedSound()
	{
		audios[0].Play();
	}

	void PickUpPieceSound()
	{
		audios[1].Play();
	}

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
																					 2.4f, gridWidth*c-(width/2*gridWidth));
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
			}
		}

	}

	void CalculatePercentage(){
		GameObject[] persons = GameObject.FindGameObjectsWithTag ("person");
		int c = 0;
		int total = persons.Length;

		foreach (GameObject p in persons) {
			if(p.GetComponent<Person>().activated){
				c++;
			}
		}
		float percent = ((float)c / (float)total) * 100f;
		percentage = (int)percent;
		percentageComplete.text = (int)percent + "%";
	}

	// player clicked somewhere, figure out what to do
	void CheckClick()
	{

		if(Input.GetMouseButtonDown(0) && whiteBackground.GetComponent<SpriteRenderer>().enabled)
		{
			if(credit.GetComponent<SpriteRenderer>().enabled)
			{
				EndGame();
			}
			else if(normalTutorial.GetComponent<SpriteRenderer>().enabled)
			{
				normalTutorial.GetComponent<SpriteRenderer>().enabled = false;
				blogTutorial.GetComponent<SpriteRenderer>().enabled = true;
			}
			else
			{

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
		GameObject[] persons = GameObject.FindGameObjectsWithTag ("person");

		/*float percent = 0f;

		if (totalCount > 0) {
			percent = ((float)partialCount/(float)totalCount) * 100f;
		} 

		percentageComplete.text = "Percent Covered: " + (int)percent + "%";*/
		CalculatePercentage ();


		// player cannot place anymore
		if(hasActivated)
			return;

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

				// from the current selection determine the current type of piece to place
				// and if you have more than 0 of it to place
				currentPlayerPieceIndex = playerPieces.IndexOf(dropDownMenu.selection);
				//print (dropDownMenu.selection);
				//print (currentPlayerPieceIndex);

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

					audios[2].Play();
				}

			}
			// activate piece
			else
			{

				if(gameObjectBoard[r,c] != null)
				{
					Person p = gameObjectBoard[r,c].GetComponent<Person>();

					// no activate dad
					if(p is Dad)
						return;

					gridBoard[r,c].GetComponent<Person>().Activate();

					if(p != null){
						//GameObject.FindGameObjectWithTag("normalButton").GetComponent<Text>().text = "Normal Person: 0";
						toBeActivated = gameObjectBoard[r,c].GetComponent<Person>().Activate();
						partialCount++;

						startTime = Time.time;
						started = true;
						hasActivated = true;

						//play first piece
						if(p is Normal) audios[3].Play();
						else if (p is Blogger) audios[4].Play();
						else if (p is Grandma) audios[5].Play();
						else if (p is BestFriend) audios[6].Play();

					}else{
						//print ("null person");
					}
				}
			}
		}
	}


	void CheckAndActivate(){
		//print ("Hello");
		List<Person> accumulator = new List<Person> ();
		foreach (Person p in toBeActivated) {


			if(!p.activated){

				//ActivatioN SouNd
				if(p is Normal) audios[3].Play();
				else if (p is Blogger) audios[4].Play();
				else if (p is Grandma) audios[5].Play();
				else if (p is BestFriend) audios[6].Play();

				//print (p.GetInstanceID());
				partialCount++;
				accumulator.AddRange(p.Activate());
				int rg = p.GetComponent<Person>().y;
				int cg = p.GetComponent<Person>().x;
				if(gridBoard[rg,cg] != null)
					gridBoard[rg,cg].GetComponent<Person>().Activate();
			}

		}
		if (accumulator.Count == 0) {
			// no more cascade, stop
			// popup?
			started = false;
			WinLevel();
		} else {
			toBeActivated = accumulator.Distinct().ToList();
			// get rid of duplicates
		}
		startTime = Time.time;
	}


  void DrawPercentBar() 
  {
		if(barWidth < 10f)
  		return;

	 	barPosition = new Rect(12f, 18f, barWidth, barHeight);
   	Texture2D texture = new Texture2D(1, 1);
   	texture.SetPixel(0,0, blueColor);
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		GUI.Box(barPosition, GUIContent.none);


		if(winPanel.active)
		{
			barPosition = new Rect(190f, 194f, barWidth*1.34f, 34f);
	   	texture = new Texture2D(1, 1);
	   	
	   	texture.SetPixel(0,0, blueColor);
			texture.Apply();
			GUI.skin.box.normal.background = texture;
			GUI.Box(barPosition, GUIContent.none);
		}
 	}


  void OnGUI()
  {
  	if(whiteBackground.GetComponent<SpriteRenderer>().enabled)
  		return;

  	barWidth = maxBarWidth * percentage / 100f;
  	DrawPercentBar();

   	//GUI.DrawTexture(outlinePosition, bar, ScaleMode.ScaleToFit, true, 10.0F);
    
  }

	
	// Update is called once per frame
	void Update () 
	{
		CheckClick();
		float curTime = Time.time;
		if (started && curTime >= startTime+maxSec) {
			CheckAndActivate();

		}
	}
}
