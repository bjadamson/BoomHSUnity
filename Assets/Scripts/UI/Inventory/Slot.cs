using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using player;

namespace ui.inventory
{
	public class Slot : MonoBehaviour, IDropHandler
	{
		public void OnDrop(PointerEventData eventData)
		{
			var dragTransform = DragHandler.itemBeingDragged.transform;
			var draggedIndex = dragTransform.GetSiblingIndex();
			var transformIndex = transform.GetSiblingIndex();

			dragTransform.GetChild(0).SetParent (transform);
			transform.GetChild(0).SetParent(dragTransform);
			//
			//if (dragTransform.parent != transform.parent)
			//{
			//	var dragParent = dragTransform.parent;
			//	dragTransform.transform.GetChild(0).SetParent(transform.parent);
			//	transform.GetChild(0).SetParent(dragParent);
			//}
			//dragTransform.SetSiblingIndex(transformIndex);
			//transform.SetSiblingIndex(draggedIndex);
			//ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
		}
	}
}