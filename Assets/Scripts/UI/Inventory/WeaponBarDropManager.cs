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
			if (weaponHighlight.Index == slotTransform.parent.GetSiblingIndex())
			{
				// drag highlight onto other
				int index = slotTransform.parent.GetSiblingIndex();
				moveHighlightTo(itemBeingDragged, index);
			}
			else if (weaponHighlight.Index == itemBeingDragged.parent.GetSiblingIndex())
			{
				// dragged ONTO highlighted
				int index = itemBeingDragged.parent.GetSiblingIndex();
				moveHighlightTo(slotTransform, index);
			}
		}

		public void equipWeaponAtThatIndexIfAny(int index)
		{
			playerBehavior.ifWeaponAtPositionThenEquip(index);
		}

		private void moveHighlightTo(Transform itemTransform, int index)
		{
			playerBehavior.equipItemAtPosition(index);

			weaponHighlight.transform.position = itemTransform.position;
			weaponHighlight.transform.localPosition = Vector3.zero;
			weaponHighlight.Index = index;
		}
	}
}