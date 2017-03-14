using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ui.inventory;
using player;

namespace ui
{
	public class UIManager : MonoBehaviour
	{
		// Player
		[SerializeField] private PlayerBehavior playerBehavior;

		// io
		[SerializeField] private CursorManager cursorManager;

		// player panel
		[SerializeField] private Slider healthSlider;
		[SerializeField] private Slider manaSlider;
		[SerializeField] private Slider expMinorSlider;
		[SerializeField] private Slider expMajorSlider;

		// ammo panel
		[SerializeField] private AmmoPanel ammoPanel;

		// crosshair panel
		[SerializeField] private HitIndicator HitIndicator;

		// buff panel
		[SerializeField] private Image[] buffIcons;

		// inventory panel
		[SerializeField] private GameObject inventoryPanel;
		[SerializeField] private GameObject sashPanelHighlight;

		[SerializeField] private InventoryItem[] inventoryItems;
		[SerializeField] private InventoryItem[] quickbarItems;
		[SerializeField] private InventoryItem[] weaponItems;

		void Update()
		{
			if (playerBehavior.isWeaponEquipped())
			{
				ammoPanel.gameObject.SetActive(true);
				setAmmo(playerBehavior.equippedWeaponAmmoCount(), playerBehavior.equippedWeaponMaxAmmo());
			}
			else
			{
				ammoPanel.gameObject.SetActive(false);
			}
		}

		public InventoryItem itemIdToInventoryItem(int index)
		{
			if (index < weaponItems.Length)
			{
				return weaponItems[index];
			}
			index -= weaponItems.Length;
			if (index < quickbarItems.Length)
			{
				return quickbarItems[index];
			}
			index -= quickbarItems.Length;
			if (index < inventoryItems.Length)
			{
				return inventoryItems[index];
			}
			throw new NotImplementedException();
		}

		public void setAmmo(int magazineCount, int magazineMax)
		{
			ammoPanel.setText(magazineCount + " / " + magazineMax);
		}

		public void setAmmoIcon(Image icon)
		{
			ammoPanel.setImage(icon);
		}

		public void setHealth(int value)
		{
			healthSlider.value = value;
		}

		public void setMana(int value)
		{
			manaSlider.value = value;
		}

		public void setExperience(int minor, int major)
		{
			setExperienceMinor(minor);
			setExperienceMajor(major);
		}

		public void setExperienceMinor(int value)
		{
			expMinorSlider.value = value;
		}

		public void setExperienceMajor(int value)
		{
			expMajorSlider.value = value;
		}

		public void setInventoryItem(int index, Sprite icon, float alpha)
		{
			setContainerItem(inventoryItems, index, icon, alpha);
		}

		public void showInventory()
		{
			inventoryPanel.GetComponent<CanvasGroup>().alpha = 1;
		}

		public void hideInventory()
		{
			inventoryPanel.GetComponent<CanvasGroup>().alpha = 0;
		}

		public void toggleInventory()
		{
			var cg = inventoryPanel.GetComponent<CanvasGroup>();

			// flip the alpha between 0 and 1
			float a = cg.alpha;
			a = 1 - a;
			cg.alpha = a;

			cg.interactable = !cg.interactable;

			cursorManager.toggleCursor();
		}

		public void setWeaponItem(int index, Sprite icon, float alpha)
		{
			setContainerItem(weaponItems, index, icon, alpha);
		}

		public void setQuickbarItem(int index, Sprite icon, float alpha)
		{
			setContainerItem(quickbarItems, index, icon, alpha);
		}

		public void setBuffIcon(int index, Image icon)
		{
			Debug.Assert(index < buffIcons.Length);
			buffIcons[index] = icon;
		}

		public void showThenHideHitIndicator()
		{
			this.HitIndicator.showThenHideHitIndicator();
		}

		public void setHighlight(Transform newParent)
		{
			sashPanelHighlight.transform.SetParent(newParent);
			sashPanelHighlight.transform.localPosition = Vector3.zero;
		}

		private static void setContainerItem(InventoryItem[] items, int index, Sprite icon, float alpha)
		{
			Debug.Assert(items.Length > 0);
			Debug.Assert(index < items.Length);
			var item = items[index];
			Debug.Assert(item != null);
			Debug.Assert(item.ImageButton != null);

			var button = item.ImageButton;
			Debug.Assert(button != null);
			Debug.Assert(button.image != null);

			button.image.sprite = icon;
			item.InventoryId = index;

			var color = button.colors.normalColor;
			color.a = alpha;
			button.image.color = color;

			item.refreshChildren();
		}
	}
}