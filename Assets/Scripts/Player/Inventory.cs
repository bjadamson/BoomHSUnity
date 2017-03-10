using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

namespace player
{
	public class Inventory
	{
		private readonly IList<WeaponModel> weapons = new List<WeaponModel>();

		public void addWeapon(WeaponModel weapon)
		{
			weapons.Add(weapon);
		}

		public WeaponModel getWeapon(int index)
		{
			return weapons[index];
		}
	}
}