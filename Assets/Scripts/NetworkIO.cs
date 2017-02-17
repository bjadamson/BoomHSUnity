using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NetworkIO : MonoBehaviour, IDisposable {
	private static readonly string VALUES = "ben: hi\nbob:sup\njessica:whatup\nbrain:what time is it pinky?\npinky:same time as it always is, it's time to take over the world.";
	private static readonly float TIME_BETWEEN = 2.0f;
	[SerializeField] private ChatManager chatManager;

	private StringReader generalInput;
	private float timeLeft = TIME_BETWEEN;

	void Start() {
		generalInput = new StringReader (VALUES);
	}

	public void FixedUpdate () {
		timeLeft -= Time.deltaTime;
		if (timeLeft >= 0) {
			return;
		}
		sendFakeMessage ();
		timeLeft = TIME_BETWEEN;
	}

	void sendFakeMessage () {
		if (generalInput.Peek () == -1) {
			resetInput ();
		}
		else {
			chatManager.sendChatMessage ("general", generalInput.ReadLine ());
			chatManager.sendChatMessage ("group", "ben: lazy as EFF");
		}
	}

	#region IDisposable implementation
	public void Dispose () {
		if (generalInput != null) {
			generalInput.Dispose ();
			generalInput = null;
		}
	}

	#endregion

	private void resetInput () {
		Dispose ();
		generalInput = new StringReader (VALUES);
	}
}