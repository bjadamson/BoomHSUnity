using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollBackgroundAndHandle : MonoBehaviour {
	private float opaqueness = 0.55f;
	private float transparency = 0.10f;
	[SerializeField] private Image background;
	[SerializeField] private Image handle;

	public void makeTransparent () {
		Color color = background.color;
		color.a = transparency;
		background.color = color;

		color.a = 0.0f;
		handle.color = color;
	}

	public void makeOpaque () {
		Color color = background.color;
		color.a = opaqueness;
		background.color = color;

		color.a = 1.0f;
		handle.color = color;
	}
}