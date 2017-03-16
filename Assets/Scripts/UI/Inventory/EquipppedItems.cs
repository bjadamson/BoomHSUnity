using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using weapon;
using ui;

namespace ui.inventory
{
	public class EquipppedItems : MonoBehaviour
	{
		[SerializeField] private UiSlot[] inventoryItems = new UiSlot[4];
		private WeaponModel[] equippedItems = new WeaponModel[4];
		private UIManager uiManager;

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

		void Start()
		{
			uiManager = GetComponent<UIManager>();
			Debug.Assert(uiManager != null);
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

		public void equipItem(int index, WeaponModel item, bool setUiIcon)
		{
			Debug.Assert(index < equippedItems.Length);
			equippedItems[index] = item;

			if (setUiIcon)
			{
				inventoryItems[index].setUiIndexAndIcon(index, item.WeaponBehavior.Icon);
			}
		}

		public bool sashIndexHasItem(int position)
		{
			return getEquippedItem(position) != null;
		}
	}
}