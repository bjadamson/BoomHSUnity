using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour {
	private IList<Tabs> tabs = new List<Tabs> ();
	private Tabs selectedTab;
	private Tabs mousedOverTab;

	public void addTab(Tabs tab) {
		tabs.Add (tab);
	}

	public void selectGeneralTab() {
		Debug.Assert (tabs [0]);
		selectTab(tabs[0]);
	}

	public void mouseOverTabEnter(Tabs tab) {
		mousedOverTab = tab;
		mousedOverTab.makeOpaque ();
	}

	public void mouseOverTabExit(Tabs tab) {
		if (selectedTab != tab) {
			mousedOverTab.makeTransparent ();
		}
	}

	public void mouseClickedOnTab(Tabs tab) {
		makeAllTransparentExcluding (tab);
		selectTab (tab);
		tab.makeOpaque ();
	}

	#region Private Methods
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
	#endregion
}