using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using player;

namespace ui.inventory
{
	public class Slot : MonoBehaviour, IDropHandler
	{
		public GameObject item
		{
			get
			{
				if (transform.childCount > 0)
				{
					return transform.GetChild(0).gameObject;
				}
				return null;
			}
		}

		public void OnDrop(PointerEventData eventData)
		{
			var dragTransform = DragHandler.itemBeingDragged.transform;
			var draggedIndex = dragTransform.GetSiblingIndex();

			//DragHandler.itemBeingDragged.transform.SetParent(transform);
			dragTransform.SetSiblingIndex(transform.GetSiblingIndex());
			transform.SetSiblingIndex(draggedIndex);
			//ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
		}
	}
}