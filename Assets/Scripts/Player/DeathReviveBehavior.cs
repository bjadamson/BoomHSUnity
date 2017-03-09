using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
	public class DeathReviveBehavior : MonoBehaviour
	{
		private readonly Quaternion TargetRotation = Quaternion.Euler(new Vector3(-90, -90, 90));
		[SerializeField] private GameObject PlayerGO;

		private Rigidbody rigidBody;
		private CapsuleCollider capsuleCollider;

		// state
		private bool transitioningToDead = false;
		private Vector3 targetPosition;

		void Start()
		{
			capsuleCollider = PlayerGO.GetComponent<CapsuleCollider>();
			rigidBody = PlayerGO.GetComponent<Rigidbody>();
		}

		public void setDead()
		{
			var positionConstraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
			rigidBody.constraints = RigidbodyConstraints.FreezeRotation | positionConstraints;
			rigidBody.useGravity = false;

			capsuleCollider.direction = 0; // x-axis
			capsuleCollider.center = new Vector3(0, 0.6f, 0);

			targetPosition = PlayerGO.transform.localPosition;
			targetPosition.y = -0.5f;

			transitioningToDead = true;
		}

		public void setAlive()
		{
			rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
			rigidBody.useGravity = true;

			capsuleCollider.direction = 1; // y-axis
			capsuleCollider.center = new Vector3(0, 0.53f, 0);

			var playerPos = PlayerGO.transform.localPosition;
			playerPos.y = 0.11f;
			PlayerGO.transform.localPosition = playerPos;

			targetPosition = playerPos;

			transitioningToDead = false;
		}

		void Update()
		{
			if (!transitioningToDead)
			{
				return;
			}
			PlayerGO.transform.position = Vector3.Lerp(PlayerGO.transform.position, targetPosition, 0.35f * Time.deltaTime);
		}
	}
}