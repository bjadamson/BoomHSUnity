using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlashCommandManager {
	private IList<SlashCommand> commands = new List<SlashCommand>();
	public bool showingAtleastOne = false;

	public void add(SlashCommand cmd) {
		commands.Add (cmd);
	}

	public void showEntriesMatchingPrefix(string prefix) {
		foreach (SlashCommand cmd in commands) {
			cmd.hide ();
		}

		showingAtleastOne = false;
		foreach(SlashCommand cmd in commands) {
			if (cmd.name ().StartsWith (prefix)) {
				cmd.show ();
				showingAtleastOne = true;
			}
		}
	}
}