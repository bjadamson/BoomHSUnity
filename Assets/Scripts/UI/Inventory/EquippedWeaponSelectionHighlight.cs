using UnityEngine;
using UnityEngine.UI;
using ui;

namespace ui.inventory
{
	public class EquippedWeaponSelectionHighlight : MonoBehaviour
	{
		[SerializeField] private InventoryItem initialSlot;

		void Start()
		{
			Debug.Assert(initialSlot != null);
			transform.position = initialSlot.transform.position;
			transform.localPosition = Vector3.zero;
		}

		public bool isParentedBy(InventoryItem item)
		{
			Debug.Assert(item != null);
			return transform.parent == item.transform;
		}
	}
}