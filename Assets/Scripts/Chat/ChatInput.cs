using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatInput : MonoBehaviour {
	// config, move out
	[SerializeField] private bool forceChatScrollBarToBottomAfterSubmit = false;

	[SerializeField] private InputField inputBox;
	[SerializeField] private ChatPanel chatPanel;
	[SerializeField] private Scrollbar scrollbar;

	private GameObject previouslySelected;
	private float scrollPos;

	void Start() {
		inputBox.gameObject.SetActive (false); // Initially input-field is hidden.
		scrollbar.value = 0.0f; // Initially scrolled all the way down.
	}

	void Update () {
		bool wasActive = inputBox.IsActive ();
		if (Input.GetKeyDown (KeyCode.Return)) {
			bool nowActive = !wasActive;
			inputBox.gameObject.SetActive (nowActive);

			if (nowActive) {
				readInputMode ();
			} else {
				normalMode ();
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape) && wasActive) {
			popSelected ();
			resetScrollBarPosition ();
			inputBox.gameObject.SetActive (false);
		}
	}

	private void readInputMode() {
		pushSelected ();
		inputBox.Select ();
		scrollPos = scrollbar.value;
	}

	private void normalMode() {
		popSelected ();
		chatPanel.replaceChatEntry (inputBox.text);
		resetScrollBarPosition ();

		// lastly clear out input box field.
		inputBox.text = string.Empty;
	}

	void resetScrollBarPosition () {
		if (forceChatScrollBarToBottomAfterSubmit) {
			scrollbar.value = 0.0f;
		} else {
			scrollbar.value = scrollPos;
		}
	}

	void pushSelected () {
		previouslySelected = EventSystem.current.currentSelectedGameObject;
	}

	void popSelected () {
		EventSystem.current.SetSelectedGameObject (previouslySelected);
	}
}
