using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		public class ChatWindow
		{
			public readonly TabView Tab;
			public readonly TextPanel Panel;
			public Color TextColor;

			public ChatWindow (string name, Color textColor, TabView tab, TextPanel panel)
			{
				this.TextColor = textColor;
				this.Tab = tab;
				this.Panel = panel;

				setName (name);
			}

			public void setTabText (string value)
			{
				Tab.setText (value);
			}

			public void addChatEntry (string value)
			{
				Panel.addEntry (value);
			}

			public void addChatEntry (string value, Color textColor)
			{
				Panel.addEntry (value, textColor);
			}

			public void rename (string name)
			{
				this.Tab.gameObject.name = name;
				setTabText (name);
			}

			public void destroyGameObjects ()
			{
				GameObject.Destroy (this.Tab.gameObject);
				GameObject.Destroy (this.Panel.gameObject);
			}

			public string getName ()
			{
				return this.Tab.gameObject.name;
			}

			private void setName (string value)
			{
				this.Tab.gameObject.name = value;
			}
		}

	}
}