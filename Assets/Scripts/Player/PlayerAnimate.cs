using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using camera;

namespace player
{
	public class PlayerAnimate : MonoBehaviour
	{
		[SerializeField] private Animator animator;
		[SerializeField] private float directionDampTime = 0.25f;
		[SerializeField] private ThirdPerson tpCamera;
		[SerializeField] private float directionSpeed = 3.0f;
		[SerializeField] private float rotationDegreesPerSecond = 120f;
		[SerializeField] private UserIO userIO;

		private float speed = 0.0f;
		private float direction = 0.0f;
		private float horizontal = 0.0f;
		private float vertical = 0.0f;
		private AnimatorStateInfo stateInfo;

		private int m_locomotionId;

		void Start()
		{
			animator = GetComponent<Animator>();

			if (animator.layerCount >= 2)
			{
				animator.SetLayerWeight(1, 1);
			}
			m_locomotionId = Animator.StringToHash("Base Layer.Locomotion");
		}

		void Update()
		{
			stateInfo = animator.GetCurrentAnimatorStateInfo(0);
			//StickToWorldspace(transform, tpCamera.transform, ref direction, ref speed);
		}

		public void updateAnimations(float horizontal, float vertical, float speed, bool jump, bool strafe)
		{
			setJump(jump);
			setStrafe(strafe);
			setCrouch(userIO.GetKey(KeyCode.LeftControl));
			setSprint(userIO.GetKey(KeyCode.LeftShift));
			setSpeed(speed);
		}

		public void sheathWeapon()
		{
			animator.SetBool("HoldingGun", false);
		}

		public void equipWeapon()
		{
			animator.SetBool("HoldingGun", true);
		}

		public void setCrouch(bool value)
		{
			animator.SetBool("Crouching", value);
		}

		public void setJump(bool value)
		{
			animator.SetBool("Jump", value);
		}

		public void setSpeed(float value)
		{
			animator.SetFloat("Speed", value);
		}

		public void setStrafe(bool value)
		{
			animator.SetBool("Strafing", value);
		}

		public void setSprint(bool value)
		{
			animator.SetBool("Sprinting", value);
		}

		void FixedUpdate()
		{
			if (IsInLocomotion() && ((direction >= 0 && horizontal >= 0) || (direction < 0 && horizontal < 0)))
			{
				Vector3 maxLerp = new Vector3(0f, rotationDegreesPerSecond * (horizontal < 0f ? -1f : 1f), 0f);
				Vector3 rotationAmount = Vector3.Lerp(Vector3.zero, maxLerp, Mathf.Abs(horizontal));
				Quaternion deltaRotation = Quaternion.Euler(rotationAmount * Time.deltaTime);
				transform.rotation = transform.rotation * deltaRotation;
			}
		}

		/*
	private void StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut)
	{
		Vector3 rootDirection = root.forward;
		Vector3 stickDirection = new Vector3 (horizontal, 0, vertical);

		speedOut = stickDirection.sqrMagnitude;

		// camera rotation
		Vector3 cameraDirection = camera.forward;
		cameraDirection.y = 0;
		Quaternion shift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);

		// stick to worldspace
		Vector3 moveDirection = shift * stickDirection;
		Vector3 axisSign = Vector3.Cross (moveDirection, rootDirection);

		float multiplier = axisSign.y >= 0 ? -1f : 1f;
		float angleRootToMove = Vector3.Angle (rootDirection, moveDirection) * multiplier;
		angleRootToMove /= 180.0f;
		directionOut = angleRootToMove * directionSpeed;
	}
	*/

		private bool IsInLocomotion()
		{
			return stateInfo.nameHash == m_locomotionId;
		}
	}
}