using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatInput : MonoBehaviour {
	// config, move out
	[SerializeField] private bool forceChatScrollBarToBottomAfterSubmit = false;
	private static readonly int NUMBER_ROWS = 50;

	[SerializeField] private InputField inputBox;
	[SerializeField] private GameObject content;
	[SerializeField] private Scrollbar scrollbar;

	private GameObject previouslySelected;
	private float scrollPos;
	private Text[] textFields = new Text[NUMBER_ROWS];
	private int row;

	void Start() {
		row = textFields.Length;
		for (int i = 0; i < row; ++i) {
			textFields [i] = addNewChatEntry (string.Empty, i);
		}

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
		replaceChatEntry (inputBox.text);
		resetScrollBarPosition ();

		// lastly clear out input box field.
		inputBox.text = string.Empty;
	}

	private void replaceChatEntry(string value) {
		for (int i = 0; i < textFields.Length - 1; ++i) {
			textFields [i].text = textFields [i + 1].text;
		}
		textFields [49].text = value;
	}

	private Text addNewChatEntry (string value, int row) {
		GameObject uiText = new GameObject ("Text" + row.ToString ());
		uiText.transform.SetParent (content.transform);
		uiText.AddComponent<RectTransform> ();
		Text text = uiText.AddComponent<Text> ();
		text.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
		text.fontSize = 13;
		text.color = Color.white;
		text.verticalOverflow = VerticalWrapMode.Overflow;
		text.horizontalOverflow = HorizontalWrapMode.Wrap;
		text.alignByGeometry = true;
		text.text = value;
		text.rectTransform.pivot = new Vector2 (0.5f, 1.0f);
		text.rectTransform.localScale = Vector3.one;

		return text;
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
