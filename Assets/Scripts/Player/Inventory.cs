using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
using ui;

namespace player
{
	public class Inventory : MonoBehaviour
	{
		// ui
		[SerializeField] private UIManager uiManager;
		private WeaponModel[] equippedItems = new WeaponModel[10];

		public WeaponModel getEquippedItem(int index)
		{
			return equippedItems[index];
		}

		public int? equippedItemPositionOnBody(WeaponModel model)
		{
			// WEIRD
			// Count the number of equipped items with meshes to get the index of the equipped item on the body..
			int equippedItemCount = 0;
			for (int i = 0; i < equippedItems.Length; ++i)
			{
				var item = equippedItems[i];

				if (item == model)
				{
					return equippedItemCount;
				}
				if (!item.hasMeshRenderer())
				{
					++equippedItemCount;
				}
			}
			return null;
		}

		public bool availableWeaponSlot()
		{
			for (int i = 0; i < equippedItems.Length; ++i)
			{
				if (equippedItems[i] == null)
				{
					return true;
				}
			}
			return false;
		}

		public void equipItem(WeaponModel weapon, int index)
		{
			Debug.Assert(index < equippedItems.Length);
			equippedItems[index] = weapon;

			uiManager.setItem(index, weapon.WeaponBehavior.Icon, 1.0f);
		}

		//public void addWeapon(WeaponModel weapon)
		//{
		//	items.Add(weapon);
		//}
	}
}