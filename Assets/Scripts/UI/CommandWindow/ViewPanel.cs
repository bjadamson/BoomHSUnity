using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ui.command_window
{
	public class ViewPanel : MonoBehaviour
	{
		private static readonly int fontSize = 13;

		[SerializeField] private GameObject commandAnchor;
		[SerializeField] private Text emptyResultText;

		[SerializeField] private InputField input;
		private CommandDatabase commandDatabase = new CommandDatabase();
		private int row = 0;

		void Start()
		{
			addCommand("who", "Display who is in your area.");
			addCommand("where", "Display your location.");
			addCommand("ffs", "................. fuck sakes");
			addCommand("animals", "are not food");
			addCommand("food", "see plants");
			addCommand("plants", "energy to do things and not die.");
			addCommand("gun", "bOOm");
			addCommand("help", "for success, I need more cowbell!");
			addCommand("zzzz", "For FRSFSFSFSF sakes");
			addCommand("daybreak", "daybreak suuuuuuuuuxxxs");
			addCommand("01001", "0xDEADBEEF");
			addCommand("pizza", "ordering pizza now..");
			addCommand("rename", "Rename a chat channel, ie: '/rename channel group party'");

			emptyResultText.text = string.Empty;
		}

		private void addCommand(string name, string description)
		{
			string guiText = "Command" + row;
			row++;

			commandDatabase.add(new Command(commandAnchor, guiText, name, description, fontSize));
		}

		void Update()
		{
			string userText = input.text.Trim();
			if (userText == string.Empty)
			{
				showEntriesMatchingPrefix("");
				return;
			}

			if (userText.StartsWith("/"))
			{
				userText = userText.Remove(0, 1);
			}

			IList<Command> matchingCommands = commandDatabase.getEntriesMatchingPrefix(userText);
			showEntriesMatchingPrefix(userText);
			hideAllEntries();
			showEntriesMatchingPrefix(userText);

			if (matchingCommands.Count == 0)
			{
				displayNoMatchingResult();
			}
			else
			{
				hideNoMatchingResults();
			}
		}

		private void showEntriesMatchingPrefix(string prefix)
		{
			foreach (Command cmd in commandDatabase.getEntries())
			{
				if (cmd.name().StartsWith(prefix))
				{
					cmd.show();
				}
			}
		}

		private void hideAllEntries()
		{
			foreach (Command cmd in commandDatabase.getEntries())
			{
				cmd.hide();
			}
		}

		private void displayNoMatchingResult()
		{
			showNoMatchingResults();
		}

		private void hideNoMatchingResults()
		{
			emptyResultText.text = string.Empty;
		}

		private void showNoMatchingResults()
		{
			emptyResultText.text = "No commands found beginning with '" + input.text + "'";
		}
	}
}