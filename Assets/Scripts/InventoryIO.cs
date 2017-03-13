﻿using UnityEngine;
using player;
using weapon;

public class InventoryIO : MonoBehaviour {
	[SerializeField] private PlayerBehavior playerBehavior;

	void Start() {
		// hack for now, we need to wait until after all MonoBehavior Start() methods have been invoked before calling this..
		Invoke("spawnItems", 0.1f);
	}

	private void spawnItems()
	{
		WeaponFactory weaponFactory = new WeaponFactory();
		var fists = weaponFactory.makeFists();
		var weapon1 = weaponFactory.makeM4A1();
		var weapon2 = weaponFactory.makeAk47();
		var weapon3 = weaponFactory.makeM4A1();

		playerBehavior.addItem(fists, 0);
		playerBehavior.addItem(weapon1, 1);
		playerBehavior.addItem(weapon2, 2);
		playerBehavior.addItem(weapon3, 3);

		playerBehavior.addItem(weaponFactory.makeAk47(), null);
		playerBehavior.addItem(weaponFactory.makeM4A1(), null);
		playerBehavior.addItem(weaponFactory.makeM4A1(), null);
		playerBehavior.addItem(weaponFactory.makeAk47(), null);

		playerBehavior.addItem(weaponFactory.makeM4A1(), null);
		playerBehavior.addItem(weaponFactory.makeM4A1(), null);
		playerBehavior.addItem(weaponFactory.makeM4A1(), null);
		playerBehavior.addItem(weaponFactory.makeAk47(), null);

		// state with fists equipped
		playerBehavior.ifWeaponAtPositionThenEquip(0);
	}
}