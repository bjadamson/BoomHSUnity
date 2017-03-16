using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using weapon;
using ui;

namespace ui.inventory
{
	public class QuickbarItems : MonoBehaviour
	{
		[SerializeField] private UiSlot[] inventoryItems;
		private WeaponModel[] quickbarItems;
		private UIManager uiManager;

		public int Length
		{
			get { return quickbarItems.Length; }
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
			Debug.Assert(inventoryItems != null);
			quickbarItems = new WeaponModel[inventoryItems.Length];

			uiManager = GetComponent<UIManager>();
			Debug.Assert(uiManager != null);
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

		public void addItem(int index, WeaponModel item, bool setUiIcon)
		{
			Debug.Assert(index < quickbarItems.Length);
			quickbarItems[index] = item;

			if (setUiIcon)
			{
				inventoryItems[index].setUiIndexAndIcon(index, item.WeaponBehavior.Icon);
			}
		}
	}
}