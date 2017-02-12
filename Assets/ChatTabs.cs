using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatTabs : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	[SerializeField] private float mouseOverTransparency = 0.25f;

	private Button button;

	void Start() {
		button = GetComponent<Button> ();
	}

	void IPointerEnterHandler.OnPointerEnter (PointerEventData eventData)
	{
		Color color = button.image.color;
		color.a = 1.0f;
		button.image.color = color;
	}

	void IPointerExitHandler.OnPointerExit (PointerEventData eventData)
	{
		Color color = button.image.color;
		color.a = mouseOverTransparency;
		button.image.color = color;
	}
}