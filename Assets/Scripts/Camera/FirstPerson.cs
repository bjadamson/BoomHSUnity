using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace camera
{
	public class FirstPerson : MonoBehaviour
	{
		[SerializeField] public GameObject playerGO;
		[SerializeField] public Transform player;
		[SerializeField] public Transform head;
		private Freelook freelook;

		// state
		private float horizontalRot = 0.0f;
		private float verticalRot = 0.0f;
		private bool lockTransform_ = false;

		void Start()
		{
			freelook = GetComponentInParent<Freelook>();
			Debug.Assert(freelook != null);
		}

		void Update()
		{
			if (lockTransform_)
			{
				return;
			}

			Vector2 mouseAxis = getMouseAxis();
			horizontalRot = mouseAxis.x;// * mouseSensitivity;
			verticalRot = mouseAxis.y;// * mouseSensitivity;

			if (!freelook.IsFreelookModeActive())
			{
				// rotate around local axis
				playerGO.transform.RotateAround(playerGO.transform.position, playerGO.transform.up, horizontalRot);
			}

			transform.position = head.position;
		}

		public void lockTransform()
		{
			lockTransform_ = true;
		}

		public void unlockTransform()
		{
			lockTransform_ = false;
		}

		void LateUpdate()
		{
			if (lockTransform_)
			{
				return;
			}

			if (!freelook.IsFreelookModeActive())
			{
				// 1) set camera's horizontal rotation to match the player's rotation.
				var newRot = transform.localEulerAngles;
				newRot.y = playerGO.transform.localEulerAngles.y;
				transform.localEulerAngles = newRot;

				// 2) rotate the camera up/down
				transform.RotateAround(transform.position, transform.right, -verticalRot);

				// 3) If we rotated up/down too far, rotate back.
				if (withinThreshold(transform.up, Vector3.up))
				{
					// Rotated too far, rotate back
					transform.RotateAround(transform.position, transform.right, verticalRot);
				}

				// 4) The camera shouldn't ever rotate around the Z axis, that doesn't yield a good user experience.
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
			}
		}

		private Vector2 getMouseAxis()
		{
			const float multiplier = 150.0f;
			float horizontal = Input.GetAxis("Mouse X");
			float vertical = Input.GetAxis("Mouse Y");
			return new Vector2(horizontal * Time.deltaTime * multiplier, vertical * Time.deltaTime * multiplier);
		}

		private static bool withinThreshold(Vector3 a, Vector3 b)
		{
			const float DELTA = 0.05f;
			return Vector3.Dot(a, b) < DELTA;
		}
	}
}