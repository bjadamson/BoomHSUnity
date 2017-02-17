using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IOManager : MonoBehaviour {
	[SerializeField] private ScrollBar scrollBar;
	[SerializeField] private InputField inputField;
	[SerializeField] private TextPanelManager panelManager;
	[SerializeField] private TabManager tabManager;
	[SerializeField] private TransparencyManager transparencyManager;
	private GameObject previouslySelected;

	void Start() {
		inputField.gameObject.SetActive (false); // Initially input-field is hidden.
	}

	void Update () {
		bool wasActive = inputField.IsActive ();
		if (Input.GetKeyDown (KeyCode.Return)) {
			bool nowActive = !wasActive;
			inputField.gameObject.SetActive (nowActive);

			if (nowActive) {
				transparencyManager.makeOpaque ();
				readInputMode ();
			} else {
				transparencyManager.makeTransparent ();
				normalMode ();
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape) && wasActive) {
			popSelected ();
			scrollBar.resetPosition ();
			inputField.gameObject.SetActive (false);
		}
	}
	private void readInputMode() {
		pushSelected ();
		inputField.Select ();
	}

	private void normalMode() {
		popSelected ();
		if (inputField.text.Trim().Length != 0) {
			panelManager.addEntry (inputField.text);
		}
		scrollBar.resetPosition ();

		// lastly clear out input box field.
		inputField.text = string.Empty;
	}

	private void pushSelected () {
		previouslySelected = EventSystem.current.currentSelectedGameObject;
	}

	private void popSelected () {
		EventSystem.current.SetSelectedGameObject (previouslySelected);
	}
}