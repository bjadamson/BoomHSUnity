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
			private readonly IList<ChatWindowView> chatWindows = new List<ChatWindowView> ();
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
				chatWindows.Add (new ChatWindowView (channelName, textColor, tab, panel));
			}

			public void sendMessage (string channelName, string message)
			{
				ChatWindowView cw = findChatWindow (channelName);
				if (cw != null) {
					cw.addChatEntry (message);
				}
			}

			public void sendMessage (string channelName, string message, Color textColor)
			{
				ChatWindowView cw = findChatWindow (channelName);
				if (cw != null) {
					cw.addChatEntry (message, textColor);
				}
			}

			public Color getChannelTextColor (string channelName)
			{
				ChatWindowView cw = findChatWindow (channelName);
				return cw.TextColor;
			}

			public void renameChatWindow (string channelName, string newName)
			{
				ChatWindowView cw = findChatWindow (channelName);
				cw.rename (newName);

				if (tabManager.rightClickedTab () == tabManager.selectedTab ()) {
					inputFieldManager.setPlaceholderText (newName + "...");
				}
			}

			public void removeChatWindow (string channelName)
			{
				ChatWindowView cw = findChatWindow (channelName);

				panelManager.removePanel (cw.Panel);
				tabManager.removeTab (cw.Tab);
				chatWindows.Remove (cw);
				cw.destroyGameObjects ();

				chatWindows.Remove (cw);
			}

			private ChatWindowView findChatWindow (string channelName)
			{
				ChatWindowView channel = null;
				foreach (ChatWindowView ch in chatWindows) {
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