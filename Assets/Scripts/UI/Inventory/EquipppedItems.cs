using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using weapon;
using ui;

namespace ui.inventory
{
	public class EquipppedItems : MonoBehaviour, IItemStorage
	{
		[SerializeField] private UiSlot[] inventoryItems;
		private WeaponModel[] equippedItems;
		private UIManager uiManager;

		void Start()
		{
			uiManager = GetComponent<UIManager>();
			Debug.Assert(uiManager != null);

			equippedItems = new WeaponModel[inventoryItems.Length];
		}

		public int Length
		{
			get { return equippedItems.Length; }
		}

		public UiSlot this [int index]
		{
			get
			{
				return inventoryItems[index];
			}
		}

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

		public void equipItem(int index, WeaponModel item)
		{
			Debug.Assert(index < equippedItems.Length);
			equippedItems[index] = item;

			inventoryItems[index].setIcon(item.WeaponBehavior.Icon);
		}

		public bool sashIndexHasItem(int position)
		{
			return getEquippedItem(position) != null;
		}

		#region IItemStorage implementation

		public WeaponModel getItem(int index)
		{
			Debug.Assert(index < equippedItems.Length);
			return equippedItems[index];
		}

		public UiSlot getUi(int index)
		{
			Debug.Assert(index < inventoryItems.Length);
			Debug.Log("getGui: " + index);
			return inventoryItems[index];
		}

		public WeaponModel replaceItem(int index, WeaponModel newItem)
		{
			var old = getItem(index);
			equippedItems[index] = newItem;
			return old;
		}

		#endregion
	}
}