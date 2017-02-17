using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmoteInfoPanel : MonoBehaviour {
	private static readonly int fontSize = 11;

	[SerializeField] private GameObject emotesAnchor;
	[SerializeField] private Text emptyResultText;

	[SerializeField] private InputField input;
	private SlashCommandManager manager = new SlashCommandManager();
	private int row = 0;

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

		manager.add (new SlashCommand (emotesAnchor, guiText, name, description, fontSize));
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

	private void displayNoMatchingResult () {
		showNoMatchingResults ();
	}

	private void hideNoMatchingResults() {
		emptyResultText.transform.parent.gameObject.SetActive (false);
		emptyResultText.gameObject.SetActive (false);
	}

	private void showNoMatchingResults() {
		emptyResultText.transform.parent.gameObject.SetActive (true);
		emptyResultText.gameObject.SetActive (true);
		emptyResultText.text = "No commands found beginning with '" + input.text + "'";
	}
}
