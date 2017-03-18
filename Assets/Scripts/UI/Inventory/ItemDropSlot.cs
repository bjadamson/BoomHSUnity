using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using player;

namespace ui.inventory
{
	public class ItemDropSlot : MonoBehaviour, IDropHandler
	{
		[SerializeField] private PlayerBehavior playerBehavior;

		void Start()
		{
			Debug.Assert(playerBehavior != null);
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

			a.transform.SetParent(bParent);
			a.transform.SetAsFirstSibling();
			a.transform.localPosition = Vector3.zero;

			b.transform.SetParent(aParent);
			b.transform.SetAsFirstSibling();
			b.transform.localPosition = Vector3.zero;

			int x = a.GetComponent<UiSlot>().InventoryId;
			int y = b.GetComponent<UiSlot>().InventoryId;
			Debug.Log("Switching inventory ids: '" + x + "' , '" + y + "'");
			Debug.Assert(x != y);
			playerBehavior.swapItems(x, y);
		}

		public void emitInventoryId()
		{
			Debug.Log("InventoryId: '" + GetComponent<UiSlot>().InventoryId + "'");
		}
	}
}