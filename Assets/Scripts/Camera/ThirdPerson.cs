using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace camera
{
	public class ThirdPerson : MonoBehaviour
	{
		[SerializeField] public GameObject playerGO;
		[SerializeField] public Transform playerModel;
		[SerializeField] public Transform modelHead;
		private Freelook freelook;

		// state
		private float verticalRot = 0.0f;
		private float horizontalRot = 0.0f;
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
			verticalRot = mouseAxis.y;
			horizontalRot = mouseAxis.x;

			if (!freelook.IsFreelookModeActive())
			{
				// rotate around local axis
				playerGO.transform.RotateAround(playerGO.transform.position, playerGO.transform.up, Input.GetAxis("Mouse X") * 150 * Time.deltaTime);
			}
		}

		void LateUpdate()
		{
			if (lockTransform_)
			{
				return;
			}
			if (!freelook.IsFreelookModeActive())
			{
				// 1) rotate the camera up/down
				transform.RotateAround(modelHead.position, modelHead.right, -verticalRot);

				// 2) If we rotated up/down too far, rotate back.
				if (withinThreshold(transform.up, Vector3.up))
				{
					// Rotated too far, rotate back
					transform.RotateAround(modelHead.position, modelHead.right, verticalRot);
				}

				// 3) The camera shouldn't ever rotate around the Z axis, that doesn't yield a good user experience.
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
			}
		}

		public void lockTransform()
		{
			lockTransform_ = true;
		}

		public void unlockTransform()
		{
			lockTransform_ = false;
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