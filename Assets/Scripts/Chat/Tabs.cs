using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tabs : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	[SerializeField] public MouseoverManager manager;
	[SerializeField] public int panelId;

	[SerializeField] private bool initiallyTransparent = true;
	[SerializeField] private float mouseOverTransparency = 0.25f;

	private Button button;

	void Start() {
		button = GetComponent<Button> ();
		Debug.Assert (button != null);

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
		Debug.Assert (button != null);
		Debug.Assert (button.image != null);

		Color color = button.image.color;
		color.a = 1.0f;
		button.image.color = color;
	}
}