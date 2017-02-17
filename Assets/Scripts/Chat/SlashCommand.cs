using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlashCommand  {
	private static readonly Color commandColor = new Color (0.902f, 1.0f, 0.0f);

	private GameObject entry;
	private GameObject entryParent;
	private readonly string commandName;
	private readonly int fontSize;

	public SlashCommand(GameObject parent, string guiText, string name, string description, int fontSize) {
		entry = new GameObject (guiText);
		entryParent = parent;
		entry.transform.SetParent (entryParent.transform);

		commandName = name;
		this.fontSize = fontSize;

		RectTransform rect = entry.AddComponent<RectTransform> ();
		rect.pivot = new Vector2 (0.5f, 0.5f);
		rect.localScale = Vector3.one;
		rect.sizeDelta = new Vector2 (490, fontSize);

		HorizontalLayoutGroup hg = entry.AddComponent<HorizontalLayoutGroup> ();
		hg.childAlignment = TextAnchor.UpperLeft;
		hg.childControlWidth = true;
		hg.childControlHeight = true;
		hg.childForceExpandWidth = true;
		hg.childForceExpandHeight = true;

		LayoutElement le = entry.AddComponent<LayoutElement> ();
		le.minHeight = fontSize + 2;

		addCommand (entry);
		addDescription (entry, description);
	}

	public void hide() {
		entry.SetActive (false);
		entry.transform.SetParent(null);
	}

	public void show() {
		entry.transform.SetParent (entryParent.transform);
		entry.SetActive (true);
	}

	public string name() {
		return commandName;
	}

	private void addCommand (GameObject parent) {
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
		text.text = commandName;
		LayoutElement layoutElement = command.AddComponent<LayoutElement> ();
		layoutElement.preferredWidth = 0;
	}

	private void addDescription (GameObject parent, string value) {
		GameObject description = new GameObject ("Description");
		description.transform.SetParent (parent.transform);
		RectTransform rect = description.AddComponent<RectTransform>();
		rect.localScale = Vector3.one;
		Text text = description.AddComponent<Text> ();
		text.font = Resources.Load<Font> ("cour");
		Debug.Assert (text.font != null);

		text.fontSize = fontSize;
		text.fontStyle = FontStyle.Normal;
		text.supportRichText = true;
		text.alignment = TextAnchor.MiddleLeft;
		text.alignByGeometry = true;
		text.resizeTextForBestFit = false;
		text.text = value;
		LayoutElement layoutElement = description.AddComponent<LayoutElement> ();
		layoutElement.preferredWidth = 384.0396f;
	}
}