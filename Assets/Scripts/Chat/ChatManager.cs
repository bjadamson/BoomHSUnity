using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour {
	[SerializeField] private bool sendAllMessagesToGeneral = true;
	[SerializeField] private InputFieldManager inputFieldManager;
	[SerializeField] private TextPanelManager panelManager;

	private readonly ChatChannelList channelList = new ChatChannelList();

	void Start() {
		ChatWindowFactory chatFactory = GetComponent<ChatWindowFactory> ();
		chatFactory.createDefaultWindow (channelList);
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
		inputFieldManager.setPlaceholderText(value);
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

	public void sendChatMessage(string channelName, string message) {
		channelList.sendMessage (channelName, message);

		if (sendAllMessagesToGeneral && channelName != "general") {
			// also send message to general channel
			channelList.sendMessage("general", message);
		}
	}
}