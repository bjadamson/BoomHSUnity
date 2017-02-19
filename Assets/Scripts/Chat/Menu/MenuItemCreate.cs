using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemCreate : MonoBehaviour, MenuItem {
	[SerializeField] private MenuItemManager menuManager;
	[SerializeField] private ChatWindowFactory chatWindowFactory;
	[SerializeField] private GameObject chatOptionsWindow;

	void Start() {
		menuManager.addMenuItem (this);
	}

	public void createNewWindow() {
		chatWindowFactory.createDefaultChatWindow ("New Channel");
		chatOptionsWindow.gameObject.SetActive (false);
	}

	public void hideHighlight () {
	}

	public void showHighlight () {
	}

	public void hideTarget() {
	}
	public void showTarget() {
	}
}