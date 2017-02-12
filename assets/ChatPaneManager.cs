using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatPaneManager : MonoBehaviour {
	[SerializeField] private GameObject general;
	[SerializeField] private GameObject whisper;
	[SerializeField] private GameObject party;
	[SerializeField] private GameObject guild;
	[SerializeField] private GameObject combatLog;

	private GameObject active;

	void Start() {
		show (general);
		hide (whisper);
		//hide (party);
		//hide (guild);
		//hide (combatLog);

		// General is the default tab to open.
		active = general;
	}

	public void makeActive(GameObject panel) {
		hide (active);
		active = panel;
		show (active);
	}

	private static void hide(GameObject panel) {
		panel.SetActive (false);
	}

	private static void show(GameObject panel) {
		panel.SetActive (true);
	}
}
