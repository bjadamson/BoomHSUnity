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
		[SerializeField] private DeathReviveBehavior deathBehavior;
		[SerializeField] private CameraController kameraController;
		[SerializeField] private UIManager uiManager;
		[SerializeField] private UserIO userIO;

		private WeaponSlotGameObjects WeaponSlotsGOs;

		// constants
		private readonly float JUMP_FORCE = 660.0f;
		private readonly float MOVEMENT_SPEED = 10.0f;

		private WeaponFactory weaponFactory = new WeaponFactory();
		private PlayerAnimate playerAnimator;
		private CrouchStand crouchStand;
		private CapsuleCollider capsuleCollider;
		private Rigidbody rigidBody;

		// state
		private PlayerStateModel playerModel;
		private bool isFalling = false;

		void Start()
		{
			playerAnimator = GetComponent<PlayerAnimate>();
			Debug.Assert(playerAnimator != null);
			WeaponSlotsGOs = GetComponent<WeaponSlotGameObjects>();
			Debug.Assert(WeaponSlotsGOs != null);

			playerAnimator = GetComponent<PlayerAnimate>();
			capsuleCollider = GetComponent<CapsuleCollider>();
			crouchStand = GetComponent<CrouchStand>();
			rigidBody = GetComponent<Rigidbody>();

			float distanceToGround = GetComponent<Collider>().bounds.extents.y;
			playerModel = new PlayerStateModel(distanceToGround, uiManager, kameraController);

			// hack for now, we need to wait until after all MonoBehavior Start() methods have been invoked before calling this..
			Invoke("spawnItems", 0.1f);
		}

		private void spawnItems()
		{
			var weapon0 = weaponFactory.makeM4A1(0);
			var weapon1 = weaponFactory.makeAk47(1);
			var weapon2 = weaponFactory.makeM4A1(2);

			playerModel.addWeapon(weapon0);
			playerModel.addWeapon(weapon1);
			playerModel.addWeapon(weapon2);

			playerModel.equipWeaponSlot(0, playerAnimator, WeaponSlotsGOs);
			playerModel.equipWeaponSlot(1, playerAnimator, WeaponSlotsGOs);
			playerModel.equipWeaponSlot(2, playerAnimator, WeaponSlotsGOs);

			playerModel.unequipEquippedWeapon(playerAnimator, WeaponSlotsGOs);
		}

		void Update()
		{
			if (userIO.GetKeyDown(KeyCode.P))
			{
				spawnItems();
			}
			maybeToggleIsDead(userIO.GetKeyDown(KeyCode.K));
			if (playerModel.isDead())
			{
				return;
			}

			if (playerModel.isWeaponEquipped())
			{
				playerModel.maybeReload(userIO.GetKeyDown(KeyCode.R));
				playerModel.shootIfAppropriate(userIO.GetButtonDown("Fire1"), userIO.GetButton("Fire1"));

				bool fire2Down = userIO.GetButtonDown("Fire2");
				bool fire2Up = userIO.GetButtonUp("Fire2");
				playerModel.adsIfAppropriate(fire2Down, fire2Up, playerAnimator, WeaponSlotsGOs.EquippedADS, WeaponSlotsGOs.EquippedRHS);
			}

			if (!playerModel.isADS())
			{
				bool fists = userIO.GetKeyDown(KeyCode.BackQuote);
				if (fists)
				{
					playerModel.unequipEquippedWeapon(playerAnimator, WeaponSlotsGOs);
				}
				else
				{
					bool weapon0 = userIO.GetKeyDown(KeyCode.Alpha1);
					bool weapon1 = userIO.GetKeyDown(KeyCode.Alpha2);
					bool weapon2 = userIO.GetKeyDown(KeyCode.Alpha3);
					if (weapon0)
					{
						playerModel.equipWeaponSlot(0, playerAnimator, WeaponSlotsGOs);
					}
					else if (weapon1)
					{
						playerModel.equipWeaponSlot(1, playerAnimator, WeaponSlotsGOs);
					}
					else if (weapon2)
					{
						playerModel.equipWeaponSlot(2, playerAnimator, WeaponSlotsGOs);
					}
				}
			}
		}

		void FixedUpdate()
		{
			if (playerModel.isDead() || playerModel.isReviving())
			{
				return;
			}
			float horizontalAxis = userIO.GetAxis("Horizontal");
			float verticalAxis = userIO.GetAxis("Vertical");

			float timeMultiplier = Time.deltaTime * MOVEMENT_SPEED;
			float horizontal = horizontalAxis * timeMultiplier;
			float vertical = verticalAxis * timeMultiplier;

			bool onGround = isOnGround(transform, playerModel.DistanceToGround);
			bool strafeLeft = horizontalAxis < 0f;
			bool strafeRight = horizontalAxis > 0f;
			Debug.Assert(strafeLeft == false || strafeRight == false);

			bool isJumping = userIO.GetKeyDown(KeyCode.Space) && onGround;

			bool isCrouch = userIO.GetKey(KeyCode.LeftControl);
			bool isSprint = userIO.GetKey(KeyCode.LeftShift);
			float forwardBackwardSpeed = onGround ? Mathf.Abs(verticalAxis) : 0.0f;

			playerAnimator.updateAnimations(horizontal, vertical, forwardBackwardSpeed, isJumping, strafeLeft, strafeRight, isCrouch, isSprint);
			crouchStand.crouchDownOrStandUp(isCrouch);

			if (isJumping)
			{
				jump();
			}
			float inGroundMultiplier = onGround ? 1000 : 500;
			float multiplier = isSprint ? inGroundMultiplier * 2 : inGroundMultiplier;
			rigidBody.AddForce(transform.forward * multiplier * vertical, ForceMode.Acceleration);
			rigidBody.AddForce(transform.right * multiplier * horizontal, ForceMode.Acceleration);
		}

		void OnCollisionEnter(Collision collision)
		{
			var what = collision.collider.gameObject;

			// Bug waiting to happen but right now we only want to work with the terrain w/regards to jumping.
			if (what.name == "Terrain")
			{
				isFalling = false;
			}
		}

		public bool isWeaponEquipped()
		{
			return playerModel.isWeaponEquipped();
		}

		public int equippedWeaponAmmoCount()
		{
			return playerModel.equippedWeaponAmmoCount();
		}

		public int equippedWeaponMaxAmmo()
		{
			return playerModel.equippedWeaponMaxAmmo();
		}

		private void maybeToggleIsDead(bool toggleLifeDeath)
		{
			if (toggleLifeDeath)
			{
				if (playerModel.isDead() && !playerModel.isReviving())
				{
					revive();
				}
				else
				{
					kill();
				}
			}
		}

		private void jump()
		{
			rigidBody.AddForce(Vector3.up * JUMP_FORCE, ForceMode.Impulse);
			isFalling = true;
			// TODO: adjust capsule collider for duration of jump, so we can jump up on things.
		}

		private void kill()
		{
			playerModel.kill();
			deathBehavior.setDead();
			playerAnimator.playDeathAnimation();
			kameraController.death();
		}

		private void revive()
		{
			playerModel.revive();
			deathBehavior.setAlive();
			playerAnimator.playReviveAnimation();
			kameraController.revive();
		}

		private bool isOnGround(Transform playerTransform, float distanceToGround)
		{
			// 0.1 offset deals with "irregularities" in the ground
			return Physics.Raycast(playerTransform.position, -Vector3.up, distanceToGround + 0.01f);
		}
	}
}