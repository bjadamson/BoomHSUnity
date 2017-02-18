using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemRenameChannel : MonoBehaviour {
	[SerializeField] private ChatManager chatManager;

	public void renameChannel() {
		chatManager.renameChannel ("new channel", "lol");
	}
}