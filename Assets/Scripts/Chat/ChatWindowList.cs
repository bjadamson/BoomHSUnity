using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatWindowList {
	private readonly IList<ChatWindow> chatWindows = new List<ChatWindow>();

	public void addNewChannel(string channelName, Color textColor, Tabs tab, TextPanel panel) {
		chatWindows.Add (new ChatWindow (channelName, textColor, tab, panel));
	}

	public void sendMessage(string channelName, string message) {
		ChatWindow cw = findChatWindow (channelName);
		if (cw != null) {
			cw.addChatEntry (message);
		}
	}

	public void sendMessage(string channelName, string message, Color textColor) {
		ChatWindow cw = findChatWindow (channelName);
		if (cw != null) {
			cw.addChatEntry (message, textColor);
		}
	}

	public ChatWindow findChatWindow(string channelName) {
		ChatWindow channel = null;
		foreach (ChatWindow ch in chatWindows) {
			if (ch.Name.ToLower() == channelName) {
				channel = ch;
				break;
			}
		}
		return channel;
	}
}