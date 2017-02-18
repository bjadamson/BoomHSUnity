using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuItemMouseover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	private GameObject mouseoverPanel;

	void Start() {
		mouseoverPanel = transform.Find ("MouseoverPanel").gameObject;
		hide ();
	}
		
	public void OnPointerEnter (PointerEventData eventData) {
		show ();
	}

	public void OnPointerExit (PointerEventData eventData) {
		hide ();
	}

	private void show() {
		mouseoverPanel.SetActive (true);
	}

	private void hide() {
		mouseoverPanel.SetActive (false);
	}
}