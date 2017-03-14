using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using player;

namespace ui.inventory
{
	public class Slot : MonoBehaviour, IDropHandler
	{
		//[SerializeField] private UIManager uiManager;
		[SerializeField] private PlayerBehavior playerBehavior;
		[SerializeField] private Inventory inventory;

		public void OnDrop(PointerEventData eventData)
		{
			var dragTransform = DragHandler.itemBeingDragged.transform;
			var a = dragTransform.GetComponent<Slot>();
			var b = transform;
			Debug.Assert(a != null);
			Debug.Assert(a != b);

			// swap their parent GO's
			var aParent = a.transform.parent;
			var bParent = b.transform.parent;

			var x = aParent.GetSiblingIndex();
			var y = bParent.GetSiblingIndex();
			Debug.Log("swapping " + x + " " + y);

			a.transform.SetParent (bParent);
			a.transform.SetAsFirstSibling();
			a.transform.localPosition = Vector3.zero;

			b.transform.SetParent (aParent);
			b.transform.SetAsFirstSibling();
			b.transform.localPosition = Vector3.zero;

			Debug.Assert(x != y);
			inventory.swapPositions(x, y);
		}

		public void readParentsSiblingIndexThenEquipWeaponAtThatIndex()
		{
			int index = transform.parent.GetSiblingIndex();
			Debug.Log("parent index: " + index);
			playerBehavior.ifWeaponAtPositionThenEquip(index);
		}
	}
}