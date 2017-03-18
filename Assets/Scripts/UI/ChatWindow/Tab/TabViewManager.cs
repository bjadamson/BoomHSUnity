using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		namespace tab
		{
			public class TabViewManager : MonoBehaviour
			{
				[SerializeField] private ChatManager chatManager;
				[SerializeField] private ChatWindowFactory chatFactory;

				private IList<TabView> tabs = new List<TabView> ();
				private TabView activeTab;
				private TabView mousedOverTab;
				private TabView rightClicked;
				private int activePanelId = 0;

				public void addTab (TabView tab)
				{
					tabs.Add (tab);
				}

				public void selectGeneralTab ()
				{
					Debug.Assert (tabs [0]);
					selectTab (tabs [0]);
				}

				public void mouseOverTabEnter (TabView tab, int panelId)
				{
					if (chatManager.userHasInputAnyCharacter ()) {
						// We don't do anything on mouse-over if the user has input any text.
						return;
					}
					mousedOverTab = tab;
					mousedOverTab.makeOpaque ();

					chatManager.showPanel (panelId);
				}

				public void mouseOverTabExit (TabView tab, int panelId)
				{
					if (activeTab != tab && mousedOverTab != null) {
						mousedOverTab.makeTransparent ();
					}

					if (panelId != activePanelId) {
						chatManager.makePanelActive (activePanelId);
					}
				}

				public void mouseLeftClickedOnTab (TabView tab, int panelId)
				{
					makeAllTransparentExcluding (tab);
					selectTab (tab);
					tab.makeOpaque ();

					activePanelId = panelId;

					chatManager.makePanelActive (panelId);
					chatManager.updateUserInputPlaceholderText (tab.name);

					// After we select the tab, move focus to back to the input field if it's active (user is in input mode)
					chatManager.moveFocusToInputFieldIfActive ();
				}

				public void mouseRightClickedOnTab (TabView tab, Vector2 pos)
				{
					rightClicked = tab;
					chatManager.addOptionsMenuUnderCursor (pos);
				}

				public TabView rightClickedTab ()
				{
					return rightClicked;
				}

				public TabView selectedTab ()
				{
					return activeTab;
				}

				public void removeTab (TabView tab)
				{
					this.tabs.Remove (tab);
					if (activeTab == tab) {
						activeTab = tabs [0];
						activePanelId = tabs [0].panelId;
					}
					if (mousedOverTab == tab) {
						mousedOverTab = null;
					}

					if (rightClicked == tab) {
						rightClicked = null;
					}

					// After removing a tab, we need to update all the panel id's set by the factory during construction.
					//
					// Also reset the factory's id counter to match the number of tabs in the list.
					// This way the next time the factory creates an instance, the panelId given internally to the tab will
					// be the next panel created by the factory.
					for (int i = 0; i < tabs.Count; ++i) {
						tabs [i].panelId = i;
					}
					chatFactory.idCounter = tabs.Count;
				}

				public void setRightClickedTabBgColor (Color color)
				{
					rightClicked.GetComponent<Image> ().color = color;
				}

				public string getActiveTabText ()
				{
					return activeTab.text ();
				}

				private void makeAllTransparentExcluding (TabView tab)
				{
					foreach (TabView t in tabs) {
						if (t != tab) {
							t.makeTransparent ();
						}
					}
				}

				private void selectTab (TabView tab)
				{
					activeTab = tab;
					activeTab.makeOpaque ();
				}
			}
		}
	}
}