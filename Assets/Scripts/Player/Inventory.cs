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
		private WeaponModel[] equippedItems = new WeaponModel[10];

		private IList<WeaponModel> inventoryItems = new List<WeaponModel>();
		private int maxItemCount = 10;

		public WeaponModel getEquippedItem(int index)
		{
			return equippedItems[index];
		}

		public void swapPositions(int i0, int i1)
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

			uiManager.setSashItem(index, item.WeaponBehavior.Icon, 1.0f);
		}

		public void addInventoryItem(WeaponModel item)
		{
			var nextPosition = nextAvailableInventoryPosition();
			Debug.Log("nextPos: " + nextPosition);
			if (!nextPosition.HasValue)
			{
				throw new NotImplementedException();
			}

			inventoryItems.Add(item);
			Debug.Assert(item.WeaponBehavior.Icon != null);
			Debug.Assert(item.WeaponBehavior.Icon != null);


			//if (inventoryItems.Count < 8)
			//{
			//	uiManager.setSashItem(nextPosition.Value, item.WeaponBehavior.Icon, 1.0f);
			//}
			//else
			//{
			//}
			uiManager.setInventoryItem(nextPosition.Value, item.WeaponBehavior.Icon, 1.0f);
		}

		public bool sashIndexHasItem(int position)
		{
			return getEquippedItem(position) != null;
		}

		private int? nextAvailableInventoryPosition()
		{
			int itemCount = inventoryItems.Count;
			if (itemCount <= maxItemCount)
			{
				return itemCount;
			}
			for (int i = 0; i < maxItemCount; ++i)
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