using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanelManager : MonoBehaviour {
	[SerializeField] private TextPanel[] panes;
	private TextPanel activePane;

	void Start() {
		showGeneral ();
		hideInactive ();

		// General is the default tab to open.
		activePane = getGeneralPane();
	}

	public void showGeneral() {
		show (getGeneralPane());
	}

	public void replaceChatEntry(string value) {
		activePane.replaceChatEntry (value);
	}

	public void showPanel(int id) {
		activePane = panes [id];
		hideInactive ();

		show (panes[id]);
	}

	#region Private Methods
	private void hideInactive() {
		foreach (TextPanel pane in panes) {
			hide(pane);
		}
	}
	#endregion

	private TextPanel getGeneralPane() {
		return panes [0];
	}

	#region Private Static methods
	private static void hide(TextPanel panel) {
		panel.gameObject.SetActive (false);
	}

	private static void show(TextPanel panel) {
		panel.gameObject.SetActive (true);
	}
	#endregion
}