using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatWindowList {
	private readonly IList<ChatWindow> chatWindows = new List<ChatWindow>();
	private readonly TextPanelManager panelManager;
	private readonly TabManager tabManager;

	public ChatWindowList(TextPanelManager panelManager, TabManager tabManager) {
		this.panelManager = panelManager;
		this.tabManager = tabManager;
	}

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
			if (ch.getName().ToLower() == channelName) {
				channel = ch;
				break;
			}
		}
		return channel;
	}

	public void renameChatWindow(string channelName, string newName) {
		ChatWindow cw = findChatWindow (channelName);
		cw.rename (newName);
	}

	public void removeChatWindow(string channelName) {
		ChatWindow cw = findChatWindow (channelName);

		panelManager.removePanel (cw.Panel);
		tabManager.removeTab (cw.Tab);
		chatWindows.Remove (cw);
		cw.destroyGameObjects ();
	}
}