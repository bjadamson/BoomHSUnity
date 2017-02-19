using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemManager : MonoBehaviour {
	[SerializeField] private Image chatOverlayAlphaTransparencyPreview;
	private readonly IList<MenuItem> menuItems = new List<MenuItem> ();
	private MenuItem highlightedMenu = null;

	public void addMenuItem(MenuItem menuItem) {
		menuItems.Add (menuItem);
	}

	public void onPointerEnterMenuItem(MenuItem menuItem) {
		hideAllHighlights ();
		menuItem.showHighlight ();

		if (highlightedMenu == null) {
			menuItem.showTarget ();
		}
	}

	public void onPointerExitMenuItem(MenuItem menuItem) {
		menuItem.hideHighlight ();

		bool mouseLeavingSelectedMenu = menuItem == highlightedMenu;
		if (!mouseLeavingSelectedMenu) {
			menuItem.hideTarget ();
		}
	}

	public void onPointerClickMenuItem(MenuItem menuItem) {
		hideAllHighlights ();
		hideAllTargets ();

		highlightedMenu = menuItem;

		highlightedMenu.showHighlight ();
		highlightedMenu.showTarget ();
	}

	public void hideAllTargets() {
		foreach (MenuItem menu in menuItems) {
			menu.hideTarget ();
		}
	}

	public void setChatOverlayTransparencyPreview(float alpha) {
		Color color = chatOverlayAlphaTransparencyPreview.color;
		color.a = alpha;
		chatOverlayAlphaTransparencyPreview.color = color;
	}

	private void hideAllHighlights() {
		foreach (MenuItem menu in menuItems) {
			menu.hideHighlight ();
		}
	}
}