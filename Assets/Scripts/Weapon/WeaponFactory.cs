using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player;

namespace weapon
{
	public class WeaponFactory
	{
		private uint weaponId = 0;

		public Weapon makeWeapon(GameObject parent, Camera kamera) {
			GameObject go = new GameObject("PlayerWeapon" + weaponId);
			++weaponId;
			go.transform.SetParent(parent.gameObject.transform);
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = Vector3.one;

			var weapon = go.AddComponent<Weapon>();
			weapon.Kamera = kamera;
			weapon.PlayerBackWeaponSlot = parent;
			return weapon;
		}
	}
}