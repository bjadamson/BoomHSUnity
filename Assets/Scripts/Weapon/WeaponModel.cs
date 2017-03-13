using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
	public class WeaponModel
	{
		public readonly WeaponBehavior WeaponBehavior;
		private WeaponState weaponState;

		// state
		private float timeWhenCanContinueShootingFullyAuto = 0.0f;

		public int AmmoCount
		{
			get
			{
				return weaponState.AmmoCount;
			}
		}

		public int MaxAmmoCount
		{
			get
			{
				return weaponState.MaximumAmmoCount;
			}
		}

		public WeaponModel(WeaponBehavior weaponBehavior, WeaponState weaponState)
		{
			this.WeaponBehavior = weaponBehavior;
			this.weaponState = weaponState;
		}

		public void reload()
		{
			this.weaponState.AmmoCount = this.weaponState.MaximumAmmoCount;
		}

		public bool isClipFull()
		{
			return this.weaponState.AmmoCount == this.weaponState.MaximumAmmoCount;
		}

		public bool hasMeshRenderer()
		{
			return this.WeaponBehavior.PrefabPath != null;
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
			this.WeaponBehavior.playReloadSound();
		}

		public void shootIfAble(bool fire1Pressed, bool fire1HeldDown)
		{
			if (!weaponState.UsesBullets)
			{
				return;
			}
			if (fire1Pressed && !weaponState.IsFullyAutomatic)
			{
				shoot();
			}
			else if (fire1HeldDown && weaponState.IsFullyAutomatic)
			{
				if (timeWhenCanContinueShootingFullyAuto <= Time.time)
				{
					shoot();
					timeWhenCanContinueShootingFullyAuto = Time.time + 0.2f;
				}
			}
		}

		public void reparentUnderGO(GameObject parentGO)
		{
			WeaponBehavior.transform.SetParent(parentGO.transform);
			WeaponBehavior.transform.localPosition = Vector3.zero;
			WeaponBehavior.transform.localRotation = Quaternion.identity;
			WeaponBehavior.transform.localScale = Vector3.one;
		}

		private void shoot()
		{
			if (this.weaponState.AmmoCount == 0)
			{
				playClipEmptySound();
				return;
			}

			float distance = this.weaponState.BulletDistance;
			float speed = this.weaponState.BulletSpeed;
			bool piercing = this.weaponState.UsesPiercingRounds;
			this.WeaponBehavior.shoot(distance, speed, piercing);
			--this.weaponState.AmmoCount;
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