using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	private float opaqueness = 0.55f;
	private float transparency = 0.10f;
	private Image image;

	void Start() {
		image = GetComponent<Image> ();
		makeTransparent ();
	}
	void IPointerEnterHandler.OnPointerEnter (PointerEventData eventData) {
		makeOpaque ();
	}

	void IPointerExitHandler.OnPointerExit (PointerEventData eventData) {
		makeTransparent ();
	}

	public void makeTransparent () {
		Color color = image.color;
		color.a = transparency;
		image.color = color;
	}

	public void makeOpaque () {
		Color color = image.color;
		color.a = opaqueness;
		image.color = color;
	}
}