using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject optionsMenu;

	void Start() {
		MainMenuScene ();
	}

	public void LoadScene(string name) {
		SceneManager.LoadScene (name);
	}

	public void MainMenuScene() {
		mainMenu.SetActive (true);
		optionsMenu.SetActive (false);
	}

	public void OptionsScene() {
		mainMenu.SetActive (false);
		optionsMenu.SetActive (true);
	}

	public void SaveOptions() {
		MainMenuScene ();
	}

	public void CancelOptions() {
		MainMenuScene ();
	}

	public void ExitGame() {
		Application.Quit ();
	}
}
