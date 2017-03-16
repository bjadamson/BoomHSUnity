using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using weapon;
using ui;

namespace ui.inventory
{
	public class InventoryItems : MonoBehaviour
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

		public void addInventoryItem(WeaponModel item, bool setUiIcon)
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
			if (setUiIcon)
			{
				int index = nextPosition.Value;
				inventoryItems[index].setUiIndexAndIcon(index, item.WeaponBehavior.Icon);
			}
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
	}
}