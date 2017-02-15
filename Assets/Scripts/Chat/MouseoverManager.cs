using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseoverManager : MonoBehaviour {
	[SerializeField] private ScrollBackground scrollBackground;
	[SerializeField] private TextPanelManager panelManager;
	[SerializeField] private TabManager tabManager;

	private int activePanelId = 0;
		
	public void onTabMouseOverEnter(Tabs tab, int panelId) {
		tabManager.mouseOverTabEnter (tab);

		panelManager.showPanel (panelId);
		scrollBackground.makeOpaque ();
	}

	public void onTabMouseOverExit(Tabs tab, int panelId) {
		tabManager.mouseOverTabExit (tab);

		if (panelId != activePanelId) {
			panelManager.showPanel (activePanelId);
		}
		scrollBackground.makeTransparent ();
	}

	public void onTabMouseClicked(Tabs tab, int panelId) {
		activePanelId = panelId;

		tabManager.mouseClickedOnTab (tab);
		panelManager.showPanel (panelId);
	}
}
