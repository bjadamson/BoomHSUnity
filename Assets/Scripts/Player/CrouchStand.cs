using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
	public class CrouchStand : MonoBehaviour
	{
		[SerializeField] private GameObject head;
		[SerializeField] private float crouchingAmount = 0.8f;

		private CapsuleCollider capsuleCollider;
		private bool isCrouching = false;

		void Start()
		{
			capsuleCollider = GetComponent<CapsuleCollider>();
		}

		public void crouchDownOrStandUp(bool toCrouch) {
			if (!isCrouching && !toCrouch)
			{
				return;
			}
			else if (toCrouch && !isCrouching)
			{
				crouch();
			}
			else if (isCrouching && !toCrouch)
			{
				standup();
			}
		}

		private void crouch() {
			var center = capsuleCollider.center;
			capsuleCollider.center = new Vector3(center.x, center.y - (crouchingAmount / 2), center.z);
			capsuleCollider.height -= crouchingAmount;

			var pos = head.transform.localPosition;
			head.transform.localPosition = new Vector3(pos.x, pos.y - crouchingAmount, pos.z);

			isCrouching = true;
		}

		private void standup() {
			var center = capsuleCollider.center;
			capsuleCollider.center = new Vector3(center.x, center.y + (crouchingAmount / 2), center.z);
			capsuleCollider.height += crouchingAmount;

			var pos = head.transform.localPosition;
			head.transform.localPosition = new Vector3(pos.x, pos.y + crouchingAmount, pos.z);

			isCrouching = false;
		}
	}
}