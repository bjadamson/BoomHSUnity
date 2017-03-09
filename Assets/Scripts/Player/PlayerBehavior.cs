using UnityEngine;
using System.Collections;
using camera;
using weapon;

// todo: move
using ui;

namespace player
{
	public class PlayerBehavior : MonoBehaviour
	{
		[SerializeField] private GameObject PlayerGO;
		[SerializeField] private UserIO userIO;
		[SerializeField] private CameraController kameraController;
		[SerializeField] public CrosshairControl CrosshairControl;
		[SerializeField] private DeathReviveBehavior deathBehavior;
		[SerializeField] private GameObject BackWeaponSlot0;
		[SerializeField] private GameObject BackWeaponSlot1;
		[SerializeField] private GameObject BackWeaponSlot2;
		[SerializeField] private GameObject equippedSlot;
		[SerializeField] private GameObject equippedADSSlot;
		[SerializeField] private GameObject guiAmmoPanel;

		// constants
		private readonly float TIME_FOR_REVIVE_ANIMATION = 4.0f;
		private readonly float TIME_FOR_RELOAD = 1.5f;

		private readonly float JUMP_FORCE = 1000.0f;
		private readonly float MOVEMENT_SPEED = 10.0f;

		private WeaponFactory weaponFactory = new WeaponFactory();
		private PlayerAnimate playerAnimator;
		private PlayerCrouchStand crouchStand;
		private CapsuleCollider capsuleCollider;
		private Rigidbody rigidBody;
		private Inventory inventory;

		// state
		private Weapon activeWeapon;
		private bool isADS = false;
		private bool startedReloading = false;
		private int health = 100;

		private float timeWhenRevivingFinished = 0.0f;
		private float timeWhenReloadingFinished = 0.0f;

		private float timeUntilJumpingAllowed = 0.0f;
		private float distanceToGround;
		private bool isCrouching = false;

		// todo: move
		private float timeWhenCanContinueShootingFullyAuto = 0.0f;

		void Start()
		{
			inventory = gameObject.AddComponent<Inventory>();
			playerAnimator = GetComponent<PlayerAnimate>();

			var weapon0 = weaponFactory.makeAk47(BackWeaponSlot0);
			inventory.addWeapon(weapon0);

			var weapon1 = weaponFactory.makeAk47(BackWeaponSlot1);
			inventory.addWeapon(weapon1);

			var weapon2 = weaponFactory.makeM4A1(BackWeaponSlot2);
			inventory.addWeapon(weapon2);

			playerAnimator = GetComponent<PlayerAnimate>();
			capsuleCollider = PlayerGO.GetComponent<CapsuleCollider>();
			crouchStand = GetComponent<PlayerCrouchStand>();
			rigidBody = PlayerGO.GetComponent<Rigidbody>();
			distanceToGround = PlayerGO.GetComponent<Collider>().bounds.extents.y;
		}

		void Update()
		{
			if (userIO.GetKeyDown(KeyCode.K))
			{
				if (isDead() && !isReviving())
				{
					revive();
				}
				else
				{
					kill();
				}
			}
			if (isDead())
			{
				return;
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
						timeWhenReloadingFinished = Time.time + TIME_FOR_RELOAD;
					}
				}
				else if (startedReloading && isReloadingAnimationFinished())
				{
					stopReloading();
					activeWeapon.reloadAmmo();
				}

				if (!startedReloading && !kameraController.isFreelookMode())
				{
					if (userIO.GetButtonDown("Fire1") && !activeWeapon.IsFullyAutomatic)
					{
						shootWeapon();
					}
					else if (userIO.GetButton("Fire1") && activeWeapon.IsFullyAutomatic)
					{
						if (timeWhenCanContinueShootingFullyAuto <= Time.time)
						{
							shootWeapon();
							timeWhenCanContinueShootingFullyAuto = Time.time + 0.2f;
						}
					}
				}

				if (!startedReloading)
				{
					if (userIO.GetButtonDown("Fire2"))
					{
						isADS = true;
						playerAnimator.setADS(true);
						activeWeapon.reparent(equippedADSSlot);
					}
					else if (userIO.GetButtonUp("Fire2"))
					{
						isADS = false;
						playerAnimator.setADS(false);
						activeWeapon.reparent(equippedSlot);
					}
				}
			}

			if (!isADS)
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
			}
		}

		void FixedUpdate()
		{
			if (isDead() || isReviving())
			{
				return;
			}
			float horizontalAxis = userIO.GetAxis("Horizontal");
			float verticalAxis = userIO.GetAxis("Vertical");

			float timeMultiplier = Time.deltaTime * MOVEMENT_SPEED;
			float horizontal = horizontalAxis * timeMultiplier;
			float vertical = verticalAxis * timeMultiplier;

			bool strafeLeft = horizontalAxis < 0f;
			bool strafeRight = horizontalAxis > 0f;
			bool canJump = isOnGround() && (timeUntilJumpingAllowed < Time.time);
			bool isJumping = userIO.GetKeyDown(KeyCode.Space) && canJump;

			bool isCrouch = userIO.GetKey(KeyCode.LeftControl);
			bool isSprint = userIO.GetKey(KeyCode.LeftShift);
			float speed = Mathf.Abs(verticalAxis);

			playerAnimator.updateAnimations(horizontal, vertical, speed, isJumping, strafeLeft, strafeRight, isCrouch, isSprint);
			crouchStand.crouchStandOverTime(isCrouch);

			Vector3 movement = (PlayerGO.transform.forward * vertical) + (PlayerGO.transform.right * horizontal);
			PlayerGO.transform.Translate(movement, Space.World);

			if (isJumping)
			{
				jump();
			}
		}

		private void jump()
		{
			rigidBody.AddForce(Vector3.up * JUMP_FORCE, ForceMode.Impulse);

			// TODO: adjust capsule collider for duration of jump, so we can jump up on things.
		}

		private bool isOnGround()
		{
			// 0.1 offset deals with "irregularities" in the ground
			return Physics.Raycast(PlayerGO.transform.position, -Vector3.up, distanceToGround + 0.1f);
		}

		private void kill()
		{
			this.health = 0;
			deathBehavior.setDead();
			playerAnimator.playDeathAnimation();
			kameraController.death();
		}

		private void revive()
		{
			this.health = 100;
			deathBehavior.setAlive();
			playerAnimator.playReviveAnimation();
			kameraController.revive();

			timeWhenRevivingFinished = Time.time + TIME_FOR_REVIVE_ANIMATION;
		}

		private bool isDead()
		{
			return health <= 0;
		}

		private bool isReviving() {
			return timeWhenRevivingFinished >= Time.time;
		}

		private void shootWeapon()
		{
			activeWeapon.shoot();
			CrosshairControl.animateShooting();
		}

		private void stopReloading()
		{
			startedReloading = false;
			if (activeWeapon != null)
			{
				activeWeapon.stopReloading();
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
			var weapon = inventory.getWeapon(index).WeaponBehavior;
			activeWeapon = inventory.getWeapon(index);

			activeWeapon.showToFpsCamera(equippedSlot.transform);

			stopReloading();
			playerAnimator.equipWeapon();
		}

		private void equipFists()
		{
			if (!isWeaponEquipped())
			{
				return;
			}
			activeWeapon.hideFromFpsCamera();
			activeWeapon = null;
		}
	}
}