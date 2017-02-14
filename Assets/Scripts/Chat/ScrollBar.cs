using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	// config, move out
	[SerializeField] private bool forceChatScrollBarToBottomAfterSubmit = false;

	private Scrollbar scrollbar;
	private float previousValue = 0.0f;

	void Start() {
		scrollbar = GetComponent<Scrollbar> ();
		scrollbar.value = 0.0f;
	}

	public void OnPointerEnter(PointerEventData eventData) {
		show ();
	}

	public void OnPointerExit(PointerEventData eventData) {
		hide ();
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

	private void show() {
		gameObject.SetActive (true);
	}

	private void hide() {
		gameObject.SetActive (false);
	}
}