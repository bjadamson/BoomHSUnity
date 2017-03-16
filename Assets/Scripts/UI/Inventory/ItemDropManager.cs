using UnityEngine;
using UnityEngine.UI;
using player;

namespace ui.inventory
{
	public class ItemDropManager : MonoBehaviour, IItemMoved
	{
		private EquipppedItems equippedItems;

		void Start()
		{
			equippedItems = GetComponent<EquipppedItems>();
			Debug.Assert(equippedItems != null);
		}

		public void onEquippedItemMoved(int index0, int index1)
		{
			swapEquippedItems(index0, index1);
		}

		private void swapEquippedItems(int index0, int index1)
		{
			var item0 = this.equippedItems.getEquippedItem(index0);
			var item1 = this.equippedItems.getEquippedItem(index1);

			bool setUiIcon = false;
			equippedItems.equipItem(index0, item1, setUiIcon);
			equippedItems.equipItem(index1, item0, setUiIcon);
		}
	}
}