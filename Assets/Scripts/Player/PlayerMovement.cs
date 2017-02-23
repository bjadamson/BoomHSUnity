using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField] private float movementSpeed = 10.0f;
		[SerializeField] private float jumpForce = 500.0f;
		[SerializeField] private UserIO userIO;

		private PlayerAnimate animator;
		private PlayerCrouchStand crouchStand;
		private CapsuleCollider capsuleCollider;
		private Rigidbody rigidBody;

		private float timeUntilJumpingAllowed = 0.0f;
		private float distanceToGround;
		private bool isCrouching = false;

		void Start()
		{
			animator = GetComponent<PlayerAnimate>();
			capsuleCollider = GetComponent<CapsuleCollider>();
			crouchStand = GetComponent<PlayerCrouchStand>();
			rigidBody = GetComponent<Rigidbody>();
			distanceToGround = GetComponent<Collider>().bounds.extents.y;
		}

		void FixedUpdate()
		{
			float timeMultiplier = Time.deltaTime * movementSpeed;
			float horizontalAxis = userIO.GetAxis("Horizontal");
			float verticalAxis = userIO.GetAxis("Vertical");

			float horizontal = horizontalAxis * timeMultiplier;
			float vertical = verticalAxis * timeMultiplier;

			Vector3 movement = (transform.forward * vertical) + (transform.right * horizontal);
			transform.Translate(movement, Space.World);

			bool strafeAnimation = Mathf.Abs(horizontalAxis) > 0.25f;
			bool canJump = isOnGround() && (timeUntilJumpingAllowed < Time.time);
			bool isJumping = userIO.GetKeyDown(KeyCode.Space) && canJump;

			animator.updateAnimations(horizontal, vertical, verticalAxis, isJumping, strafeAnimation);

			if (isJumping)
			{
				Invoke("jump", 0.5f);
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