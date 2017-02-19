﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		public class ChatWindowList
		{
			private readonly IList<ChatWindow> chatWindows = new List<ChatWindow> ();
			private readonly TextPanelManager panelManager;
			private readonly TabManager tabManager;
			private readonly InputFieldManager inputFieldManager;

			public ChatWindowList (TextPanelManager panelManager, TabManager tabManager, InputFieldManager inputFieldManager)
			{
				this.panelManager = panelManager;
				this.tabManager = tabManager;
				this.inputFieldManager = inputFieldManager;
			}

			public void addNewChannel (string channelName, Color textColor, Tabs tab, TextPanel panel)
			{
				chatWindows.Add (new ChatWindow (channelName, textColor, tab, panel));
			}

			public void sendMessage (string channelName, string message)
			{
				ChatWindow cw = findChatWindow (channelName);
				if (cw != null) {
					cw.addChatEntry (message);
				}
			}

			public void sendMessage (string channelName, string message, Color textColor)
			{
				ChatWindow cw = findChatWindow (channelName);
				if (cw != null) {
					cw.addChatEntry (message, textColor);
				}
			}

			public ChatWindow findChatWindow (string channelName)
			{
				ChatWindow channel = null;
				foreach (ChatWindow ch in chatWindows) {
					if (ch.getName ().ToLower () == channelName.ToLower ()) {
						channel = ch;
						break;
					}
				}
				return channel;
			}

			public void renameChatWindow (string channelName, string newName)
			{
				ChatWindow cw = findChatWindow (channelName);
				cw.rename (newName);

				if (tabManager.rightClickedTab () == tabManager.selectedTab ()) {
					inputFieldManager.setPlaceholderText (newName + "...");
				}
			}

			public void removeChatWindow (string channelName)
			{
				ChatWindow cw = findChatWindow (channelName);

				panelManager.removePanel (cw.Panel);
				tabManager.removeTab (cw.Tab);
				chatWindows.Remove (cw);
				cw.destroyGameObjects ();

				chatWindows.Remove (cw);
			}
		}

	}
}