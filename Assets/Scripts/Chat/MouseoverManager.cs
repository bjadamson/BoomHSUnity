using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseoverManager : MonoBehaviour {
	[SerializeField] private TextPanelManager panelManager;
	[SerializeField] private TabManager tabManager;
	[SerializeField] private InputField inputField;

	private int activePanelId = 0;

	void Start() {
		updatePlaceholderText ();
	}
		
	public void onTabMouseOverEnter(Tabs tab, int panelId) {
		if (inputField.text.Length > 0) {
			// We don't do anything on mouse-over if the user has input any text.
			return;
		}
		tabManager.mouseOverTabEnter (tab);
		panelManager.showButNotMakeActive (panelId);
	}

	public void onTabMouseOverExit(Tabs tab, int panelId) {
		tabManager.mouseOverTabExit (tab);

		if (panelId != activePanelId) {
			panelManager.makePanelActive (activePanelId);
		}
	}

	public void onTabMouseClicked(Tabs tab, int panelId) {
		activePanelId = panelId;

		tabManager.mouseClickedOnTab (tab);
		panelManager.makePanelActive (panelId);
		updatePlaceholderText ();

		// After we select the tab, move focus to back to the input field if it's active (user is in input mode)
		if (inputField.IsActive()) {
			inputField.Select ();
		}
	}

	void updatePlaceholderText () {
		string text = tabManager.getActiveTabText ();
		inputField.placeholder.GetComponent<Text> ().text = text.ToLower() + "...";
	}
}
