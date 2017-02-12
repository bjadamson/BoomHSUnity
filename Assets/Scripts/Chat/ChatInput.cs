using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatInput : MonoBehaviour {
	[SerializeField] private InputField inputBox;
	[SerializeField] private GameObject content;
	[SerializeField] private Scrollbar scrollbar;

	private GameObject previouslySelected;
	private float scrollPos;
	private int row = 0;

	// config, move out
	[SerializeField] private bool forceChatScrollBarToBottomAfterSubmit = false;

	void Start() {
		inputBox.gameObject.SetActive (false); // Initially input-field is hidden.
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
		scrollPos = scrollbar.value;
	}

	private void normalMode() {
		EventSystem.current.SetSelectedGameObject (previouslySelected);
		addNewChatEntry ();
		resetScrollBarPosition ();

		// lastly clear out input box field.
		inputBox.text = string.Empty;
	}

	private void addNewChatEntry () {
		GameObject uiText = new GameObject ("Text" + row.ToString ());
		++row;
		uiText.transform.SetParent (content.transform);
		uiText.AddComponent<RectTransform> ();
		Text text = uiText.AddComponent<Text> ();
		text.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
		text.fontSize = 13;
		text.color = Color.white;
		text.verticalOverflow = VerticalWrapMode.Overflow;
		text.horizontalOverflow = HorizontalWrapMode.Wrap;
		text.alignByGeometry = true;
		text.text = inputBox.text;
		text.rectTransform.pivot = new Vector2 (0.5f, 1.0f);
		text.rectTransform.localScale = Vector3.one;
	}

	void resetScrollBarPosition ()
	{
		if (forceChatScrollBarToBottomAfterSubmit) {
			scrollbar.value = 0.0f;
		} else {
			scrollbar.value = scrollPos;
		}
	}
}
