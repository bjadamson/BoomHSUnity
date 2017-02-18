using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemCreateNewChannel : MonoBehaviour {
	[SerializeField] private ChatWindowFactory chatWindowFactory;
	[SerializeField] private GameObject chatOptionsWindow;

	public void createNewWindow() {
		chatWindowFactory.createDefaultChatWindow ("New Channel");
		chatOptionsWindow.gameObject.SetActive (false);
	}
}