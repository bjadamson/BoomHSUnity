using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatInput : MonoBehaviour {
	[SerializeField] private InputField inputBox;
	[SerializeField] private Text textArea;

	private GameObject previouslySelected;

	void Start() {
		inputBox.gameObject.SetActive (false); // Initially input-field is hidden.
		//textArea.text = string.Empty;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			bool wasActive = inputBox.IsActive ();
			bool nowActive = !wasActive;
			inputBox.gameObject.SetActive (nowActive);

			if (nowActive) {
				readInputMode ();
			} else {
				normalMode ();
			}
		}
	}

	private void readInputMode() {
		previouslySelected = EventSystem.current.currentSelectedGameObject;
		inputBox.Select ();
	}

	private void normalMode() {
		string combined = textArea.text + "\n" + inputBox.text;
		if (combined.Length > 500) {
			combined.Remove (0, 100);
		}
		textArea.text = combined;
		inputBox.text = string.Empty;
		EventSystem.current.SetSelectedGameObject (previouslySelected);
	}
}
