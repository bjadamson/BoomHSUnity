using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuItemRename : MonoBehaviour {
	[SerializeField] private MenuItemManager menuManager;
	[SerializeField] private ChatManager chatManager;
	[SerializeField] private GameObject renamePanel;

	private GameObject previouslySelected;

	public void renameChannel() {
		chatManager.renameRightClickedTab (menuManager.renameFieldText());
	}

	public void hideHighlight() {
		renamePanel.SetActive (false);
	}

	public void showHighlight() {
		renamePanel.SetActive (true);
	}

	public void hideTarget() {
	}

	public void showTarget() {
	}
}