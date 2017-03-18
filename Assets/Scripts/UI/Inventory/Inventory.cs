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
				equipped.equipItem(equippedPosition.Value, item);
			}
			else
			{
				int? qslot = quickbar.availableQuickbarSlot();
				if (qslot.HasValue)
				{
					quickbar.addItem(qslot.Value, item);
				}
				else
				{
					inventory.addInventoryItem(item);
				}
			}
		}

		public void setInventoryItemId(int inventoryId)
		{
			var lookup = lookupItemById(inventoryId);
			var ui = lookup.Storage.getUi(lookup.Index);
			Debug.Assert(ui != null);
			ui.InventoryId = inventoryId;
		}

		public void swapItems(int i0, int i1)
		{
			var lookup0 = lookupItemById(i0);
			var lookup1 = lookupItemById(i1);

			var item0 = lookup0.Storage.getItem(lookup0.Index);
			var item1 = lookup1.Storage.getItem(lookup1.Index);

			lookup0.Storage.replaceItem(lookup0.Index, item1);
			lookup1.Storage.replaceItem(lookup1.Index, item0);
		}

		public ItemLookupResult lookupItemById(int index)
		{
			if (index < equipped.Length)
			{
				Debug.Log("index: " + index);
				return new ItemLookupResult(index, equipped);
			}
			index -= equipped.Length;
			if (index < quickbar.Length)
			{
				return new ItemLookupResult(index, quickbar);
			}
			index -= quickbar.Length;
			if (index < inventory.Count)
			{
				return new ItemLookupResult(index, inventory);
			}
			throw new NotImplementedException();
		}

		public bool isItemIndexEquipped(int index)
		{
			return equipped.sashIndexHasItem(index);
		}
	}

	public class ItemLookupResult
	{
		public readonly int Index;
		public readonly IItemStorage Storage;

		public ItemLookupResult(int index, IItemStorage storage)
		{
			this.Index = index;
			this.Storage = storage;
		}
	}
}