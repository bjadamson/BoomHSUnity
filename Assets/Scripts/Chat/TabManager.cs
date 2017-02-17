using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour {
	private IList<TextPanel> panes = new List<TextPanel> ();
	private IList<Tabs> tabs = new List<Tabs> ();

	private Tabs selectedTab;
	private Tabs mousedOverTab;

	[SerializeField] private TextPanelManager panelManager;
	[SerializeField] private InputField inputField;
	private int activePanelId = 0;

	void Start() {
		updatePlaceholderText ();
	}

	public void addTab(Tabs tab) {
		tabs.Add (tab);
	}

	public void selectGeneralTab() {
		Debug.Assert (tabs [0]);
		selectTab(tabs[0]);
	}

	public void mouseOverTabEnter(Tabs tab, int panelId) {
		if (inputField.text.Length > 0) {
			// We don't do anything on mouse-over if the user has input any text.
			return;
		}
		mousedOverTab = tab;
		mousedOverTab.makeOpaque ();


		panelManager.showButNotMakeActive (panelId);
	}

	public void mouseOverTabExit(Tabs tab, int panelId) {
		if (selectedTab != tab) {
			mousedOverTab.makeTransparent ();
		}

		if (panelId != activePanelId) {
			panelManager.makePanelActive (activePanelId);
		}
	}

	public void mouseClickedOnTab(Tabs tab, int panelId) {
		makeAllTransparentExcluding (tab);
		selectTab (tab);
		tab.makeOpaque ();

		activePanelId = panelId;

		panelManager.makePanelActive (panelId);
		updatePlaceholderText ();

		// After we select the tab, move focus to back to the input field if it's active (user is in input mode)
		if (inputField.IsActive()) {
			inputField.Select ();
		}
	}

	private string getActiveTabText() {
		return selectedTab.text ();
	}

	private void makeAllTransparentExcluding(Tabs tab) {
		foreach (Tabs t in tabs) {
			if (t != tab) {
				t.makeTransparent ();
			}
		}
	}

	private void selectTab(Tabs tab) {
		selectedTab = tab;
		selectedTab.makeOpaque ();
	}

	private void updatePlaceholderText () {
		string text = getActiveTabText ();
		inputField.placeholder.GetComponent<Text> ().text = text.ToLower() + "...";
	}
}