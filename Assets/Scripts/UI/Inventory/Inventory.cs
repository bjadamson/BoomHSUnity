using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using weapon;
using ui;

namespace ui.inventory
{
	public class Inventory : MonoBehaviour
	{
		private EquipppedItems equipped;
		private QuickbarItems quickbar;
		private InventoryItems inventory;
		private UIManager uiManager;

		void Start()
		{
			equipped = GetComponent<EquipppedItems>();
			Debug.Assert(equipped != null);

			quickbar = GetComponent<QuickbarItems>();
			Debug.Assert(quickbar != null);

			inventory = GetComponent<InventoryItems>();
			Debug.Assert(inventory != null);

			uiManager = GetComponent<UIManager>();
			Debug.Assert(uiManager != null);
		}

		public void addItem(WeaponModel item, int? equippedPosition)
		{
			if (equippedPosition.HasValue)
			{
				bool updateUiIcon = true;
				equipped.equipItem(equippedPosition.Value, item, updateUiIcon);
			}
			else
			{
				bool setUiIcon = true;
				int? qslot = quickbar.availableQuickbarSlot();
				if (qslot.HasValue)
				{
					quickbar.addItem(qslot.Value, item, setUiIcon);
				}
				else
				{
					inventory.addInventoryItem(item, setUiIcon);
				}
			}
		}

		public UiSlot itemIdToInventoryItem(int index)
		{
			if (index < equipped.Length)
			{
				return equipped[index];
			}
			index -= equipped.Length;
			if (index < quickbar.Length)
			{
				return quickbar[index];
			}
			index -= quickbar.Length;
			if (index < inventory.Count)
			{
				return inventory[index];
			}
			throw new NotImplementedException();
		}

		public bool isItemIndexEquipped(int index)
		{
			return equipped.sashIndexHasItem(index);
		}
	}
}