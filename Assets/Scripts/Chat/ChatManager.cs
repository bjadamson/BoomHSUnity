using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour {
	[SerializeField] private bool sendAllMessagesToGeneral = true;
	[SerializeField] private GameObject tabAnchor;
	[SerializeField] private GameObject panelAnchor;

	[SerializeField] private TabManager tabManager;
	[SerializeField] private TextPanelManager panelManager;

	private readonly ChatChannelList channelList = new ChatChannelList();

	void Start() {
		ChatWindowFactory cwf = new ChatWindowFactory (tabAnchor, panelAnchor, tabManager, panelManager);
		cwf.createDefaultWindow (channelList);
	}

	public void sendChatMessage(string channelName, string message) {
		channelList.sendMessage (channelName, message);

		if (sendAllMessagesToGeneral && channelName != "general") {
			// also send message to general channel
			channelList.sendMessage("general", message);
		}
	}
}