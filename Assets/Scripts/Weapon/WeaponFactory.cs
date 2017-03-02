using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player;

namespace weapon
{
	public class WeaponFactory
	{
		private uint weaponId = 0;

		public Weapon makeAk47(GameObject parent) {
			bool fullyAutomatic = true;
			bool piercingRounds = true;
			return makeWeapon(parent, "Prefabs/Weapons/Ak-47", fullyAutomatic, piercingRounds);
		}

		public Weapon makeM4A1(GameObject parent) {
			bool fullyAutomatic = false;
			bool piercingRounds = false;
			return makeWeapon(parent, "Prefabs/Weapons/M4A1 Sopmod", fullyAutomatic, piercingRounds);
		}

		private Weapon makeWeapon(GameObject parent, string prefabPath, bool fullyAutomatic, bool piercingRounds) {
			GameObject go = new GameObject("PlayerWeapon" + weaponId);
			++weaponId;
			go.transform.SetParent(parent.gameObject.transform);
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = Vector3.one;

			var weaponBehavior = go.AddComponent<WeaponBehavior>();
			weaponBehavior.PrefabPath = prefabPath;
			weaponBehavior.PlayerBackWeaponSlot = parent;

			return new Weapon(weaponBehavior, prefabPath, fullyAutomatic, piercingRounds);
		}
	}
}