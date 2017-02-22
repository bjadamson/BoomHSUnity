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
		[SerializeField] private AudioSource ShootGunAudioSource;
		[SerializeField] private GameObject BackWeaponSlot1;

		[SerializeField] private GameObject backSlot;
		[SerializeField] private GameObject equippedSlot;

		private PlayerAnimate playerAnimator;
		private Weapon weapon;
		private CrosshairControl crosshairAnimator;

		// state
		private bool weaponEquipped = false;

		void Start() {
			GameObject go = new GameObject("PlayerWeapon");
			go.transform.SetParent(BackWeaponSlot1.transform);
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = Vector3.one;

			playerAnimator = GetComponent<PlayerAnimate>();

			weapon = go.AddComponent<Weapon>();
			weapon.UserIO = this.userIO;
			weapon.Kamera = this.kamera;
			weapon.ShootSound = this.ShootGunAudioSource;
		}

		void Update()
		{
			if (userIO.GetKeyDown(KeyCode.Alpha1))
			{
				equipWeapon();
			}
			else if (userIO.GetKeyDown(KeyCode.BackQuote))
			{
				putAwayWeapon();
			}

			if (userIO.GetButtonDown("Fire1") && weaponEquipped)
			{
				weapon.shoot();
				CrosshairControl.animate();
			}
		}

		private void equipWeapon()
		{
			weaponEquipped = true;
			weapon.transform.SetParent(equippedSlot.transform);
			weapon.transform.localPosition = Vector3.zero;
			weapon.transform.localRotation = Quaternion.identity * Quaternion.AngleAxis(180, Vector3.up);
			playerAnimator.equipWeapon();
		}

		private void putAwayWeapon()
		{
			weaponEquipped = false;
			weapon.transform.SetParent(backSlot.transform);
			weapon.transform.localPosition = weapon.transform.parent.localPosition;
			weapon.transform.localRotation = weapon.transform.parent.localRotation;
			playerAnimator.sheathWeapon();
		}
	}
}