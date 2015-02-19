using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DropDown : MonoBehaviour {

	public int selection;
	public Dictionary <string, int> namesToType = new Dictionary<string, int>();
	public Dictionary <int, string> typeToNames = new Dictionary<int, string>();
	public Dictionary <string, int> inventory = new Dictionary<string, int>(); // set inventory first
	public Dictionary <GameObject, string> buttonsToName = new Dictionary<GameObject, string>();
	public Dictionary <string, GameObject> nameToButtons = new Dictionary<string, GameObject> ();
	public Dictionary<string, Text> nameToText = new Dictionary<string, Text>();
	public string[] options;
	public bool initialized = false;

	public Button topButton;

	[SerializeField] Transform menuPanel;
	
	ToggleGroup toggles;

	[SerializeField] GameObject buttonPrefab; // change to toggle

	AudioSource[] audios;

	/*
	 	0: empty square
	 	1: normal person
	 	2: blogger
	 	3: grandma
	 	4: best friend
	 	5: dad
	 */

	// Use this for initialization
	void Start () {
		// make list
		selection = -1;
		//toggles = menuPanel.GetComponent<ToggleGroup>();
		audios = GetComponents<AudioSource>(); 
	}

	public void destroyButtons(){
		selection = -1;
		for(int i = 0; i < options.Length; i++){
			GameObject b = nameToButtons[options[i]];
			Destroy(b);
		}
		nameToButtons = new Dictionary<string, GameObject> ();
		buttonsToName = new Dictionary<GameObject, string>();
		inventory = new Dictionary<string, int> ();
		nameToText = new Dictionary<string, Text> ();
	}

	public void createButtons(){
		for(int i = 0; i < options.Length; i++){
			GameObject button = (GameObject) Instantiate(buttonPrefab);
			Text t = button.GetComponentInChildren<Text>();
			nameToText.Add(options[i], t);
			int num = inventory[options[i]];
			button.GetComponentInChildren<Text>().text = num.ToString();
			if(namesToType.ContainsKey(options[i])){
				int index = namesToType[options[i]];
				button.GetComponent<Button>().onClick.AddListener(() => setSelection (index));
				buttonsToName.Add (button, options[i]);
				nameToButtons.Add (options[i], button);
				button.transform.SetParent(menuPanel, false);
			}
		}
	}

	void Update(){
		if (initialized) {
			createButtons();
			initialized = false;
		}

		if(selection != -1)
		{
			string name = typeToNames[selection];
			topButton.GetComponentInChildren<Text>().text = name + " " + inventory[name];
			if(inventory[name] <= 0){
				selection = -1;
			}
		}
	}

	public void resetButtons(){
		// reset text on buttons
		for (int i = 0; i < options.Length; i++) {
			if(nameToButtons.ContainsKey(options[i])){
				Text button = nameToText[options[i]];
				if(button != null){
					int num = inventory[options[i]];
					button.text = options[i] + ": " + num;
				}
			}
		}
	}

	public void buttonCount(){
		// change text and inventory count of current selection
		if (selection != -1) {
			string name = typeToNames [selection];
			inventory[name]--;	// check this
			Text button = nameToText[name];
			int num = inventory[name];
			button.text = num.ToString();
			
		}


	}

	void setSelection(int s){

		audios[0].Play();
		selection = s;
		string name = typeToNames[s];
		//topButton.GetComponentInChildren<Text>().text = name + " " + inventory[name];

	}

}
