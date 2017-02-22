using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

namespace player
{
	public class Inventory : MonoBehaviour
	{
		private readonly IList<Weapon> weapons = new List<Weapon>();

		public void addWeapon(Weapon weapon) {
			weapons.Add(weapon);
		}

		public Weapon getWeapon(int index) {
			return weapons[index];
		}
	}
}