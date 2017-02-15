﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tabs : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	[SerializeField] public MouseoverManager manager;
	public int panelId;
	public Button button;

	public bool initiallyTransparent = true;
	public float mouseOverTransparency = 0.25f;

	void Start() {
		if (initiallyTransparent) {
			makeTransparent ();
		} else {
			makeOpaque ();
		}
	}

	void IPointerEnterHandler.OnPointerEnter (PointerEventData eventData) {
		manager.onTabMouseOverEnter (this, panelId);
	}

	void IPointerExitHandler.OnPointerExit (PointerEventData eventData) {
		manager.onTabMouseOverExit (this, panelId);
	}

	public void OnPointerClick (PointerEventData eventData) {
		manager.onTabMouseClicked (this, panelId);
	}
	public void makeTransparent () {
		Color color = button.image.color;
		color.a = mouseOverTransparency;
		button.image.color = color;
	}

	public void makeOpaque () {
		Color color = button.image.color;
		color.a = 1.0f;
		button.image.color = color;
	}

	public string text() {
		return gameObject.name;
	}
}