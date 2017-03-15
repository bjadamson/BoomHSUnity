using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using player;

namespace ui.inventory
{
	public class ItemDropSlot : MonoBehaviour, IDropHandler
	{
		[SerializeField] private ItemDropManager itemDropManager;

		void Start()
		{
			Debug.Assert(itemDropManager != null);
		}

		public void OnDrop(PointerEventData _)
		{
			var dragTransform = DragHandler.itemBeingDragged.transform;
			var a = dragTransform.GetComponent<ItemDropSlot>();
			var b = transform;
			Debug.Assert(a != null);
			Debug.Assert(a != b);

			// swap their parent GO's
			var aParent = a.transform.parent;
			var bParent = b.transform.parent;

			var x = aParent.GetSiblingIndex();
			var y = bParent.GetSiblingIndex();

			a.transform.SetParent(bParent);
			a.transform.SetAsFirstSibling();
			a.transform.localPosition = Vector3.zero;

			b.transform.SetParent(aParent);
			b.transform.SetAsFirstSibling();
			b.transform.localPosition = Vector3.zero;

			Debug.Assert(x != y);
			itemDropManager.onItemMoved(x, y);
		}
	}
}