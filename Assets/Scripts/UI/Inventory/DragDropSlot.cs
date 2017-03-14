using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using player;

namespace ui.inventory
{
	public class DragDropSlot : MonoBehaviour, IDropHandler
	{
		[SerializeField] private ItemHighlight itemHighlight;
		[SerializeField] private PlayerBehavior playerBehavior;
		[SerializeField] private WeaponBarDragManager weaponManager;

		void Start()
		{
			//Debug.Assert(itemHighlight != null);
		//	Debug.Assert(playerBehavior != null);
			//Debug.Assert(weaponManager != null);
		}

		public void OnDrop(PointerEventData eventData)
		{
			var dragTransform = DragHandler.itemBeingDragged.transform;
			var a = dragTransform.GetComponent<DragDropSlot>();
			var b = transform;
			Debug.Assert(a != null);
			Debug.Assert(a != b);

			// swap their parent GO's
			var aParent = a.transform.parent;
			var bParent = b.transform.parent;

			var x = aParent.GetSiblingIndex();
			var y = bParent.GetSiblingIndex();

			a.transform.SetParent (bParent);
			a.transform.SetAsFirstSibling();
			a.transform.localPosition = Vector3.zero;

			b.transform.SetParent (aParent);
			b.transform.SetAsFirstSibling();
			b.transform.localPosition = Vector3.zero;

			Debug.Assert(x != y);
			weaponManager.onItemMoved(x, y);

			if (itemHighlight.transform.parent == transform.parent)
			{
				// drag highlight onto other
				Debug.Log("drag highlight onto other");
				playerBehavior.equipItemAtPosition(transform.parent.GetSiblingIndex());
			}
			else if (itemHighlight.transform.parent == dragTransform.parent)
			{
				// dragged ONTO highlighted
				Debug.Log("dragged ONTO highlighted");
				playerBehavior.equipItemAtPosition(dragTransform.parent.GetSiblingIndex());
			}
		}

		public void readParentsSiblingIndexThenEquipWeaponAtThatIndex()
		{
			int index = transform.parent.GetSiblingIndex();
			playerBehavior.ifWeaponAtPositionThenEquip(index);
		}
	}
}