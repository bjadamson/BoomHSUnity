using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		public class MenuItemRename : MonoBehaviour
		{
			[SerializeField] private ChatViewManager chatManager;
			[SerializeField] private GameObject chatOptionsWindow;
			[SerializeField] private GameObject renamePanel;
			[SerializeField] private InputField renameInputField;

			private GameObject previouslySelected;

			public void renameChannel ()
			{
				string channelName = renameInputField.text.Trim ();
				if (channelName.Length == 0) {
					// Do nothing if the user didn't enter anything.
					return;
				}
				chatManager.renameRightClickedTab (renameInputField.text);
				renameInputField.text = string.Empty;
				chatOptionsWindow.SetActive (false);
			}

			public void hideHighlight ()
			{
				renamePanel.SetActive (false);
			}

			public void showHighlight ()
			{
				renamePanel.SetActive (true);
			}
		}

	}
}