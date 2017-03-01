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
			return makeWeapon(parent, "Prefabs/Weapons/Ak-47", true);
		}

		public Weapon makeM4A1(GameObject parent) {
			return makeWeapon(parent, "Prefabs/Weapons/M4A1 Sopmod", false);
		}

		private Weapon makeWeapon(GameObject parent, string prefabPath, bool fullyAutomatic) {
			GameObject go = new GameObject("PlayerWeapon" + weaponId);
			++weaponId;
			go.transform.SetParent(parent.gameObject.transform);
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = Vector3.one;

			var weaponBehavior = go.AddComponent<WeaponBehavior>();
			weaponBehavior.PrefabPath = prefabPath;
			weaponBehavior.PlayerBackWeaponSlot = parent;

			return new Weapon(weaponBehavior, prefabPath, fullyAutomatic);
		}
	}
}