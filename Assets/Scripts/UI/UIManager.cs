using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ui.player.inventory;
using player;

namespace ui
{
	public class UIManager : MonoBehaviour
	{
		// Player
		[SerializeField] private PlayerBehavior playerBehavior;

		// ui-elements
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
		[SerializeField] private Image[] inventoryIcons;
		[SerializeField] private Text[] inventoryKeybingShortcutText;

		void Start()
		{
			Debug.Assert(inventoryIcons.Length == inventoryKeybingShortcutText.Length);
		}

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

		public void setItem(int index, Sprite icon)
		{
			Debug.Assert(index < inventoryIcons.Length);
			inventoryIcons[index].sprite = icon;
		}

		public void setItemAlpha(int index, float alpha)
		{
			Debug.Assert(index < inventoryIcons.Length);
			var icon = inventoryIcons[index];
			var color = icon.color;
			color.a = alpha;
			icon.color = color;
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
	}
}