using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace ui.inventory
{
	public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public static GameObject itemBeingDragged;
		private CanvasGroup canvasGroup;
		Vector3 startPosition;
		Transform startParent;

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
		}

		public void OnDrag(PointerEventData eventData)
		{
			transform.position = eventData.position;
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