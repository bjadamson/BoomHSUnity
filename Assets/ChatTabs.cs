using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatTabs : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	[SerializeField] private ChatTextPanel panel;
	[SerializeField] private ChatScrollView scrollView;
	[SerializeField] private ChatPaneManager manager;

	[SerializeField] private bool initiallyTransparent = true;
	[SerializeField] private float mouseOverTransparency = 0.25f;

	private Button button;
	private ChatTextPanel clicked;
	private ChatTextPanel active;

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
		scrollView.makeOpaque ();

		active = manager.getActive ();
		manager.makeActive (this.panel);
	}

	void IPointerExitHandler.OnPointerExit (PointerEventData eventData) {
		if (clicked && clicked != active) {
			manager.makeActive (clicked);
		} else {
			manager.makeActive (active);
		}

		if (!isActive()) {
			makeTransparent ();
		}

		// regardless of activeness, we do this when the mouse leaves
		scrollView.makeTransparent ();
	}

	public void OnPointerClick (PointerEventData eventData) {
		clicked = this.panel;
		makeActive ();
	}

	private void makeActive() {
		manager.makeActive (this.panel);
	}

	private bool isActive() {
		return manager.isActive (this.panel);
	}

	private void makeTransparent () {
		Color color = button.image.color;
		color.a = mouseOverTransparency;
		button.image.color = color;

		//scrollView.makeTransparent ();
	}

	void makeOpaque () {
		Color color = button.image.color;
		color.a = 1.0f;
		button.image.color = color;

		//scrollView.makeOpaque ();
	}
}