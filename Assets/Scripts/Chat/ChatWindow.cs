using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatWindow {
	public readonly Tabs Tab;
	public readonly TextPanel Panel;

	private string name;
	public Color TextColor;

	public ChatWindow(string name, Color textColor, Tabs tab, TextPanel panel) {
		this.name = name;
		this.TextColor = textColor;
		this.Tab = tab;
		this.Panel = panel;
	}

	public void setTabText(string value) {
		Tab.transform.FindChild ("Text").GetComponent<Text> ().text = value;
	}

	public void addChatEntry(string value) {
		Panel.addEntry (value);
	}

	public void addChatEntry(string value, Color textColor) {
		Panel.addEntry (value, textColor);
	}

	public void rename(string name) {
		this.name = name;
		setTabText (name);
	}

	public void destroyGameObjects() {
		GameObject.Destroy (this.Tab.gameObject);
		GameObject.Destroy (this.Panel.gameObject);
	}

	public string getName() {
		return this.name;
	}
}