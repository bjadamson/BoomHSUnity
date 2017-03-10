using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
	public class UIManager : MonoBehaviour
	{
		// player panel
		[SerializeField] private Slider healthSlider;
		[SerializeField] private Slider manaSlider;
		[SerializeField] private Slider expMinorSlider;
		[SerializeField] private Slider expMajorSlider;

		// ammo panel
		[SerializeField] private Text ammoCountText;
		[SerializeField] private Image ammoIcon;

		// buff panel
		[SerializeField] private Image[] buffIcons;

		// inventory panel
		[SerializeField] private Image[] inventoryIcons;
		[SerializeField] private Text[] inventoryKeybingShortcutText;

		void Start()
		{
			Debug.Assert(inventoryIcons.Length == inventoryKeybingShortcutText.Length);
		}

		public void setAmmo(int magazineCount, int magazineMax)
		{
			ammoCountText.text = magazineCount + " / " + magazineMax;
		}

		public void setAmmoIcon(Image icon)
		{
			ammoIcon = icon;
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

		public void setItem(int index, Image icon)
		{
			Debug.Assert(index < inventoryIcons.Length);
			inventoryIcons[index] = icon;
		}

		public void setBuffIcon(int index, Image icon)
		{
			Debug.Assert(index < buffIcons.Length);
			buffIcons[index] = icon;
		}
	}
}