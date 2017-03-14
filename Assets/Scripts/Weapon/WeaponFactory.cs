using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using player;
using ui;
using ui.inventory;

namespace weapon
{
	public class WeaponFactory
	{
		private int weaponId = 0;

		private readonly UIManager uiManager;

		public WeaponFactory(UIManager uim)
		{
			uiManager = uim;
		}

		public WeaponModel makeAk47()
		{
			bool fullyAutomatic = true;
			bool piercingRounds = true;
			bool usesBullets = true;
			return makeWeapon("Ak-47", "Ak-47", "AkInventoryIcon", usesBullets, fullyAutomatic, piercingRounds);
		}

		public WeaponModel makeM4A1()
		{
			bool fullyAutomatic = false;
			bool piercingRounds = false;
			bool usesBullets = true;
			return makeWeapon("M4A1", "M4A1 Sopmod", "ArInventoryIcon", usesBullets, fullyAutomatic, piercingRounds);
		}

		public WeaponModel makeFists()
		{
			bool fullyAutomatic = false;
			bool piercingRounds = false;
			bool usesBullets = false;
			string pathToPrefab = null;
			return makeWeapon("Fists", pathToPrefab, "FistInventoryIcon", usesBullets, fullyAutomatic, piercingRounds);
		}

		private WeaponModel makeWeapon(string weaponName, string prefabWeaponName, string iconName, bool usesBullets, bool fullyAutomatic, bool piercingRounds)
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
			if (prefabWeaponName != null)
			{
				weaponBehavior.PrefabPath = pathPrefix + "Guns/" + prefabWeaponName;
			}
			else
			{
				weaponBehavior.PrefabPath = null;
			}

			var inventoryItem = uiManager.itemIdToInventoryItem(weaponId - 1);
			weaponBehavior.InventoryUiItem = inventoryItem;
			weaponBehavior.Icon = Resources.Load<Sprite>("Textures/UI/" + iconName);
			Debug.Assert(weaponBehavior.Icon != null);

			var state = new WeaponState(weaponName, usesBullets, fullyAutomatic, piercingRounds, 1000, 350, 30);
			return new WeaponModel(weaponBehavior, state);
		}
	}
}