using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollBar : MonoBehaviour {
	// config, move out
	[SerializeField] private bool forceChatScrollBarToBottomAfterSubmit = false;

	private Scrollbar scrollbar;
	private float previousValue = 0.0f;

	void Start() {
		scrollbar = GetComponent<Scrollbar> ();
		scrollbar.value = 0.0f;
	}

	public void cachePosition() {
		previousValue = scrollbar.value;
	}

	public void resetPosition () {
		if (forceChatScrollBarToBottomAfterSubmit) {
			scrollbar.value = 0.0f;
		} else {
			scrollbar.value = previousValue;
		}
	}
}