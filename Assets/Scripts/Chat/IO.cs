using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IO : MonoBehaviour {
	[SerializeField] private ScrollBar scrollBar;
	[SerializeField] private InputField inputBox;
	[SerializeField] private MouseoverManager manager;

	private GameObject previouslySelected;

	void Start() {
		inputBox.gameObject.SetActive (false); // Initially input-field is hidden.
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
			scrollBar.resetPosition ();
			inputBox.gameObject.SetActive (false);
		}
	}

	private void readInputMode() {
		pushSelected ();
		inputBox.Select ();
		scrollBar.cachePosition ();
	}

	private void normalMode() {
		popSelected ();
		if (inputBox.text.Trim().Length != 0) {
			manager.replaceChatEntry (inputBox.text);
		}
		scrollBar.resetPosition ();

		// lastly clear out input box field.
		inputBox.text = string.Empty;
	}

	void pushSelected () {
		previouslySelected = EventSystem.current.currentSelectedGameObject;
	}

	void popSelected () {
		EventSystem.current.SetSelectedGameObject (previouslySelected);
	}
}