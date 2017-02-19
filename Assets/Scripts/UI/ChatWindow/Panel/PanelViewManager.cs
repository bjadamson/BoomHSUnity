using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ui.chat_window.scroll;

namespace ui
{
	namespace chat_window
	{
		public class PanelViewManager : MonoBehaviour
		{
			[SerializeField] private ScrollViewManager scrollViewManager;
			[SerializeField] private GameObject panelAnchor;

			private IList<PanelView> panes = new List<PanelView> ();
			private PanelView activePane;
			private PanelView mousedOverPane;

			public void addPane (PanelView panel)
			{
				panes.Add (panel);

				if (!activePane) {
					activePane = panel;
					setScrollViewRectToActivePaneRect ();
				}
			}

			public void showGeneral ()
			{
				makePanelActive (0);
			}

			public void addEntry (string value)
			{
				activePane.addEntry (value);
			}

			public void showButNotMakeActive (int id)
			{
				mousedOverPane = panes [id];
				hideAllPanels ();
				show (mousedOverPane);

				setScrollViewRect (mousedOverPane.GetComponent<RectTransform> ());
			}

			public void makePanelActive (int id)
			{
				activePane = panes [id];
				refresh ();
			}

			public void removePanel (PanelView panel)
			{
				this.panes.Remove (panel);
				if (activePane == panel) {
					makePanelActive (0);
				} else {
					// only in else because we already refresh() inside makePanelActive()
					refresh ();
				}
			}

			public void setPanelBgColor (Color color)
			{
				panelAnchor.GetComponent<Image> ().color = color;
			}

			public void setPanelTextColor (Color color)
			{
				activePane.setTextColor (color);
			}

			private void refresh ()
			{
				hideAllPanels ();
				show (activePane);
				setScrollViewRectToActivePaneRect ();
			}

			private void hideAllPanels ()
			{
				foreach (PanelView pane in panes) {
					hide (pane);
				}
			}
				
			void setScrollViewRectToActivePaneRect ()
			{
				setScrollViewRect(activePane.GetComponent<RectTransform> ());
			}

			private void setScrollViewRect(RectTransform rect) {
				scrollViewManager.setContentRect (rect);
			}

			private static void hide (PanelView panel)
			{
				panel.gameObject.SetActive (false);
			}

			private static void show (PanelView panel)
			{
				panel.gameObject.SetActive (true);
			}
		}
	}
}