using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using ui.chat_window;

public class UserIO : MonoBehaviour {
	private bool blockingNonChatInput = false;

	public bool GetKeyDown(KeyCode code) {
		return !blockingNonChatInput && Input.GetKeyDown(code);
	}

	public bool GetKey(KeyCode code) {
		return !blockingNonChatInput && Input.GetKey(code);
	}

	public bool GetKeyUp(KeyCode code) {
		return !blockingNonChatInput && Input.GetKeyUp(code);
	}

	public float GetAxis(string axisName) {
		return blockingNonChatInput ? 0.0f : Input.GetAxis(axisName);
	}

	public void toggleBlockingPlayerActions() {
		blockingNonChatInput = !blockingNonChatInput;
	}

	public bool GetButtonDown(string name) {
		return Input.GetButtonDown(name);
	}

	public bool GetButtonUp(string name) {
		return Input.GetButtonUp(name);
	}
}