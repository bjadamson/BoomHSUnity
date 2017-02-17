using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour {
	[SerializeField] private bool sendAllMessagesToGeneral = true;
	[SerializeField] private InputFieldManager inputFieldManager;

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

	public void sendChatMessage(string channelName, string message) {
		channelList.sendMessage (channelName, message);

		if (sendAllMessagesToGeneral && channelName != "general") {
			// also send message to general channel
			channelList.sendMessage("general", message);
		}
	}
}