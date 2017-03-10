using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player;

using UnityEngine.UI;

namespace weapon
{
	public class WeaponFactory
	{
		private uint weaponId = 0;

		public WeaponModel makeAk47()
		{
			bool fullyAutomatic = true;
			bool piercingRounds = true;
			return makeWeapon("Ak-47", "AkInventoryIcon", fullyAutomatic, piercingRounds);
		}

		public WeaponModel makeM4A1()
		{
			bool fullyAutomatic = false;
			bool piercingRounds = false;
			return makeWeapon("M4A1 Sopmod", "ArInventoryIcon", fullyAutomatic, piercingRounds);
		}

		private WeaponModel makeWeapon(string prefabWeaponName, string iconName, bool fullyAutomatic, bool piercingRounds)
		{
			string pathPrefix = "Prefabs/Weapons/";
			GameObject go = (GameObject)MonoBehaviour.Instantiate(Resources.Load(pathPrefix + "PlayerWeapon"));
			Debug.Assert(go != null);
			go.name = "PlayerWeapon" + weaponId;

			++weaponId;
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = Vector3.one;

			var weaponBehavior = go.GetComponent<WeaponBehavior>();
			weaponBehavior.PrefabPath = pathPrefix + "Guns/" + prefabWeaponName;
			weaponBehavior.Icon = Resources.Load<Sprite>("Textures/UI/" + iconName);
			Debug.Assert(weaponBehavior.Icon != null);

			return new WeaponModel(weaponBehavior, prefabWeaponName, fullyAutomatic, piercingRounds);
		}
	}
}