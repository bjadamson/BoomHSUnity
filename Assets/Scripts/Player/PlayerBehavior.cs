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
		[SerializeField] private DeathReviveBehavior deathBehavior;
		[SerializeField] private UIManager uiManager;

		private WeaponSlotGameObjects WeaponSlotsGOs;

		// constants
		private readonly float JUMP_FORCE = 1000.0f;
		private readonly float MOVEMENT_SPEED = 10.0f;

		private WeaponFactory weaponFactory = new WeaponFactory();
		private PlayerAnimate playerAnimator;
		private CrouchStand crouchStand;
		private CapsuleCollider capsuleCollider;
		private Rigidbody rigidBody;

		// state
		private PlayerStateModel playerModel;

		void Start()
		{
			playerAnimator = GetComponent<PlayerAnimate>();
			Debug.Assert(playerAnimator != null);
			WeaponSlotsGOs = GetComponent<WeaponSlotGameObjects>();
			Debug.Assert(WeaponSlotsGOs != null);

			playerAnimator = GetComponent<PlayerAnimate>();
			capsuleCollider = PlayerGO.GetComponent<CapsuleCollider>();
			crouchStand = GetComponent<CrouchStand>();
			rigidBody = PlayerGO.GetComponent<Rigidbody>();

			float distanceToGround = PlayerGO.GetComponent<Collider>().bounds.extents.y;

			var weapon0 = weaponFactory.makeM4A1();
			weapon0.EquippedPosition = 0;
			var weapon1 = weaponFactory.makeAk47();
			weapon1.EquippedPosition = 1;
			var weapon2 = weaponFactory.makeM4A1();
			weapon2.EquippedPosition = 2;

			playerModel = new PlayerStateModel(distanceToGround, uiManager, weapon0, weapon1, weapon2);
			playerModel.equipWeaponSlot(0, playerAnimator, WeaponSlotsGOs);
			playerModel.equipWeaponSlot(1, playerAnimator, WeaponSlotsGOs);
			playerModel.equipWeaponSlot(2, playerAnimator, WeaponSlotsGOs);

			playerModel.unequipEquippedWeapon(playerAnimator, WeaponSlotsGOs);
		}

		void Update()
		{
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

			bool strafeLeft = horizontalAxis < 0f;
			bool strafeRight = horizontalAxis > 0f;


			bool onGround = isOnGround(PlayerGO.transform, playerModel.DistanceToGround);
			bool canJump = playerModel.canJump(onGround);
			bool isJumping = userIO.GetKeyDown(KeyCode.Space) && canJump;

			bool isCrouch = userIO.GetKey(KeyCode.LeftControl);
			bool isSprint = userIO.GetKey(KeyCode.LeftShift);
			float speed = Mathf.Abs(verticalAxis);

			playerAnimator.updateAnimations(horizontal, vertical, speed, isJumping, strafeLeft, strafeRight, isCrouch, isSprint);
			crouchStand.crouchDownOrStandUp(isCrouch);

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
			return Physics.Raycast(playerTransform.position, -Vector3.up, distanceToGround + 0.1f);
		}
	}
}