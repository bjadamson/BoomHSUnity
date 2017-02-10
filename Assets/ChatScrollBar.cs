using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChatScrollBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	void Start() {
		hide ();
	}

	public void OnPointerEnter(PointerEventData eventData) {
		show ();
	}

	public void OnPointerExit(PointerEventData eventData) {
		hide ();
	}

	private void show() {
		gameObject.SetActive (true);
	}

	private void hide() {
		gameObject.SetActive (false);
	}
}