using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatTextPanel : MonoBehaviour {
	[SerializeField] private string initialText = string.Empty;
	[SerializeField] private Color textColor = Color.white;
	private static readonly int NUM_FIELDS = 50;
	private Text[] textFields = new Text[NUM_FIELDS];

	void Start() {
		for (int i = 0; i < NUM_FIELDS; ++i) {
			textFields [i] = addNewChatEntry (string.Empty, i);
		}
		textFields [NUM_FIELDS - 1].text = initialText;
	}

	private Text addNewChatEntry (string value, int row) {
		GameObject uiText = new GameObject ("Text" + row.ToString ());
		uiText.transform.SetParent (this.transform);
		uiText.AddComponent<RectTransform> ();
		Text text = uiText.AddComponent<Text> ();
		text.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
		text.fontSize = 14;
		text.fontStyle = FontStyle.Bold;
		text.color = textColor;
		text.verticalOverflow = VerticalWrapMode.Overflow;
		text.horizontalOverflow = HorizontalWrapMode.Wrap;
		text.alignByGeometry = true;
		text.text = value;
		text.rectTransform.pivot = new Vector2 (0.5f, 1.0f);
		text.rectTransform.localScale = Vector3.one;

		return text;
	}

	void Update() {
		foreach (Text t in textFields) {
			t.color = textColor;
		}
	}

	public void replaceChatEntry(string value) {
		for (int i = 0; i < textFields.Length - 1; ++i) {
			textFields [i].text = textFields [i + 1].text;
		}
		textFields [NUM_FIELDS - 1].text = value;
	}
}