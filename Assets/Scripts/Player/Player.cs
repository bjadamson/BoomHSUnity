using UnityEngine;
using System.Collections;
using camera;
using weapon;

namespace player
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private GameObject PlayerGO;
		[SerializeField] private UserIO userIO;
		[SerializeField] private Camera kamera;
		[SerializeField] private Freelook cameraFreelook;
		[SerializeField] public CrosshairControl CrosshairControl;
		[SerializeField] private GameObject BackWeaponSlot0;
		[SerializeField] private GameObject BackWeaponSlot1;
		[SerializeField] private GameObject BackWeaponSlot2;
		[SerializeField] private GameObject equippedSlot;
		[SerializeField] private GameObject guiAmmoPanel;
		[SerializeField] private float reloadTime = 1.5f;

		private WeaponFactory weaponFactory = new WeaponFactory();
		private PlayerAnimate playerAnimator;
		private Inventory inventory;
		private CrosshairControl crosshairAnimator;

		// state
		private Weapon activeWeapon;
		private bool startedReloading = false;
		private float timeWhenReloadingFinished = 0.0f;

		void Start()
		{
			inventory = gameObject.AddComponent<Inventory>();
			playerAnimator = GetComponent<PlayerAnimate>();

			var weapon0 = weaponFactory.makeWeapon(BackWeaponSlot0, this.kamera, "Weapons/Ak-47");
			inventory.addWeapon(weapon0);

			var weapon1 = weaponFactory.makeWeapon(BackWeaponSlot1, this.kamera, "Weapons/Ak-47");
			inventory.addWeapon(weapon1);

			var weapon2 = weaponFactory.makeWeapon(BackWeaponSlot2, this.kamera, "Weapons/M4A1 Sopmod");
			inventory.addWeapon(weapon2);
		}

		void Update()
		{
			if (!cameraFreelook.IsFreelookModeActive())
			{
				// rotate around local axis
				PlayerGO.transform.RotateAround(PlayerGO.transform.position, PlayerGO.transform.up, Input.GetAxis("Mouse X") * 150 * Time.deltaTime);
			}

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

			if (isWeaponEquipped())
			{
				if (userIO.GetKeyDown(KeyCode.R))
				{
					if (activeWeapon.isClipFull())
					{
						activeWeapon.playClipFullSound();
					}
					else if (!startedReloading)
					{
						startedReloading = true;
						activeWeapon.playReloadAnimation();
						timeWhenReloadingFinished = Time.time + reloadTime;
					}
				}
				else if (startedReloading && isReloadingAnimationFinished())
				{
					stopReloading();
					activeWeapon.reloadAmmo();
				}
				
				if (userIO.GetButtonDown("Fire1") && !startedReloading)
				{
					activeWeapon.shoot();
					CrosshairControl.animate();
				}
			}
		}

		private void stopReloading()
		{
			startedReloading = false;
			if (activeWeapon != null)
			{
				activeWeapon.stopAnimations();
			}
		}

		private bool isReloadingAnimationFinished()
		{
			return timeWhenReloadingFinished < Time.time;
		}

		public bool isWeaponEquipped()
		{
			return null != activeWeapon;
		}

		public uint equippedWeaponAmmoCount()
		{
			return activeWeapon.AmmoCount;
		}

		public uint equippedWeaponMaxAmmo()
		{
			return activeWeapon.MaximumAmmoCount;
		}

		private void equipWeaponSlot(int index)
		{
			var weapon = inventory.getWeapon(index);
			activeWeapon = weapon;

			weapon.transform.SetParent(equippedSlot.transform);
			weapon.transform.localPosition = Vector3.zero;
			weapon.transform.localRotation = Quaternion.identity * Quaternion.AngleAxis(180, Vector3.up);
			weapon.showToFpsCamera();

			stopReloading();
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
			activeWeapon.hideFromFpsCamera();

			activeWeapon = null;
		}
	}
}