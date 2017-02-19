using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ui.chat_window.tab;
using ui.chat_window.user_input;

namespace ui
{
	namespace chat_window
	{
		public class ChatModel
		{
			private readonly IList<ChatWindow> chatWindows = new List<ChatWindow> ();
			private readonly PanelViewManager panelManager;
			private readonly TabViewManager tabManager;
			private readonly InputFieldManager inputFieldManager;

			public ChatModel (PanelViewManager panelManager, TabViewManager tabManager, InputFieldManager inputFieldManager)
			{
				this.panelManager = panelManager;
				this.tabManager = tabManager;
				this.inputFieldManager = inputFieldManager;
			}

			public void addNewChannel (string channelName, Color textColor, TabView tab, PanelView panel)
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

			public Color getChannelTextColor (string channelName)
			{
				ChatWindow cw = findChatWindow (channelName);
				return cw.TextColor;
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

			private ChatWindow findChatWindow (string channelName)
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
		}
	}
}