using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delete : MonoBehaviour {
	[SerializeField] private GameObject chatOptionsWindow;
	[SerializeField] private ChatManager chatManager;

	public void deleteWindow() {
		chatOptionsWindow.gameObject.SetActive (false);

		chatManager.removeChannel ("new channel");
	}
}