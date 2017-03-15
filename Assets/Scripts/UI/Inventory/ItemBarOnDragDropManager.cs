using UnityEngine;
using UnityEngine.UI;
using player; // TODO: move inventory to ui
using ui;

namespace ui.inventory
{
	public class ItemBarOnDragDropManager : MonoBehaviour, IItemMoved
	{
		[SerializeField] private Inventory inventory;

		void swapEquippedItems(int index0, int index1)
		{
			var item0 = this.inventory.getEquippedItem(index0);
			var item1 = this.inventory.getEquippedItem(index1);

			inventory.equipItem(index0, item1);
			inventory.equipItem(index1, item0);
		}

		public void onItemMoved(int index0, int index1)
		{
			swapEquippedItems(index0, index1);
		}
	}
}