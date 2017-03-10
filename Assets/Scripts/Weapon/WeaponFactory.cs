using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player;

namespace weapon
{
	public class WeaponFactory
	{
		private uint weaponId = 0;

		public WeaponModel makeAk47() {
			bool fullyAutomatic = true;
			bool piercingRounds = true;
			return makeWeapon("Prefabs/Weapons/Ak-47", fullyAutomatic, piercingRounds);
		}

		public WeaponModel makeM4A1() {
			bool fullyAutomatic = false;
			bool piercingRounds = false;
			return makeWeapon("Prefabs/Weapons/M4A1 Sopmod", fullyAutomatic, piercingRounds);
		}

		private WeaponModel makeWeapon(string prefabPath, bool fullyAutomatic, bool piercingRounds) {
			GameObject go = new GameObject("PlayerWeapon" + weaponId);
			++weaponId;
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = Vector3.one;

			var weaponBehavior = go.AddComponent<WeaponBehavior>();
			weaponBehavior.PrefabPath = prefabPath;

			return new WeaponModel(weaponBehavior, prefabPath, fullyAutomatic, piercingRounds);
		}
	}
}