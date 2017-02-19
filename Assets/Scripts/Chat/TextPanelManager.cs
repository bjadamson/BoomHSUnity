using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanelManager : MonoBehaviour {
	[SerializeField] private ScrollBackgroundAndHandle scrollBackground;
	[SerializeField] private GameObject panelAnchor;

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
		makePanelActive (0);
	}

	public void addEntry(string value) {
		activePane.addEntry (value);
	}

	public void showButNotMakeActive(int id) {
		mousedOverPane = panes [id];
		hideAllPanels ();
		show (mousedOverPane);

		scrollBackground.GetComponent<ScrollRect>().content = mousedOverPane.GetComponent<RectTransform>();
	}

	public void makePanelActive(int id) {
		activePane = panes [id];
		refresh ();
	}

	public void removePanel(TextPanel panel) {
		this.panes.Remove (panel);
		if (activePane == panel) {
			makePanelActive (0);
		} else {
			// only in else because we already refresh() inside makePanelActive()
			refresh ();
		}
	}

	public void setPanelBgColor(Color color) {
		panelAnchor.GetComponent<Image> ().color = color;
	}

	public void setPanelTextColor(Color color) {
		activePane.setTextColor(color);
	}

	#region Private Methods
	private void refresh() {
		hideAllPanels ();
		show (activePane);
		scrollBackground.GetComponent<ScrollRect>().content = activePane.GetComponent<RectTransform>();
	}

	private void hideAllPanels() {
		foreach (TextPanel pane in panes) {
			hide(pane);
		}
	}
	#endregion

	#region Private Static methods
	private static void hide(TextPanel panel) {
		panel.gameObject.SetActive (false);
	}

	private static void show(TextPanel panel) {
		panel.gameObject.SetActive (true);
	}
	#endregion
}