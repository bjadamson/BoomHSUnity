using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatPaneManager : MonoBehaviour {
	[SerializeField] private GameObject generalPane;
	[SerializeField] private GameObject[] panes;

	private GameObject activePane;
	private Button activeButton;

	void Start() {
		show (generalPane);
		hideNonActive ();

		// General is the default tab to open.
		activePane = generalPane;
	}

	public void makeActive(GameObject panel) {
		hide(activePane);
		activePane = panel;
		show (activePane);
	}

	public bool isActive(GameObject panel) {
		return panel == activePane;
	}

	private void hideNonActive() {
		foreach (GameObject pane in panes) {
			hide(pane);
		}
	}

	private static void hide(GameObject panel) {
		panel.SetActive (false);
	}

	private static void show(GameObject panel) {
		panel.SetActive (true);
	}
}
