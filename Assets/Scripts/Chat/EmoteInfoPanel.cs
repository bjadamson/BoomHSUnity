using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmoteInfoPanel : MonoBehaviour {
	[SerializeField] private GameObject textFieldAnchor;

	private static readonly Color commandColor = new Color (0.902f, 1.0f, 0.0f);
	private static readonly int fontSize = 11;
	private int row = 0;

	void Start() {
		addEntry ("/who", "Display who is in your area.");
		addEntry ("/where", "Display your location.");

		for (int i = 0; i < 30; ++i) {
			addEntry ("/ffs", "For fuck sakes");
		}
	}

	public void addEntry(string name, string description) {
		GameObject entry = new GameObject ("Emote" + row);
		entry.transform.SetParent (textFieldAnchor.transform);
		++row;

		RectTransform rect = entry.AddComponent<RectTransform> ();
		rect.pivot = new Vector2 (0.5f, 0.5f);
		rect.localScale = Vector3.one;
		rect.sizeDelta = new Vector2 (490, 11);// = 12;

		HorizontalLayoutGroup hg = entry.AddComponent<HorizontalLayoutGroup> ();
		hg.childAlignment = TextAnchor.UpperLeft;
		hg.childControlWidth = true;
		hg.childControlHeight = true;
		hg.childForceExpandWidth = true;
		hg.childForceExpandHeight = true;

		LayoutElement le = entry.AddComponent<LayoutElement> ();
		le.minHeight = fontSize + 2;

		addCommand (entry, name);
		addDescription (entry, description);
	}

	void addCommand (GameObject parent, string value) {
		GameObject command = new GameObject ("Command");
		command.transform.SetParent (parent.transform);
		RectTransform rect = command.AddComponent<RectTransform>();
		rect.localScale = Vector3.one;
		Text text = command.AddComponent<Text> ();
		text.font = Resources.Load<Font> ("courbd");
		Debug.Assert (text.font != null);

		text.fontSize = fontSize;
		text.fontStyle = FontStyle.Normal;
		text.supportRichText = true;
		text.alignment = TextAnchor.MiddleLeft;
		text.alignByGeometry = true;
		text.resizeTextForBestFit = false;
		text.color = commandColor;
		text.text = value;
		LayoutElement layoutElement = command.AddComponent<LayoutElement> ();
		layoutElement.preferredWidth = 0;
	}

	private static void addDescription (GameObject parent, string value) {
		GameObject command = new GameObject ("Description");
		command.transform.SetParent (parent.transform);
		RectTransform rect = command.AddComponent<RectTransform>();
		rect.localScale = Vector3.one;
		Text text = command.AddComponent<Text> ();
		text.font = Resources.Load<Font> ("cour");
		Debug.Assert (text.font != null);

		text.fontSize = fontSize;
		text.fontStyle = FontStyle.Normal;
		text.supportRichText = true;
		text.alignment = TextAnchor.MiddleLeft;
		text.alignByGeometry = true;
		text.resizeTextForBestFit = false;
		text.text = value;
		LayoutElement layoutElement = command.AddComponent<LayoutElement> ();
		layoutElement.preferredWidth = 384.0396f;
	}
}
