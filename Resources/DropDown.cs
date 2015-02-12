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

	[SerializeField] GameObject buttonPrefab;

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
		/*
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
		*/
		topButton.GetComponentInChildren<Text> ().text = "Select a Piece";
		selection = -1;
	}

	public void destroyButtons(){
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
			button.GetComponentInChildren<Text>().text = options[i] + ": " + num;
			if(namesToType.ContainsKey(options[i])){
				int index = namesToType[options[i]];
				button.GetComponent<Button>().onClick.AddListener(() => setSelection (index));
				buttonsToName.Add (button, options[i]);
				nameToButtons.Add (options[i], button);
				//button.transform.parent = menuPanel;
				button.transform.SetParent(menuPanel, false);
			}
		}
	}

	void Update(){
		if (initialized) {
			createButtons();
			initialized = false;
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
			button.text = name + ": " + num;
			//print (button.GetComponentInChildren<Text>() != null);
			//print (name);
			//if(button != null){
			//	int num = inventory[name];
				//print (num);
				//print (button.GetComponentInChildren<Text>() != null);
				//button.GetComponentInChildren<Text>().text = name + ": " + num;
			//}
		}


	}

	void setSelection(int s){
		selection = s;
		topButton.GetComponentInChildren<Text>().text = typeToNames[s];

	}

}
