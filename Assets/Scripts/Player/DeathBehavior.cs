using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
	public class DeathBehavior : MonoBehaviour
	{
		private readonly Quaternion TargetRotation = Quaternion.Euler(new Vector3(-90, -90, 90));
		[SerializeField] private GameObject PlayerGO;

		private Rigidbody rigidBody;
		private CapsuleCollider capsuleCollider;

		// state
		Vector3 targetPosition;

		void Start()
		{
			rigidBody = PlayerGO.GetComponent<Rigidbody>();
			var positionConstraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
			rigidBody.constraints = RigidbodyConstraints.FreezeRotation | positionConstraints;
			rigidBody.useGravity = false;

			capsuleCollider = PlayerGO.GetComponent<CapsuleCollider>();
			capsuleCollider.direction = 0; // x-axis
			capsuleCollider.center = new Vector3(0, 0.6f, 0);

			targetPosition = PlayerGO.transform.position;
			targetPosition.y = -0.5f;
		}

		void Update()
		{
			PlayerGO.transform.position = Vector3.Lerp(PlayerGO.transform.position, targetPosition, 0.35f * Time.deltaTime);
		}
	}
}