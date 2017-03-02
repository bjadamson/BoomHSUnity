using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
	public class Weapon
	{
		public readonly WeaponBehavior WeaponBehavior;
		public readonly GameObject WeaponGO;
		public readonly string Name;
		public readonly bool IsFullyAutomatic;
		public readonly bool UsesPiercingRounds;

		public float BulletDistance = 1000.0f;
		public float BulletSpeed = 200.0f;
		public readonly uint MaximumAmmoCount = 30;
		public uint AmmoCount = 30;

		public Weapon(WeaponBehavior weaponBehavior, string name, bool fullyAutomatic, bool piercingRounds) {
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