﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatAlpha : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField] private Image panel;
	[SerializeField] private float mouseOverAlpha = 0.60f;

	private Color idleColor;
	private Color activeColor;

	void Start() {
		idleColor = panel.color;
	}

	private void updateColor(Color color) {
		activeColor = color;
		panel.color = color;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Color color = idleColor;
		color.a = mouseOverAlpha;
		updateColor (color);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		updateColor (idleColor);
	}
}