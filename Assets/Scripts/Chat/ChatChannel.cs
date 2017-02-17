using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatChannel {
	public readonly string Name;
	private TextPanel textPanel;

	public ChatChannel(string name, TextPanel panel) {
		this.Name = name;
		this.textPanel = panel;
	}

	public void sendMessage(string message) {
		textPanel.addEntry (message);
	}
}