using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatChannelList {
	private readonly IList<ChatChannel> channels = new List<ChatChannel>();

	public void addNewChannel(string channelName, TextPanel panel) {
		channels.Add (new ChatChannel (channelName, panel));
	}

	public void sendMessage(string channelName, string message) {
		foreach (ChatChannel ch in channels) {
			if (ch.Name.ToLower() == channelName) {
				ch.sendMessage (message);
				break;
			}
		}
	}
}