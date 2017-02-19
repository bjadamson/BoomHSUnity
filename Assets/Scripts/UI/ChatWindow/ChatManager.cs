using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		public class ChatManager : MonoBehaviour
		{
			[SerializeField] private bool sendAllMessagesToGeneral = true;
			[SerializeField] private InputFieldManager inputFieldManager;
			[SerializeField] private TextPanelManager panelManager;
			[SerializeField] private TabManager tabManager;
			[SerializeField] private GameObject chatWindow;
			[SerializeField] private CanvasGroup chatCanvasGroup;

			private ChatWindowFactory chatFactory;
			private ChatModel channelModel;

			void Start ()
			{
				channelModel = new ChatModel (panelManager, tabManager, inputFieldManager);
				chatFactory = GetComponent<ChatWindowFactory> ();
				chatFactory.setChannelList (channelModel);
				chatFactory.createDefaultWindow ();

				updateUserInputPlaceholderText (tabManager.getActiveTabText ().ToLower ());
			}

			public void createChatWindow (string channelName)
			{
				chatFactory.createDefaultChatWindow (channelName);
			}

			public bool userHasInputAnyCharacter ()
			{
				return inputFieldManager.isEmpty ();
			}

			public void moveFocusToInputFieldIfActive ()
			{
				inputFieldManager.selectOnlyIfActive ();
			}

			public void showPanel (int panelId)
			{
				panelManager.showButNotMakeActive (panelId);
			}

			public void makePanelActive (int panelId)
			{
				panelManager.makePanelActive (panelId);
			}

			public void sendActiveChannelMessage (string message)
			{
				panelManager.addEntry (message);
			}

			public void renameRightClickedTab (string newName)
			{
				string channelName = getRightClickedTabName ();
				channelModel.renameChatWindow (channelName, newName);
			}

			public void renameChannel (string channelName, string newName)
			{
				channelModel.renameChatWindow (channelName, newName);
			}

			public void removeRightClickedTabWindow ()
			{
				string channelName = tabManager.rightClickedTab ().name;
				channelModel.removeChatWindow (channelName);
			}

			public void sendChatMessage (string channelName, string message)
			{
				channelModel.sendMessage (channelName, message);

				if (sendAllMessagesToGeneral && channelName != "general") {
					// also send message to general channel, but do it using original channels TextColor
					Color channelsTextColor = channelModel.getChannelTextColor (channelName);
					channelModel.sendMessage ("general", message, channelsTextColor);
				}
			}

			public void addOptionsMenuUnderCursor (Vector2 pos)
			{
				chatWindow.SetActive (true);
				chatWindow.GetComponent<RectTransform> ().position = new Vector3 (pos.x, pos.y, 1.0f);
			}

			public void hideOptionsMenu ()
			{
				chatWindow.SetActive (false);
			}

			public void setScrollViewBgColor (Color panelColor)
			{
				panelManager.setPanelBgColor (panelColor);
			}

			public void setOverlayTransparency (float alpha)
			{
				chatCanvasGroup.alpha = alpha;
			}

			public void setPanelTextColor (Color color)
			{
				string rightClickedTabName = getRightClickedTabName ();
				panelManager.setPanelTextColor (color);
			}

			public void setRightClickedTabBgColor (Color color)
			{
				string rightClickedTabName = getRightClickedTabName ();
				tabManager.setRightClickedTabBgColor (color);
			}

			public void updateUserInputPlaceholderText(string channelName) {
				string text = tabManager.getActiveTabText ().ToLower ();
				this.setPlaceholderText (text + "...");
			}

			private string getRightClickedTabName ()
			{
				return tabManager.rightClickedTab ().name;
			}

			private void setPlaceholderText (string value)
			{
				inputFieldManager.setPlaceholderText (value);
			}
		}
	}
}