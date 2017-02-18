using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatWindow {
	private readonly Tabs tab;
	private readonly TextPanel panel;

	private string name;
	public Color TextColor;

	public ChatWindow(string name, Color textColor, Tabs tab, TextPanel panel) {
		this.name = name;
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

	public void rename(string name) {
		this.name = name;
		setTabText (name);
	}

	public void destroyGameObjects() {
		GameObject.Destroy (tab);
		GameObject.Destroy (panel);
	}

	public string getName() {
		return this.name;
	}
}