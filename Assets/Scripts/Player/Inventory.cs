using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using weapon;
using ui;

namespace player
{
	public class Inventory : MonoBehaviour
	{
		// ui
		[SerializeField] private UIManager uiManager;
		private WeaponModel[] equippedItems = new WeaponModel[4];
		private WeaponModel[] quickbarItems = new WeaponModel[6];

		private IList<WeaponModel> inventoryItems = new List<WeaponModel>();

		// TODO: HACK REMOVAL. This shouldn't be defined in two places (it's defined in the UI visually currently also).
		// Either generate the "Items" on demand, or know ahead of time how many we'll need tops.
		private int MAX_INVENTORY_ITEM_COUNT = 20;

		public WeaponModel getEquippedItem(int index)
		{
			return equippedItems[index];
		}

		// TESTING
		/*
		public void swapEquippedItems(int i0, int i1)
		{
			Debug.Assert(i0 != i1);
			Debug.Assert(i0 < equippedItems.Length && i1 < equippedItems.Length);

			var temp = equippedItems[i0];
			equippedItems[i0] = equippedItems[i1];
			equippedItems[i1] = temp;

			//uiManager.setInventoryItem(i0, equippedItems[i0].WeaponBehavior.Icon, 1.0f);
			//uiManager.setInventoryItem(i1, equippedItems[i1].WeaponBehavior.Icon, 1.0f);
			//equipItem(i0, getEquippedItem(i1));
			//equipItem(i1, temp);
		}
		*/

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

		public void equipItem(int index, WeaponModel item)
		{
			Debug.Assert(index < equippedItems.Length);
			equippedItems[index] = item;
		}

		public void addInventoryItem(WeaponModel item)
		{
			Debug.Assert(item != null);
			Debug.Assert(item.WeaponBehavior != null);
			Debug.Assert(item.WeaponBehavior.Icon != null);

			var nextPosition = nextAvailableInventoryPosition();
			if (!nextPosition.HasValue)
			{
				throw new NotImplementedException();
			}

			inventoryItems.Add(item);
			uiManager.setInventoryItem(nextPosition.Value, item.WeaponBehavior.Icon, 1.0f);
		}

		public int? availableQuickbarSlot()
		{
			for (int i = 0; i < quickbarItems.Length; ++i)
			{
				if (quickbarItems[i] == null)
				{
					return i;
				}
			}
			return null;
		}

		public void addQuickbarItem(int index, WeaponModel item)
		{
			Debug.Assert(index < quickbarItems.Length);
			quickbarItems[index] = item;

			uiManager.setQuickbarItem(index, item.WeaponBehavior.Icon, 1.0f);
		}

		public bool sashIndexHasItem(int position)
		{
			return getEquippedItem(position) != null;
		}

		private int? nextAvailableInventoryPosition()
		{
			int itemCount = inventoryItems.Count;
			if (itemCount <= MAX_INVENTORY_ITEM_COUNT)
			{
				return itemCount;
			}
			for (int i = 0; i < MAX_INVENTORY_ITEM_COUNT; ++i)
			{
				if (inventoryItems[i] == null)
				{
					return i;
				}
			}
			return null;
		}
	}
}