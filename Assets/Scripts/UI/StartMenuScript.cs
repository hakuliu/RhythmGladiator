using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMenuScript : MonoBehaviour
{
	public Button newGameButton;
	public Button continueButton;
	public Button optionsButton;
	public Button quitButton;

	public void QuitGame() {
		Application.Quit ();
	}
	public void NewGame() {
		Application.LoadLevel (1);
	}
}

