using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour {
	[SerializeField] private bool sendAllMessagesToGeneral = true;
	[SerializeField] private InputFieldManager inputFieldManager;
	[SerializeField] private TextPanelManager panelManager;
	[SerializeField] private TabManager tabManager;
	[SerializeField] private GameObject chatWindow;

	private ChatWindowFactory chatFactory;
	private ChatWindowList channelList;

	void Start() {
		channelList = new ChatWindowList (panelManager, tabManager, inputFieldManager);
		chatFactory = GetComponent<ChatWindowFactory> ();
		chatFactory.setChannelList (channelList);
		chatFactory.createDefaultWindow ();
	}

	public void createChatWindow(string channelName) {
		chatFactory.createDefaultChatWindow (channelName);
	}

	public bool userHasInputAnyCharacter() {
		return inputFieldManager.isEmpty ();
	}

	public void moveFocusToInputFieldIfActive() {
		inputFieldManager.selectOnlyIfActive ();
	}

	public string placeholderText() {
		return inputFieldManager.getPlaceholderText ();
	}

	public void setPlaceholderText(string value) {
		inputFieldManager.setPlaceholderText(value + "...");
	}

	public void showPanel(int panelId) {
		panelManager.showButNotMakeActive (panelId);
	}

	public void makePanelActive(int panelId) {
		panelManager.makePanelActive (panelId);
	}

	public void sendActiveChannelMessage(string message) {
		panelManager.addEntry (message);
	}

	public void renameRightClickedTab(string newName) {
		string channelName = tabManager.rightClickedTab ().name;
		channelList.renameChatWindow (channelName, newName);
	}

	public void renameChannel(string channelName, string newName) {
		channelList.renameChatWindow (channelName, newName);
	}

	public void removeRightClickedTabWindow() {
		string channelName = tabManager.rightClickedTab ().name;
		channelList.removeChatWindow (channelName);
	}

	public void sendChatMessage(string channelName, string message) {
		channelList.sendMessage (channelName, message);

		if (sendAllMessagesToGeneral && channelName != "general") {
			// also send message to general channel, but do it using original channels TextColor
			Color channelsTextColor = channelList.findChatWindow (channelName).TextColor;
			channelList.sendMessage("general", message, channelsTextColor);
		}
	}

	public void addOptionsMenuUnderCursor(Vector2 pos) {
		chatWindow.SetActive (true);
		chatWindow.GetComponent<RectTransform> ().position = new Vector3 (pos.x, pos.y, 1.0f);
	}
}