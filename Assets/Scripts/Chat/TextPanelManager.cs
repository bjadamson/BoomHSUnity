using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanelManager : MonoBehaviour {
	[SerializeField] private ScrollBackgroundAndHandle scrollBackground;

	private IList<TextPanel> panes = new List<TextPanel> ();
	private TextPanel activePane;
	private TextPanel mousedOverPane;

	public void addPane(TextPanel panel) {
		panes.Add (panel);

		if (!activePane) {
			activePane = panel;
			scrollBackground.GetComponent<ScrollRect>().content = activePane.GetComponent<RectTransform>();
		}
	}

	public void showGeneral() {
		show (getGeneralPane());
	}

	public void replaceChatEntry(string value) {
		activePane.addNewChatEntry (value);
	}

	public void showButNotMakeActive(int id) {
		mousedOverPane = panes [id];
		hideAllPanels ();
		show (mousedOverPane);

		scrollBackground.GetComponent<ScrollRect>().content = mousedOverPane.GetComponent<RectTransform>();
	}

	public void makePanelActive(int id) {
		activePane = panes [id];

		hideAllPanels ();
		show (activePane);
		scrollBackground.GetComponent<ScrollRect>().content = activePane.GetComponent<RectTransform>();
	}

	#region Private Methods
	private void hideAllPanels() {
		foreach (TextPanel pane in panes) {
			hide(pane);
		}
	}
	#endregion

	private TextPanel getGeneralPane() {
		return panes[0];
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