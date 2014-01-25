using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// State

	private enum menuState
	{
		main, play, join, loading, error, hidden
	}

	private menuState curMenuState = menuState.main;

	private const int DEFAULT_PORT = 5552;

	private string titleString = "Game Title";
	private string playString = "Play";
	private string makeServerString = "Create Server";
	private string joinServerString = "Join Game";
	private string quitString = "Exit";
	private string backString = "Back";
	private string loadingString = "Loading...";
	private string errorString = "Error";
	private string acceptString = "OK";

	private string inputIPString = "";

	private float buttonWidth = 90.0f;
	private float buttonHeight = 20.0f;
	private float buttonLeft = (Screen.width/2) - (90.0f/2);
	private float buttonTop = Screen.height/2 - (20.0f/2);

	void OnGUI(){

		switch(curMenuState)
		{
		case menuState.main:
			// Draw Box
			GUI.Label(new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight), titleString);

			// Start
			if (GUI.Button (new Rect (buttonLeft, buttonTop + buttonHeight, buttonWidth, buttonHeight), playString)) {
				// Launch
				curMenuState = menuState.play;
			}
			// Exit
			if (GUI.Button (new Rect (buttonLeft, buttonTop + buttonHeight * 2.0f, buttonWidth, buttonHeight), quitString)) {
				// This code is executed when the Button is clicked
			}

			break;

		case menuState.play:
			// Draw Box
			GUI.Label(new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight), titleString);
			
			// Start
			if (GUI.Button (new Rect (buttonLeft, buttonTop + buttonHeight, buttonWidth, buttonHeight), joinServerString)) {
				// Launch
				curMenuState = menuState.join;
			}

			if (GUI.Button (new Rect (buttonLeft, buttonTop + buttonHeight * 2.0f, buttonWidth, buttonHeight), makeServerString)) {
				// This code is executed when the Button is clicked
				buildServer();
			}
			// Exit
			if (GUI.Button (new Rect (buttonLeft, buttonTop + buttonHeight * 3.0f, buttonWidth, buttonHeight), backString)) {
				// This code is executed when the Button is clicked
				curMenuState = menuState.main;
			}
			break;

		case menuState.join:
			// Draw Box
			GUI.Label(new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight), titleString);

			// Input box
			inputIPString = GUI.TextField(new Rect(buttonLeft, buttonTop + buttonHeight, buttonWidth, buttonHeight), inputIPString, 25);
			// Start
			if(GUI.Button (new Rect (buttonLeft, buttonTop + buttonHeight * 2.0f, buttonWidth, buttonHeight), joinServerString)) {
				// This code is executed when the Button is clicked
				curMenuState = menuState.loading;
				joinServer( inputIPString );
			}
			// Exit
			if (GUI.Button (new Rect (buttonLeft, buttonTop + buttonHeight * 3.0f, buttonWidth, buttonHeight), backString)) {
				// This code is executed when the Button is clicked
				curMenuState = menuState.main;
			}
			break;

		case menuState.hidden:
		break;
		case menuState.loading:
			GUI.Label(new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight), loadingString);
			break;
		
		case menuState.error:
			// Draw Box
			GUI.Label(new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight), errorString);
			if(GUI.Button (new Rect (buttonLeft, buttonTop + buttonHeight * 1.0f, buttonWidth, buttonHeight), acceptString)) {
				// This code is executed when the Button is clicked
				curMenuState = menuState.main;
			}
		break;
		}
	}

	
	// Networking 
	
	void buildServer()
	{
		Network.InitializeServer(2,5552, true);
		string levelName = "Scene_Test";

		//Application.LoadLevel(levelName);

		// TODO: Add a fail case
	}
	void joinServer( string inIPAddress )
	{
		// Validate IP
		try
		{
			NetworkConnectionError error = Network.Connect( inIPAddress,DEFAULT_PORT);
			if( error != NetworkConnectionError.NoError)
			{

			}
			else
			{
				// Connected
			}
		}
		catch(System.Exception e)// NetworkConnectionError
		{
			errorString = "Failed to Connect to Server";
			curMenuState = menuState.error;
		}
	}

	void OnFailedToConnect(NetworkConnectionError error) {
		//Debug.Log("Could not connect to server: " + error);
		errorString = "Failed to Connect to Server";
		curMenuState = menuState.error;
	}
}
