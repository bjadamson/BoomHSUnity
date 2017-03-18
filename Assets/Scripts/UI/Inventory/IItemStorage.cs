using UnityEngine;
using UnityEngine.UI;
using weapon;

namespace ui.inventory
{
	public interface IItemStorage
	{
		WeaponModel getItem(int index);
		UiSlot getUi(int index);

		// Replaces the item stored at index with newItem, returning the value previously stored there.
		WeaponModel replaceItem(int index, WeaponModel newItem);
	}
}