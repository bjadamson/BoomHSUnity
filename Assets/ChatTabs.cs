using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatTabs : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	[SerializeField] private GameObject panel;
	[SerializeField] private ChatPaneManager manager;

	[SerializeField] private bool initiallyTransparent = true;
	[SerializeField] private float mouseOverTransparency = 0.25f;

	private Button button;

	void Start() {
		button = GetComponent<Button> ();

		if (initiallyTransparent) {
			makeTransparent ();
		} else {
			makeOpaque ();
		}
	}

	void IPointerEnterHandler.OnPointerEnter (PointerEventData eventData) {
		makeOpaque ();
	}

	void IPointerExitHandler.OnPointerExit (PointerEventData eventData) {
		if (!isActive()) {
			makeTransparent ();
		}
	}

	public void OnPointerClick (PointerEventData eventData) {
		manager.makeActive (this.panel);
	}

	private bool isActive() {
		return manager.isActive (this.panel);
	}

	private void makeTransparent () {
		Color color = button.image.color;
		color.a = mouseOverTransparency;
		button.image.color = color;
	}

	void makeOpaque () {
		Color color = button.image.color;
		color.a = 1.0f;
		button.image.color = color;
	}
}