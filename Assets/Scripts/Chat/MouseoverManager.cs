using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseoverManager : MonoBehaviour {
	[SerializeField] private TextPanel generalPane;
	[SerializeField] private TextPanel[] panes;

	private TextPanel activePane;
	private Button activeButton;

	private Transform parent;

	void Start() {
		show (generalPane);
		hideNonActive ();

		// General is the default tab to open.
		activePane = generalPane;
	}

	public void makeActive(TextPanel panel) {
		hide(activePane);

		activePane = panel;
		show (activePane);
	}

	public bool isActive(TextPanel panel) {
		return panel == activePane;
	}

	public void replaceChatEntry(string value) {
		activePane.replaceChatEntry (value);
	}

	// TODO: huge abstraction leak. Should probably use an idex or something.
	public TextPanel getActive() {
		return activePane;
	}

	private void hideNonActive() {
		foreach (TextPanel pane in panes) {
			hide(pane);
		}
	}

	private static void hide(TextPanel panel) {
		panel.gameObject.SetActive (false);
	}

	private static void show(TextPanel panel) {
		panel.gameObject.SetActive (true);
	}
}
