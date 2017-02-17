using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmoteInfoPanel : MonoBehaviour {
	private static readonly int fontSize = 11;

	[SerializeField] private GameObject textFieldAnchor;
	[SerializeField] private InputField input;
	private SlashCommandManager manager = new SlashCommandManager();
	private int row = 0;

	GameObject emptyResult;

	void Start() {
		addCommand ("/who", "Display who is in your area.");
		addCommand("/where", "Display your location.");
		addCommand("/ffs", "................. fuck sakes");
		addCommand("/efs", "xxxxxxxxxx fuck sakes");
		addCommand("/def", "XXXXXXXXXX fuck sakes");
		addCommand("/ffs", "For fuck sakes");
		addCommand("/abc", "For fuck sakes");
		addCommand("/ffs", "For fuck sakes");
		addCommand("/zzzzzzzzzzzzzzzwwwwwwwwwwww", "For FRSFSFSFSF sakes");
		addCommand("/ffs", "ZZZZZZZZZZ fuck sakes");
		addCommand("/ser", "TTTTTTTTTTTTTTTT fuck sakes");
		addCommand("/zcvzxcvz", "66666666666 fuck sakes");
		addCommand("/aaaaaaaaaaaaaaa", "For fuck sakes");
	}
		
	private void addCommand(string name, string description) {
		string guiText = "Emote" + row;
		row++;

		manager.add (new SlashCommand (textFieldAnchor, guiText, name, description, fontSize));
	}

	public void refresh() {
		manager.showEntriesMatchingPrefix (input.text);

		bool atleastOneResult = manager.showingAtleastOne;
		if (!atleastOneResult) {
			displayNoMatchingResult ();
		} else {
			hideNoMatchingResults ();
		}
	}

	void displayNoMatchingResult ()
	{
		if (!emptyResult) {
			createAndAddNoMatchingResults ();
		}
		showNoMatchingResults ();
	}

	private void createAndAddNoMatchingResults () {
		emptyResult = new GameObject ("NoSlashCommand");
		emptyResult.transform.SetParent (textFieldAnchor.transform);
		RectTransform rect = emptyResult.AddComponent<RectTransform> ();
		rect.localScale = Vector3.one;
		Text text = emptyResult.AddComponent<Text> ();
		text.font = Resources.Load<Font> ("courbd");
		Debug.Assert (text.font != null);
		text.fontSize = fontSize;
		text.fontStyle = FontStyle.Normal;
		text.supportRichText = true;
		text.alignment = TextAnchor.MiddleLeft;
		text.alignByGeometry = true;
		text.resizeTextForBestFit = false;
		text.color = Color.red;
		LayoutElement layoutElement = emptyResult.AddComponent<LayoutElement> ();
		layoutElement.preferredWidth = 0;
	}

	private void hideNoMatchingResults() {
		if (emptyResult) {
			emptyResult.SetActive (false);
		}
	}

	private void showNoMatchingResults() {
		emptyResult.SetActive (true);
		emptyResult.GetComponent<Text>().text = "No commands found beginning with '" + input.text + "'";
	}
}
