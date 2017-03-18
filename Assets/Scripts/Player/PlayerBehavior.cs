using UnityEngine;
using System.Collections;
using camera;
using weapon;

// todo: move
using ui;
using ui.inventory;

namespace player
{
	public class PlayerBehavior : MonoBehaviour
	{
		[SerializeField] private Inventory inventory;
		[SerializeField] private EquipppedItems equippedItems;

		[SerializeField] private CameraController kameraController;
		[SerializeField] private UIManager uiManager;
		[SerializeField] private UserIO userIO;

		// constants
		private readonly float JUMP_FORCE = 660.0f;
		private readonly float MOVEMENT_SPEED = 10.0f;

		private CrouchStand crouchStand;
		private CapsuleCollider capsuleCollider;
		private DeathReviveBehavior deathBehavior;
		private PlayerAnimate playerAnimator;
		private Rigidbody rigidBody;

		private WeaponSlotGameObjects weaponSlotsGOs;

		// state
		private bool isFalling = false;

		// psm
		// constants
		private readonly float TIME_FOR_REVIVE_ANIMATION = 3.0f;
		private readonly float TIME_FOR_RELOAD = 1.5f;

		// environment
		private float distanceToGround;

		// weapon
		private WeaponModel activeWeaponModel;
		private bool isPlayerADS = false;
		private bool startedReloading = false;
		private float timeWhenReloadingFinished = 0.0f;

		// stats
		private int health = 100;

		// life/death
		private float timeWhenRevivingFinished = 0.0f;

		// camera
		private bool isFreelookMode = false;

		void Start()
		{
			Debug.Assert(inventory != null);
			Debug.Assert(kameraController != null);
			Debug.Assert(uiManager != null);
			Debug.Assert(userIO != null);

			deathBehavior = GetComponent<DeathReviveBehavior>();
			Debug.Assert(deathBehavior != null);

			playerAnimator = GetComponent<PlayerAnimate>();
			Debug.Assert(playerAnimator != null);

			weaponSlotsGOs = GetComponent<WeaponSlotGameObjects>();
			Debug.Assert(weaponSlotsGOs != null);

			playerAnimator = GetComponent<PlayerAnimate>();
			capsuleCollider = GetComponent<CapsuleCollider>();
			crouchStand = GetComponent<CrouchStand>();
			rigidBody = GetComponent<Rigidbody>();

			distanceToGround = GetComponent<Collider>().bounds.extents.y;
		}

		void Update()
		{
			maybeToggleIsDead(userIO.GetKeyDown(KeyCode.K));
			if (isDead())
			{
				return;
			}

			if (userIO.GetKeyDown(KeyCode.I))
			{
				uiManager.toggleInventory();
			}

			if (isWeaponEquipped())
			{
				maybeReload(userIO.GetKeyDown(KeyCode.R));
				shootIfAppropriate(userIO.GetButtonDown("Fire1"), userIO.GetButton("Fire1"));

				bool fire2Down = userIO.GetButtonDown("Fire2");
				bool fire2Up = userIO.GetButtonUp("Fire2");
				adsIfAppropriate(fire2Down, fire2Up, playerAnimator, weaponSlotsGOs.EquippedADS, weaponSlotsGOs.EquippedRHS);
			}

			if (!isADS())
			{
				bool fists = userIO.GetKeyDown(KeyCode.BackQuote);
				bool weapon1 = userIO.GetKeyDown(KeyCode.Alpha1);
				bool weapon2 = userIO.GetKeyDown(KeyCode.Alpha2);
				bool weapon3 = userIO.GetKeyDown(KeyCode.Alpha3);
				if (fists)
				{
					equipWeaponSlot(0);
				}
				else if (weapon1)
				{
					equipWeaponSlot(1);
				}
				else if (weapon2)
				{
					equipWeaponSlot(2);
				}
				else if (weapon3)
				{
					equipWeaponSlot(3);
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

			bool onGround = isOnGround(transform, distanceToGround);
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

		public void addItem(WeaponModel weapon, int? equipPos)
		{
			inventory.addItem(weapon, equipPos);
			if (equipPos.HasValue)
			{
				ifWeaponAtPositionThenEquip(equipPos.Value);
			}
		}

		public void ifWeaponAtPositionThenEquip(int position)
		{
			if (inventory.isItemIndexEquipped(position))
			{
				equipWeaponSlot(position);
			}
		}

		public bool isWeaponEquipped()
		{
			return activeWeaponModel != null;
		}

		public int equippedWeaponAmmoCount()
		{
			return activeWeaponModel.AmmoCount;
		}

		public int equippedWeaponMaxAmmo()
		{
			return activeWeaponModel.MaxAmmoCount;
		}
			
		public void swapItems(int index0, int index1)
		{
			inventory.swapItems(index0, index1);

			var item0 = inventory.lookupItemById(index0);
			var item1 = inventory.lookupItemById(index1);

			if (item0.Storage.getItem(item0.Index) == activeWeaponModel)
			{
				equipWeaponSlot(index1);
			}
			else if (item1.Storage.getItem(item1.Index) == activeWeaponModel)
			{
				equipWeaponSlot(index0);
			}
		}

		#region Private Methods

		private void maybeToggleIsDead(bool toggleLifeDeath)
		{
			if (toggleLifeDeath)
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
		}

		private void jump()
		{
			rigidBody.AddForce(Vector3.up * JUMP_FORCE, ForceMode.Impulse);
			isFalling = true;
			// TODO: adjust capsule collider for duration of jump, so we can jump up on things.
		}

		private void kill()
		{
			health = -1;
			deathBehavior.setDead();
			playerAnimator.playDeathAnimation();
			kameraController.death();
		}

		private void revive()
		{
			health = 100;
			timeWhenRevivingFinished = Time.time + TIME_FOR_REVIVE_ANIMATION;

			deathBehavior.setAlive();
			playerAnimator.playReviveAnimation();
			kameraController.revive();
		}

		private bool isOnGround(Transform playerTransform, float distanceToGround)
		{
			// 0.1 offset deals with "irregularities" in the ground
			return Physics.Raycast(playerTransform.position, -Vector3.up, distanceToGround + 0.01f);
		}

		private void maybeReload(bool reloadKeyPressed)
		{
			if (reloadKeyPressed)
			{
				if (activeWeaponModel.isClipFull())
				{
					activeWeaponModel.playClipFullSound();
				}
				else if (!startedReloading)
				{
					startedReloading = true;
					activeWeaponModel.playReloadAnimation();
					timeWhenReloadingFinished = Time.time + TIME_FOR_RELOAD;
				}
			}
			else if (startedReloading && isReloadingAnimationFinished())
			{
				activeWeaponModel.stopReloadingAnimation();
				activeWeaponModel.reload();

				startedReloading = false;
			}
		}

		private void equipWeaponSlot(int index)
		{
			// 1) If reloading, stop.
			startedReloading = false;
			if (activeWeaponModel != null)
			{
				activeWeaponModel.stopReloadingAnimation();
			}

			// 2) If there is a weapon equipped, move it to the player's back.
			if (isWeaponEquipped())
			{
				unequipEquippedWeapon();
			}

			// 3) Find the active weapon by index.
			var item = equippedItems.getEquippedItem(index);
			Debug.Assert(item != null);
			activeWeaponModel = item;

			// 4) Reparent the active weapon GO to the equipped weapon slot GO.
			var equippedWeaponSlot = weaponSlotsGOs.EquippedRHS;
			activeWeaponModel.reparentUnderGO(equippedWeaponSlot);

			// 5) instruct the animator to equip the weapon
			playerAnimator.equipWeapon();

			// 6) Draw highlight around the correct InventoryItem.
			uiManager.setWeaponHighlightIndex(index);
		}

		private void shootIfAppropriate(bool fire1Pressed, bool fire1HeldDown)
		{
			if (startedReloading || isFreelookMode)
			{
				return;
			}
			activeWeaponModel.shootIfAble(fire1Pressed, fire1HeldDown);
		}

		private void adsIfAppropriate(bool fire2Down, bool fire2Up, PlayerAnimate playerAnimator, GameObject equippedADS, GameObject equippedRHS)
		{
			if (!startedReloading)
			{
				if (fire2Down)
				{
					isPlayerADS = true;
					playerAnimator.setADS(true);
					activeWeaponModel.reparentUnderGO(equippedADS);
					kameraController.adsZoomIn();
				}
				else if (fire2Up)
				{
					isPlayerADS = false;
					playerAnimator.setADS(false);
					activeWeaponModel.reparentUnderGO(equippedRHS);
					kameraController.adsZoomOut();
				}
			}
		}

		private bool isDead()
		{
			return health < 0;
		}

		private bool isReviving()
		{
			return timeWhenRevivingFinished >= Time.time;
		}

		private bool isADS()
		{
			return isPlayerADS;
		}

		private void unequipEquippedWeapon()
		{
			// 1) Find the index for the weapon on the player's back.
			var equippedItemIndex = equippedItems.equippedItemPositionOnBody(activeWeaponModel);
			if (equippedItemIndex == null)
			{
				return;
			}

			// 2) Confirm that we only ever unequip items that our inventory is aware of.
			Debug.Assert(equippedItemIndex != null);
			var backSlotGO = weaponSlotsGOs.BackWeaponSlots[equippedItemIndex.Value];
			Debug.Assert(backSlotGO != null);

			// 3) Reparent the equipped weapon to the back slot.
			activeWeaponModel.reparentUnderGO(backSlotGO);
			activeWeaponModel = null;

			// 4) Animate putting away the weapon.
			playerAnimator.sheathWeapon();
		}

		private bool isReloadingAnimationFinished()
		{
			return timeWhenReloadingFinished < Time.time;
		}

		#endregion
	}
}