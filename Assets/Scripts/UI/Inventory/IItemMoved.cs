using UnityEngine;
using UnityEngine.UI;

namespace ui.inventory
{
	interface IItemMoved
	{
		void onEquippedItemMoved(int index0, int index1);
	}
}