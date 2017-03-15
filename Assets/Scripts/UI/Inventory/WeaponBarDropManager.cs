using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using player;
using ui;

namespace ui.inventory
{
	public class WeaponBarDropManager : MonoBehaviour
	{
		[SerializeField] private EquippedWeaponHighlight weaponHighlight;
		[SerializeField] private PlayerBehavior playerBehavior;
		private Inventory inventory;

		void Start()
		{
			Debug.Assert(weaponHighlight != null);
			Debug.Assert(playerBehavior != null);

			inventory = GetComponent<Inventory>();
			Debug.Assert(inventory != null);
		}

		public void OnWeaponBarDropped(Transform slotTransform, Transform itemBeingDragged)
		{
			//var dragTransform = DragHandler.itemBeingDragged.transform;
			if (weaponHighlight.transform.parent == slotTransform.parent)
			{
				// drag highlight onto other
				playerBehavior.equipItemAtPosition(slotTransform.parent.GetSiblingIndex());
			}
			else if (weaponHighlight.transform.parent == itemBeingDragged.parent)
			{
				// dragged ONTO highlighted
				playerBehavior.equipItemAtPosition(itemBeingDragged.parent.GetSiblingIndex());
			}
		}

		public void equipWeaponAtThatIndexIfAny(int index)
		{
			playerBehavior.ifWeaponAtPositionThenEquip(index);
		}
	}
}