using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatPaneManager : MonoBehaviour {
	[SerializeField] private ChatTextPanel generalPane;
	[SerializeField] private ChatTextPanel[] panes;

	private ChatTextPanel activePane;
	private Button activeButton;

	void Start() {
		show (generalPane);
		hideNonActive ();

		// General is the default tab to open.
		activePane = generalPane;
	}

	public void makeActive(ChatTextPanel panel) {
		hide(activePane);
		activePane = panel;
		show (activePane);
	}

	public bool isActive(ChatTextPanel panel) {
		return panel == activePane;
	}

	public void replaceChatEntry(string value) {
		activePane.replaceChatEntry (value);
	}

	private void hideNonActive() {
		foreach (ChatTextPanel pane in panes) {
			hide(pane);
		}
	}

	private static void hide(ChatTextPanel panel) {
		panel.gameObject.SetActive (false);
	}

	private static void show(ChatTextPanel panel) {
		panel.gameObject.SetActive (true);
	}
}
