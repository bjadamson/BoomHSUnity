﻿using UnityEngine;
using UnityEngine.UI;
using weapon;
using camera;
using ui;

namespace player
{
	public class PlayerStateModel
	{
		// constants
		private readonly float TIME_FOR_REVIVE_ANIMATION = 3.0f;
		private readonly float TIME_FOR_RELOAD = 1.5f;

		// camera
		private readonly CameraController kameraController;

		// environment
		public readonly float DistanceToGround;

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

		// storage
		private readonly Inventory inventory;

		// ui
		private readonly UIManager uiManager;

		internal PlayerStateModel(float distance, Inventory inventory, UIManager uiMgr, CameraController camController)
		{
			this.DistanceToGround = distance;
			this.inventory = inventory;
			this.uiManager = uiMgr;
			this.kameraController = camController;
		}

		public void addWeapon(WeaponModel w, int? equippedPosition) {
			if (equippedPosition.HasValue)
			{
				inventory.equipItem(w, equippedPosition.Value);
			}
			else
			{
				Debug.Assert(0 == 1);
				//inventory.addWeapon(w);
			}
		}

		public void maybeReload(bool reloadKeyPressed)
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

		public void unequipEquippedWeapon(PlayerAnimate playerAnimator, WeaponSlotGameObjects weaponSlotsGOs)
		{
			// 1) If there isn't a weapon equipped, nothing to do.
			if (!isWeaponEquipped())
			{
				return;
			}
				
			// 2) Find the index for the weapon on the player's back.
			var equippedItemIndex = inventory.equippedItemPositionOnBody(activeWeaponModel);
			if (equippedItemIndex == null)
			{
				return;
			}

			// 3) Confirm that we only ever unequip items that our inventory is aware of.
			Debug.Assert(equippedItemIndex != null);
			var backSlotGO = weaponSlotsGOs.BackWeaponSlots[equippedItemIndex.Value];
			Debug.Assert(backSlotGO != null);

			// 4) Reparent the equipped weapon to the back slot.
			activeWeaponModel.reparentUnderGO(backSlotGO);
			activeWeaponModel = null;

			// 5) Animate putting away the weapon.
			playerAnimator.sheathWeapon();
		}

		public void equipWeaponSlot(int index, PlayerAnimate playerAnimator, WeaponSlotGameObjects weaponSlotsGOs)
		{
			// 1) If reloading, stop.
			startedReloading = false;
			if (activeWeaponModel != null)
			{
				activeWeaponModel.stopReloadingAnimation();
			}

			// 2) Move the equipped weapon to the player's back.
			unequipEquippedWeapon(playerAnimator, weaponSlotsGOs);

			// 3) Find the active weapon by index.
			var weapon = inventory.getEquippedItem(index).WeaponBehavior;
			Debug.Assert(weapon != null);
			activeWeaponModel = inventory.getEquippedItem(index);

			// 4) Reparent the active weapon GO to the equipped weapon slot GO.
			var equippedWeaponSlot = weaponSlotsGOs.EquippedRHS;
			activeWeaponModel.reparentUnderGO(equippedWeaponSlot);

			// 5) instruct the animator to equip the weapon
			playerAnimator.equipWeapon();

			// 6) Finally update the inventory UI icon
			uiManager.setItem(index, weapon.Icon, 1.0f);
		}

		public void shootIfAppropriate(bool fire1Pressed, bool fire1HeldDown)
		{
			if (startedReloading || isFreelookMode)
			{
				return;
			}
			activeWeaponModel.shootIfAble(fire1Pressed, fire1HeldDown);
		}

		public void adsIfAppropriate(bool fire2Down, bool fire2Up, PlayerAnimate playerAnimator, GameObject equippedADS, GameObject equippedRHS)
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

		public void kill()
		{
			health = -1;
		}

		public void revive()
		{
			health = 100;
			timeWhenRevivingFinished = Time.time + TIME_FOR_REVIVE_ANIMATION;
		}

		public bool isDead()
		{
			return health < 0;
		}

		public bool isReviving()
		{
			return timeWhenRevivingFinished >= Time.time;
		}

		public bool isADS()
		{
			return isPlayerADS;
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

		#region Private Methods

		private bool isReloadingAnimationFinished()
		{
			return timeWhenReloadingFinished < Time.time;
		}

		#endregion
	}
}