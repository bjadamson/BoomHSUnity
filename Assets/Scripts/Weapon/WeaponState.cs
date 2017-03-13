using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using player;

namespace weapon
{
	public struct WeaponState
	{
		public readonly string Name;
		public readonly bool UsesBullets;
		public readonly bool IsFullyAutomatic;
		public readonly bool UsesPiercingRounds;

		public readonly float BulletDistance;
		public readonly float BulletSpeed;
		public readonly int MaximumAmmoCount;

		// state
		public int AmmoCount;

		public WeaponState(string name, bool usesBullets, bool fullyAuto, bool piercingRounds, float bulletDistance, float bulletSpeed, int maxAmmoCount)
		{
			this.Name = name;
			this.UsesBullets = usesBullets;
			this.IsFullyAutomatic = fullyAuto;
			this.UsesPiercingRounds = piercingRounds;
			this.BulletDistance = bulletDistance;
			this.BulletSpeed = bulletSpeed;
			this.MaximumAmmoCount = maxAmmoCount;

			this.AmmoCount = 0;
		}
	}
}