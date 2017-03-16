using UnityEngine;
using player;
using weapon;
using ui;
using ui.inventory;

public class InventoryIO : MonoBehaviour
{
	[SerializeField] private PlayerBehavior playerBehavior;
	[SerializeField] private Inventory inventory;

	void Start()
	{
		Debug.Assert(playerBehavior != null);
		Debug.Assert(inventory != null);

		// hack for now, we need to wait until after all MonoBehavior Start() methods have been invoked before calling this..
		Invoke("spawnItems", 0.1f);
	}

	private void spawnItems()
	{
		WeaponFactory weaponFactory = new WeaponFactory(inventory);
		var fists = weaponFactory.makeFists();
		var weapon1 = weaponFactory.makeM4A1();
		var weapon2 = weaponFactory.makeAk47();
		var weapon3 = weaponFactory.makeM4A1();

		playerBehavior.addItem(fists, 0);
		playerBehavior.addItem(weapon1, 1);
		playerBehavior.addItem(weapon2, 2);
		playerBehavior.addItem(weapon3, 3);

		for (int i = 0; i < 3; ++i)
		{
			playerBehavior.addItem(weaponFactory.makeAk47(), null);
			playerBehavior.addItem(weaponFactory.makeM4A1(), null);
			playerBehavior.addItem(weaponFactory.makeM4A1(), null);
			playerBehavior.addItem(weaponFactory.makeAk47(), null);

			playerBehavior.addItem(weaponFactory.makeM4A1(), null);
			playerBehavior.addItem(weaponFactory.makeM4A1(), null);
			playerBehavior.addItem(weaponFactory.makeM4A1(), null);
			playerBehavior.addItem(weaponFactory.makeAk47(), null);
		}

		// state with fists equipped
		playerBehavior.ifWeaponAtPositionThenEquip(0);
	}
}