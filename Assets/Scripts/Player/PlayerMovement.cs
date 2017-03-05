using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField] private GameObject PlayerGO;
		[SerializeField] private float movementSpeed = 10.0f;
		[SerializeField] private float jumpForce = 500.0f;
		[SerializeField] private UserIO userIO;

		private PlayerAnimate playerAnimate;
		private PlayerCrouchStand crouchStand;
		private CapsuleCollider capsuleCollider;
		private Rigidbody rigidBody;

		private float timeUntilJumpingAllowed = 0.0f;
		private float distanceToGround;
		private bool isCrouching = false;

		void Start()
		{
			playerAnimate = GetComponent<PlayerAnimate>();
			capsuleCollider = PlayerGO.GetComponent<CapsuleCollider>();
			crouchStand = GetComponent<PlayerCrouchStand>();
			rigidBody = PlayerGO.GetComponent<Rigidbody>();
			distanceToGround = PlayerGO.GetComponent<Collider>().bounds.extents.y;
		}

		void FixedUpdate()
		{
			float timeMultiplier = Time.deltaTime * movementSpeed;
			float horizontalAxis = userIO.GetAxis("Horizontal");
			float verticalAxis = userIO.GetAxis("Vertical");

			float horizontal = horizontalAxis * timeMultiplier;
			float vertical = verticalAxis * timeMultiplier;

			Vector3 movement = (PlayerGO.transform.forward * vertical) + (PlayerGO.transform.right * horizontal);
			PlayerGO.transform.Translate(movement, Space.World);

			bool strafeLeft = horizontalAxis < 0f;
			bool strafeRight = horizontalAxis > 0f;
			Debug.Log("sl '" + strafeLeft + "', sr '" + strafeRight + "'");
			bool canJump = isOnGround() && (timeUntilJumpingAllowed < Time.time);
			bool isJumping = userIO.GetKeyDown(KeyCode.Space) && canJump;

			playerAnimate.updateAnimations(horizontal, vertical, verticalAxis, isJumping, strafeLeft, strafeRight);

			if (isJumping)
			{
				jump();
			}
			crouchStand.crouchStandOverTime(userIO.GetKey(KeyCode.LeftControl));
		}

		private void jump()
		{
			rigidBody.AddForce(Vector3.up * jumpForce);
		}

		private bool isOnGround()
		{
			// 0.1 offset deals with "irregularities" in the ground
			return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
		}
	}
}