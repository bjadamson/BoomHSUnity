using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
	public class WeaponModel
	{
		public readonly WeaponBehavior WeaponBehavior;
		public readonly string Name;
		public readonly bool IsFullyAutomatic;
		public readonly bool UsesPiercingRounds;

		public readonly float BulletDistance = 1000.0f;
		public readonly float BulletSpeed = 350.0f;
		public readonly int MaximumAmmoCount = 30;

		// state
		public int AmmoCount = 30;
		public int? EquippedPosition = null;

		public WeaponModel(WeaponBehavior weaponBehavior, string name, bool fullyAutomatic, bool piercingRounds)
		{
			this.WeaponBehavior = weaponBehavior;
			this.Name = name;
			this.IsFullyAutomatic = fullyAutomatic;
			this.UsesPiercingRounds = piercingRounds;
		}

		public void shoot()
		{
			if (this.AmmoCount == 0)
			{
				playClipEmptySound();
				return;
			}

			this.WeaponBehavior.shoot(this.BulletDistance, this.BulletSpeed, this.UsesPiercingRounds);
			--this.AmmoCount;
		}

		public void setAmmoCountMaximum()
		{
			this.AmmoCount = this.MaximumAmmoCount;
		}

		public bool isClipFull()
		{
			return this.AmmoCount == this.MaximumAmmoCount;
		}

		public void stopReloadingAnimation()
		{
			this.WeaponBehavior.stopAnimations();
		}

		public void playClipFullSound()
		{
			this.WeaponBehavior.playClipFullSound();
		}

		public void playClipEmptySound()
		{
			this.WeaponBehavior.playClipEmptySound();
		}

		public void playReloadAnimation()
		{
			this.WeaponBehavior.playReloadAnimation();
		}

		public void reparentUnderGO(GameObject parentGO)
		{
			WeaponBehavior.transform.SetParent(parentGO.transform);
			WeaponBehavior.transform.localPosition = Vector3.zero;
			WeaponBehavior.transform.localRotation = Quaternion.identity;
			WeaponBehavior.transform.localScale = Vector3.one;
		}

		//public void showToFpsCamera(Transform parentGO)
		//{
		//	WeaponBehavior.transform.SetParent(parentGO.transform);
		//	WeaponBehavior.transform.localPosition = Vector3.zero;
		//	WeaponBehavior.transform.localRotation = Quaternion.identity;

			// TODO: implement
			//this.WeaponBehavior.showToFpsCamera();
		//}

		//public void moveToBackSlot()
		//{
			//this.WeaponBehavior.transform.SetParent(this.WeaponBehavior.PlayerBackWeaponSlot.transform);
			//this.WeaponBehavior.transform.localPosition = Vector3.zero;
			//this.WeaponBehavior.transform.localRotation = Quaternion.identity;

			// TODO: implement
			//this.WeaponBehavior.hideFromFpsCamera();
		//}
	}
}