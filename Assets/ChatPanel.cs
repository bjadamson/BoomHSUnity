using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatPanel : MonoBehaviour {
	private Text[] textFields = new Text[50];

	void Start() {
		//row = textFields.Length;
		for (int i = 0; i < 50; ++i) {
			textFields [i] = addNewChatEntry (string.Empty, i);
		}
	}

	private Text addNewChatEntry (string value, int row) {
		GameObject uiText = new GameObject ("Text" + row.ToString ());
		uiText.transform.SetParent (this.transform);
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

	public void replaceChatEntry(string value) {
		for (int i = 0; i < textFields.Length - 1; ++i) {
			textFields [i].text = textFields [i + 1].text;
		}
		textFields [49].text = value;
	}
}