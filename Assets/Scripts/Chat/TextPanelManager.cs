using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanelManager : MonoBehaviour {
	[SerializeField] private ScrollView scrollView;

	private IList<TextPanel> panes = new List<TextPanel> ();
	private TextPanel activePane;

	public void addPane(TextPanel panel) {
		panes.Add (panel);

		if (!activePane) {
			activePane = panel;
		}
	}

	public void showGeneral() {
		show (getGeneralPane());
	}

	public void replaceChatEntry(string value) {
		activePane.addNewChatEntry (value);
	}

	public void showPanel(int id) {
		activePane = panes [id];
		hideInactive ();

		show (panes[id]);
		scrollView.GetComponent<ScrollRect>().content = activePane.GetComponent<RectTransform>();
	}

	public TextPanel getById(int id) {
		return panes [id];
	}

	#region Private Methods
	private void hideInactive() {
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