using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using weapon;
using ui;

namespace ui.inventory
{
	public class InventoryItems : MonoBehaviour, IItemStorage
	{
		[SerializeField] UiSlot[] inventoryItems;
		private IList<WeaponModel> weaponModels;
		private UIManager uiManager;

		public int Count
		{
			get { return inventoryItems.Length; }
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

			Debug.Assert(inventoryItems.Length > 0);
			weaponModels = new List<WeaponModel>(inventoryItems.Length);
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

			weaponModels.Add(item);
	
			int index = nextPosition.Value;
			inventoryItems[index].setIcon(item.WeaponBehavior.Icon);
		}

		private int? nextAvailableInventoryPosition()
		{
			int maxItemCount = weaponModels.Count;
			int itemCount = weaponModels.Count;
			if (itemCount <= maxItemCount)
			{
				return itemCount;
			}
			for (int i = 0; i < itemCount; ++i)
			{
				if (weaponModels[i] == null)
				{
					return i;
				}
			}
			return null;
		}

		#region IItemStorage implementation

		public WeaponModel getItem(int index)
		{
			Debug.Assert(index < weaponModels.Count);
			return weaponModels[index];
		}

		public UiSlot getUi(int index)
		{
			Debug.Assert(index < inventoryItems.Length);
			return inventoryItems[index];
		}

		public WeaponModel replaceItem(int index, WeaponModel newItem)
		{
			var old = getItem(index);
			weaponModels[index] = newItem;
			return old;
		}

		#endregion
	}
}