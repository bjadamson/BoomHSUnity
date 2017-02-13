using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseoverManager : MonoBehaviour {
	[SerializeField] private ScrollView scrollView;
	[SerializeField] private TextPanelManager panelManager;
	[SerializeField] private TabManager tabManager;

	void Start() {
		panelManager.showGeneral ();
		tabManager.selectGeneralTab ();
	}
		
	public void onTabMouseOverEnter(Tabs tab) {
		tabManager.mouseOverTabEnter (tab);
		scrollView.makeOpaque ();
	}

	public void onTabMouseOverExit(Tabs tab) {
		tabManager.mouseOverTabExit (tab);
		scrollView.makeTransparent ();
	}

	public void onTabMouseClicked(Tabs tab, int panelId) {
		tabManager.mouseClickedOnTab (tab);
		panelManager.showPanel (panelId);
	}
}
