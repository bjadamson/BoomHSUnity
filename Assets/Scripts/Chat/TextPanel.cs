using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanel : MonoBehaviour {
	public string initialText = string.Empty;
	public Color textColor = Color.white;

	// config, move out
	private static readonly uint MAX_HISTORY_NUMBER_LINES = 50;
	private IList<Text> textFields = new List<Text>();

	void Start() {
		insertNew (initialText);
	}

	public void addNewChatEntry(string value) {
		if (textFields.Count < MAX_HISTORY_NUMBER_LINES) {
			insertNew(value);
		} else {
			updateExisting (value);
		}
	}

	private void insertNew (string value) {
		GameObject uiText = new GameObject ("Text" + textFields.Count.ToString ());

		uiText.transform.SetParent (this.transform);
		uiText.AddComponent<RectTransform> ();

		Text text = uiText.AddComponent<Text> ();
		text.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
		text.fontSize = 14;
		text.fontStyle = FontStyle.Bold;
		text.color = textColor;
		text.verticalOverflow = VerticalWrapMode.Overflow;
		text.horizontalOverflow = HorizontalWrapMode.Wrap;
		text.alignment = TextAnchor.MiddleLeft;
		text.alignByGeometry = true;
		text.text = value;
		text.rectTransform.pivot = new Vector2 (0.5f, 1.0f);
		text.rectTransform.localScale = Vector3.one;

		textFields.Add(text);
	}
	private void updateExisting(string value) {
		for (int i = 0; i < textFields.Count - 1; ++i) {
			textFields [i].text = textFields [i + 1].text;
		}
		textFields [textFields.Count - 1].text = value;
	}
}