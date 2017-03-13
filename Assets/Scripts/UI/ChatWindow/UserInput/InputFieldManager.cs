using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ui.chat_window.scroll;

namespace ui
{
	namespace chat_window
	{
		namespace user_input
		{
			public class InputFieldManager : MonoBehaviour
			{
				[SerializeField] private CursorManager cursorManager;
				[SerializeField] private ScrollViewManager scrollBar;
				[SerializeField] private InputField inputField;
				[SerializeField] private ChatManager chatManager;
				[SerializeField] private GameObject commandPanel;
				[SerializeField] private TransparencyManager transparencyManager;
				[SerializeField] private UserIO userIO;

				private GameObject previouslySelected;

				void Start()
				{
					disableReadFromUserStdin(false);
				}

				public bool isEmpty()
				{
					return inputField.text.Length > 0;
				}

				public void selectOnlyIfActive()
				{
					// After we select the tab, move focus to back to the input field if it's active (user is in input mode)
					if (inputField.IsActive())
					{
						inputField.Select();
					}
				}

				public string getPlaceholderText()
				{
					return inputField.placeholder.GetComponent<Text>().text;
				}

				public void setPlaceholderText(string value)
				{
					inputField.placeholder.GetComponent<Text>().text = value;
				}

				void Update()
				{
					bool isActive = inputField.IsActive();
					if (Input.GetKeyDown(KeyCode.Return))
					{
						userIO.toggleBlockingPlayerActions();
						if (!isActive)
						{
							enableReadFromUserStdin();
						}
						else
						{
							disableReadFromUserStdin(true);
						}
					}
					if (Input.GetKeyDown(KeyCode.Escape) && isActive)
					{
						userIO.toggleBlockingPlayerActions();
						disableReadFromUserStdin(true);
					}

					if (Input.GetKeyDown(KeyCode.Tab))
					{
						toggleCommandPanel();
					}
				}

				public bool isReadingChatInputFromStdin()
				{
					return inputField.gameObject.activeSelf;
				}

				private void toggleCommandPanel()
				{
					commandPanel.SetActive(!commandPanel.activeSelf);
				}

				private void hideCommandPanel()
				{
					commandPanel.SetActive(false);
				}

				private void enableReadFromUserStdin()
				{
					inputField.gameObject.SetActive(true);

					transparencyManager.makeOpaque();
					pushSelected();
					inputField.Select();

					cursorManager.unlockCursor();
				}

				private void disableReadFromUserStdin(bool acceptInput)
				{
					inputField.gameObject.SetActive(false);
					transparencyManager.makeTransparent();
					popSelected();
					if (acceptInput && inputField.text.Trim().Length != 0)
					{
						chatManager.sendActiveChannelMessage(inputField.text);
					}
					scrollBar.resetPosition();

					// clear out input box field.
					inputField.text = string.Empty;

					hideCommandPanel();
					cursorManager.lockCursor();
				}

				private void pushSelected()
				{
					previouslySelected = EventSystem.current.currentSelectedGameObject;
				}

				private void popSelected()
				{
					EventSystem.current.SetSelectedGameObject(previouslySelected);
				}
			}

		}
	}
}