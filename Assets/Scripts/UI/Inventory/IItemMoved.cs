using UnityEngine;
using UnityEngine.UI;

namespace ui.inventory
{
	interface IItemMoved
	{
		void onItemMoved(int index0, int index1);
	}
}