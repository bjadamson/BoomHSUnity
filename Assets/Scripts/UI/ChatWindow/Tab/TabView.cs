﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ui
{
	namespace chat_window
	{
		namespace tab
		{
			public class TabView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
			{
				[SerializeField] public TabViewManager manager;
				public int panelId;
				public Button button;

				public bool initiallyTransparent = true;
				public float mouseOverTransparency = 0.25f;

				void Start ()
				{
					if (initiallyTransparent) {
						makeTransparent ();
					} else {
						makeOpaque ();
					}
				}

				void IPointerEnterHandler.OnPointerEnter (PointerEventData eventData)
				{
					manager.mouseOverTabEnter (this, panelId);
				}

				void IPointerExitHandler.OnPointerExit (PointerEventData eventData)
				{
					manager.mouseOverTabExit (this, panelId);
				}

				public void OnPointerClick (PointerEventData eventData)
				{
					if (eventData.button == PointerEventData.InputButton.Left) {
						manager.mouseLeftClickedOnTab (this, panelId);
					} else {
						manager.mouseRightClickedOnTab (this, eventData.position);
					}
				}

				public void makeTransparent ()
				{
					Color color = button.image.color;
					color.a = mouseOverTransparency;
					button.image.color = color;
				}

				public void makeOpaque ()
				{
					Color color = button.image.color;
					color.a = 1.0f;
					button.image.color = color;
				}

				public void setText (string value)
				{
					GetComponentInChildren<Text> ().text = value;
				}

				public string text ()
				{
					return GetComponentInChildren<Text> ().text;
				}
			}
		}
	}
}