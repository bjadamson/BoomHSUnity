using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatScrollBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	// config, move out
	[SerializeField] private bool forceChatScrollBarToBottomAfterSubmit = false;

	private Scrollbar scrollbar;
	private float previousValue = 0.0f;

	void Start() {
		scrollbar = GetComponent<Scrollbar> ();

		// This is something to look into, I have to delay setting the scroll bar's initial value until the end of the frame or else the scroll bar's
		// postition is always reset to the top. This may be a hack/kludge.
		StartCoroutine(LateStart ());
	}

	private IEnumerator LateStart() {
		yield return new WaitForEndOfFrame ();
		scrollbar.value = 0.0f;
		hide ();
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