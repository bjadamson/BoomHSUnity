using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatWindow {
	private readonly Tabs tab;
	private readonly TextPanel panel;

	public string Name;
	public Color TextColor;

	public ChatWindow(string name, Color textColor, Tabs tab, TextPanel panel) {
		this.Name = name;
		this.TextColor = textColor;
		this.tab = tab;
		this.panel = panel;
	}

	public void setTabText(string value) {
		tab.transform.FindChild ("Text").GetComponent<Text> ().text = value;
	}

	public void addChatEntry(string value) {
		panel.addEntry (value);
	}

	public void addChatEntry(string value, Color textColor) {
		panel.addEntry (value, textColor);
	}
}