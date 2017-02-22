using UnityEngine;
using System.Collections;
using weapon;

namespace player
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private UserIO userIO;
		[SerializeField] private Camera kamera;
		[SerializeField] public CrosshairControl CrosshairControl;
		[SerializeField] private GameObject BackWeaponSlot0;
		[SerializeField] private GameObject BackWeaponSlot1;
		[SerializeField] private GameObject BackWeaponSlot2;
		[SerializeField] private GameObject equippedSlot;
		[SerializeField] private GameObject guiAmmoPanel;

		private WeaponFactory weaponFactory = new WeaponFactory();
		private PlayerAnimate playerAnimator;
		private Inventory inventory;
		private CrosshairControl crosshairAnimator;

		// state
		private Weapon activeWeapon;

		void Start() {
			inventory = gameObject.AddComponent<Inventory>();
			playerAnimator = GetComponent<PlayerAnimate>();

			var weapon0 = weaponFactory.makeWeapon(BackWeaponSlot0, this.kamera);
			inventory.addWeapon(weapon0);

			var weapon1 = weaponFactory.makeWeapon(BackWeaponSlot1, this.kamera);
			inventory.addWeapon(weapon1);

			var weapon2 = weaponFactory.makeWeapon(BackWeaponSlot2, this.kamera);
			inventory.addWeapon(weapon2);
		}

		void Update()
		{
			if (userIO.GetKeyDown(KeyCode.BackQuote))
			{
				equipFists();
				playerAnimator.sheathWeapon();
			}
			else if (userIO.GetKeyDown(KeyCode.Alpha1))
			{
				equipFists();
				equipWeaponSlot(0);
			}
			else if (userIO.GetKeyDown(KeyCode.Alpha2))
			{
				equipFists();
				equipWeaponSlot(1);
			}
			else if (userIO.GetKeyDown(KeyCode.Alpha3))
			{
				equipFists();
				equipWeaponSlot(2);
			}
			else if (userIO.GetKeyDown(KeyCode.R))
			{
				reloadWeapon();
			}
				
			if (userIO.GetButtonDown("Fire1") && (activeWeapon != null))
			{
				activeWeapon.shoot();
				CrosshairControl.animate();
			}
		}

		public bool isWeaponEquipped() {
			return null != activeWeapon;
		}

		public uint equippedWeaponAmmoCount() {
			return activeWeapon.AmmoCount;
		}

		public uint equippedWeaponMaxAmmo() {
			return activeWeapon.MaximumAmmoCount;
		}

		private void reloadWeapon() {
			if (!isWeaponEquipped())
			{
				return;
			}
			activeWeapon.tryReload();
		}

		private void equipWeaponSlot(int index)
		{
			var weapon = inventory.getWeapon(index);
			activeWeapon = weapon;

			weapon.transform.SetParent(equippedSlot.transform);
			weapon.transform.localPosition = Vector3.zero;
			weapon.transform.localRotation = Quaternion.identity * Quaternion.AngleAxis(180, Vector3.up);
			playerAnimator.equipWeapon();
		}

		private void equipFists()
		{
			if (!isWeaponEquipped())
			{
				return;
			}
			activeWeapon.transform.SetParent(activeWeapon.PlayerBackWeaponSlot.transform);
			activeWeapon.transform.localPosition = Vector3.zero;
			activeWeapon.transform.localRotation = Quaternion.identity;

			activeWeapon = null;
		}
	}
}