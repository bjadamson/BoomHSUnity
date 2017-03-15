using UnityEngine;
using UnityEngine.UI;
using player;

namespace ui.inventory
{
	public class ItemDropManager : MonoBehaviour, IItemMoved
	{
		private Inventory inventory;

		void Start()
		{
			inventory = GetComponent<Inventory>();
			Debug.Assert(inventory != null);
		}

		public void onEquippedItemMoved(int index0, int index1)
		{
			swapEquippedItems(index0, index1);
		}

		private void swapEquippedItems(int index0, int index1)
		{
			var item0 = this.inventory.getEquippedItem(index0);
			var item1 = this.inventory.getEquippedItem(index1);

			inventory.equipItem(index0, item1);
			inventory.equipItem(index1, item0);
		}
	}
}