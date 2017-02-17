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
		bool isActive = inputField.IsActive ();
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (!isActive) {
				enableReadFromUserStdin ();
			} else {
				disableReadFromUserStdin (true);
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape) && isActive) {
			disableReadFromUserStdin(false);
		}
	}
	private void enableReadFromUserStdin() {
		inputField.gameObject.SetActive (true);
		transparencyManager.makeOpaque ();
		pushSelected ();
		inputField.Select ();
	}

	private void disableReadFromUserStdin(bool acceptInput) {
		inputField.gameObject.SetActive (false);
		transparencyManager.makeTransparent ();
		popSelected ();
		if (acceptInput && inputField.text.Trim ().Length != 0) {
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