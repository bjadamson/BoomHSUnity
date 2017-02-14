using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseoverManager : MonoBehaviour {
	[SerializeField] private ScrollView scrollView;
	[SerializeField] private TextPanelManager panelManager;
	[SerializeField] private TabManager tabManager;

	private int activePanelId = 0;

	void Start() {
		//panelManager.showGeneral ();
		//tabManager.selectGeneralTab ();
	}
		
	public void onTabMouseOverEnter(Tabs tab, int panelId) {
		tabManager.mouseOverTabEnter (tab);

		panelManager.showPanel (panelId);
		scrollView.makeOpaque ();
	}

	public void onTabMouseOverExit(Tabs tab, int panelId) {
		tabManager.mouseOverTabExit (tab);

		if (panelId != activePanelId) {
			panelManager.showPanel (activePanelId);
		}
		scrollView.makeTransparent ();
	}

	public void onTabMouseClicked(Tabs tab, int panelId) {
		activePanelId = panelId;

		tabManager.mouseClickedOnTab (tab);
		panelManager.showPanel (panelId);
	}
}
