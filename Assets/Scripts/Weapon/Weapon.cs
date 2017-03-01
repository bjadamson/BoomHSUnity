using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
	public class Weapon
	{
		public readonly WeaponBehavior WeaponBehavior;
		public readonly GameObject WeaponGO;
		public readonly bool IsFullyAutomatic;
		public readonly string Name;

		public readonly uint MaximumAmmoCount = 30;
		public uint AmmoCount = 5;

		public Weapon(WeaponBehavior weaponBehavior, string name, bool fullyAutomatic) {
			this.WeaponBehavior = weaponBehavior;
			this.IsFullyAutomatic = fullyAutomatic;
			this.Name = name;
		}

		public void shoot()
		{
			if (this.AmmoCount == 0)
			{
				playClipEmptySound();
				return;
			}

			this.WeaponBehavior.shoot();
			--this.AmmoCount;
		}

		public void reloadAmmo() {
			this.AmmoCount = this.MaximumAmmoCount;
		}

		public bool isClipFull() {
			return this.AmmoCount == this.MaximumAmmoCount;
		}

		public void stopReloading() {
			this.WeaponBehavior.stopAnimations();
		}

		public void playClipFullSound() {
			this.WeaponBehavior.playClipFullSound();
		}

		public void playClipEmptySound() {
			this.WeaponBehavior.playClipEmptySound();
		}

		public void playReloadAnimation() {
			this.WeaponBehavior.playReloadAnimation();
		}

		public void showToFpsCamera(Transform parent) {
			this.WeaponBehavior.transform.SetParent(parent);
			this.WeaponBehavior.transform.localPosition = Vector3.zero;
			this.WeaponBehavior.transform.localRotation = Quaternion.identity * Quaternion.AngleAxis(180, Vector3.up);
		}

		public void hideFromFpsCamera() {
			this.WeaponBehavior.transform.SetParent(this.WeaponBehavior.PlayerBackWeaponSlot.transform);
			this.WeaponBehavior.transform.localPosition = Vector3.zero;
			this.WeaponBehavior.transform.localRotation = Quaternion.identity;
			this.WeaponBehavior.hideFromFpsCamera();
		}
	}
}