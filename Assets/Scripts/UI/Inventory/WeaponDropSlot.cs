using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using player;

namespace ui.inventory
{
	public class WeaponDropSlot : MonoBehaviour, IDropHandler
	{
		[SerializeField] private WeaponBarDropManager weaponDropManager;

		void Start()
		{
			Debug.Assert(weaponDropManager != null);
		}

		public void OnDrop(PointerEventData eventData)
		{
			weaponDropManager.OnWeaponBarDropped(transform, DragHandler.itemBeingDragged.transform);
		}

		public void readParentsSiblingIndexThenEquipWeaponAtThatIndex()
		{
			int index = transform.parent.GetSiblingIndex();
			weaponDropManager.equipWeaponAtThatIndexIfAny(index);
		}
	}
}