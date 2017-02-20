using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		public class MenuItemCreate : MonoBehaviour
		{
			[SerializeField] private MenuItemManager menuManager;
			[SerializeField] private GameObject chatOptionsWindow;
			[SerializeField] private ChatManager chatManager;
			[SerializeField] private InputField createInputField;

			public void createNewWindow ()
			{
				string channelName = createInputField.text.Trim ();
				if (channelName.Length == 0) {
					// Do nothing if the user didn't enter anything.
					return;
				}

				createInputField.text = string.Empty;
				chatManager.createChatWindow (channelName);

				// Do this last
				chatOptionsWindow.gameObject.SetActive (false);
			}
		}

	}
}