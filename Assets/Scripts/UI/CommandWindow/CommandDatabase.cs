using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
	namespace command_window
	{
		public class CommandDatabase
		{
			private IList<Command> commands = new List<Command> ();
			public bool showingAtleastOne = false;

			public void add (Command cmd)
			{
				commands.Add (cmd);
			}

			public IList<Command> getEntries ()
			{
				return commands;
			}

			public IList<Command> getEntriesMatchingPrefix (string prefix)
			{
				IList<Command> matching = new List<Command> ();
				foreach (Command cmd in commands) {
					if (cmd.name ().StartsWith (prefix)) {
						matching.Add (cmd);
					}
				}
				return matching;
			}
		}
	}
}