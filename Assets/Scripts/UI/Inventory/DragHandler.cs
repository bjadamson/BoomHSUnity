using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace ui.inventory
{
	public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public static GameObject itemBeingDragged;
		private CanvasGroup canvasGroup;
		private Vector3 mouseOffset, startPosition;
		private Transform startParent;

		void Start()
		{
			canvasGroup = GetComponent<CanvasGroup>();
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			itemBeingDragged = gameObject;
			startPosition = transform.position;
			startParent = transform.parent;
			canvasGroup.blocksRaycasts = false;

			mouseOffset = Input.mousePosition - startPosition;
		}

		public void OnDrag(PointerEventData eventData)
		{
			transform.position = eventData.position;
			transform.position -= mouseOffset;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			itemBeingDragged = null;
			canvasGroup.blocksRaycasts = true;
			if (transform.parent == startParent)
			{
				transform.position = startPosition;
			}
		}
	}
}