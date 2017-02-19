using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		public class TabManager : MonoBehaviour
		{
			[SerializeField] private ChatManager chatManager;
			[SerializeField] private ChatWindowFactory chatFactory;

			private IList<Tabs> tabs = new List<Tabs> ();
			private Tabs activeTab;
			private Tabs mousedOverTab;
			private Tabs rightClicked;
			private int activePanelId = 0;

			void Start ()
			{
				updatePlaceholderText ();
			}

			public void addTab (Tabs tab)
			{
				tabs.Add (tab);
			}

			public void selectGeneralTab ()
			{
				Debug.Assert (tabs [0]);
				selectTab (tabs [0]);
			}

			public void mouseOverTabEnter (Tabs tab, int panelId)
			{
				if (chatManager.userHasInputAnyCharacter ()) {
					// We don't do anything on mouse-over if the user has input any text.
					return;
				}
				mousedOverTab = tab;
				mousedOverTab.makeOpaque ();

				chatManager.showPanel (panelId);
			}

			public void mouseOverTabExit (Tabs tab, int panelId)
			{
				if (activeTab != tab) {
					mousedOverTab.makeTransparent ();
				}

				if (panelId != activePanelId) {
					chatManager.makePanelActive (activePanelId);
				}
			}

			public void mouseLeftClickedOnTab (Tabs tab, int panelId)
			{
				makeAllTransparentExcluding (tab);
				selectTab (tab);
				tab.makeOpaque ();

				activePanelId = panelId;

				chatManager.makePanelActive (panelId);
				updatePlaceholderText ();

				// After we select the tab, move focus to back to the input field if it's active (user is in input mode)
				chatManager.moveFocusToInputFieldIfActive ();
			}

			public void mouseRightClickedOnTab (Tabs tab, Vector2 pos)
			{
				rightClicked = tab;
				chatManager.addOptionsMenuUnderCursor (pos);
			}

			public Tabs rightClickedTab ()
			{
				return rightClicked;
			}

			public Tabs selectedTab ()
			{
				return activeTab;
			}

			public void removeTab (Tabs tab)
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

			private string getActiveTabText ()
			{
				return activeTab.text ();
			}

			private void makeAllTransparentExcluding (Tabs tab)
			{
				foreach (Tabs t in tabs) {
					if (t != tab) {
						t.makeTransparent ();
					}
				}
			}

			private void selectTab (Tabs tab)
			{
				activeTab = tab;
				activeTab.makeOpaque ();
			}

			private void updatePlaceholderText ()
			{
				string text = getActiveTabText ().ToLower ();
				string placeholderText = text + "...";
				chatManager.setPlaceholderText (placeholderText);
			}
		}

	}
}