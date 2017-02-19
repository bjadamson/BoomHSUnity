using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmoteInfoPanel : MonoBehaviour {
	private static readonly int fontSize = 13;

	[SerializeField] private GameObject emotesAnchor;
	[SerializeField] private Text emptyResultText;

	[SerializeField] private InputField input;
	private SlashCommandManager manager = new SlashCommandManager();
	private int row = 0;

	void Start() {
		addCommand ("who", "Display who is in your area.");
		addCommand("where", "Display your location.");
		addCommand("ffs", "................. fuck sakes");
		addCommand("efs", "xxxxxxxxxx fuck sakes");
		addCommand("def", "XXXXXXXXXX fuck sakes");
		addCommand("ffs", "For fuck sakes");
		addCommand("abc", "For fuck sakes");
		addCommand("ffs", "For fuck sakes");
		addCommand("zzzz", "For FRSFSFSFSF sakes");
		addCommand("ffs", "ZZZZZZZZZZ fuck sakes");
		addCommand("ser", "TTTTTTTTTTTTTTTT fuck sakes");
		addCommand("zcvzz", "66666666666 fuck sakes");
		addCommand("aaaa", "For fuck sakes");
		addCommand ("rename channel", "Rename a chat channel, ie: '/rename channel group party'");

		emptyResultText.text = string.Empty;
	}
		
	private void addCommand(string name, string description) {
		string guiText = "Emote" + row;
		row++;

		manager.add (new SlashCommand (emotesAnchor, guiText, name, description, fontSize));
	}

	public void refresh() {
		string userText = input.text.Trim ();
		if (userText == string.Empty) {
			return;
		}

		if (userText.StartsWith ("/")) {
			userText = userText.Remove (0, 1);
		}
		manager.showEntriesMatchingPrefix (userText);

		bool atleastOneResult = manager.showingAtleastOne;
		if (!atleastOneResult) {
			displayNoMatchingResult ();
		} else {
			hideNoMatchingResults ();
		}
	}

	private void displayNoMatchingResult () {
		showNoMatchingResults ();
	}

	private void hideNoMatchingResults() {
		emptyResultText.text = string.Empty;
	}

	private void showNoMatchingResults() {
		emptyResultText.text = "No commands found beginning with '" + input.text + "'";
	}
}
