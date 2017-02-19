using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		public class TextPanel : MonoBehaviour
		{
			private Color textColor = Color.white;

			// config, move out
			private static readonly uint MAX_HISTORY_NUMBER_LINES = 10;
			private IList<Text> textFields = new List<Text> ();

			public void addEntry (string value)
			{
				addEntry (value, this.textColor);
			}

			public void addEntry (string value, Color color)
			{
				if (textFields.Count < MAX_HISTORY_NUMBER_LINES) {
					insertNew (value, color);
				} else {
					updateExisting (value, color);
				}
			}

			public void setTextColor (Color color)
			{
				textColor = color;
				foreach (var it in textFields) {
					it.color = textColor;
				}
			}

			private void insertNew (string value, Color fontColor)
			{
				GameObject uiText = new GameObject ("Text" + textFields.Count.ToString ());

				uiText.transform.SetParent (this.transform);
				uiText.AddComponent<RectTransform> ();

				Text text = uiText.AddComponent<Text> ();
				text.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
				text.fontSize = 14;
				text.fontStyle = FontStyle.Bold;
				text.color = fontColor;
				text.verticalOverflow = VerticalWrapMode.Overflow;
				text.horizontalOverflow = HorizontalWrapMode.Wrap;
				text.alignment = TextAnchor.MiddleLeft;
				text.alignByGeometry = true;
				text.text = value;
				text.rectTransform.pivot = new Vector2 (0.5f, 1.0f);
				text.rectTransform.localScale = Vector3.one;

				textFields.Add (text);
			}

			private void updateExisting (string value, Color textColor)
			{
				for (int i = 0; i < textFields.Count - 1; ++i) {
					Text text = textFields [i];
					Text nextText = textFields [i + 1];
					text.text = nextText.text;
					text.color = nextText.color;
				}
				Text lastField = textFields [textFields.Count - 1];
				lastField.text = value;
				lastField.color = textColor;
			}
		}

	}
}