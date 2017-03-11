using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace camera
{
	public class Freelook : MonoBehaviour
	{
		[SerializeField] private UserIO userIO;

		private float horizontalRot = 0.0f;
		private Vector3 anchorDirectionRightVector = Vector3.zero;

		public GameObject ActiveCamera;
		public Transform TargetObject;

		public Quaternion PreviousCameraRotation;

		void OnEnable()
		{
			// Immediately capture the "right" vector for comparisons used when calculating how far the freelook camera can pan.
			if (TargetObject != null)
			{
				anchorDirectionRightVector = TargetObject.right;
			}
		}

		void OnDisable()
		{
			ActiveCamera.transform.localPosition = new Vector3(0.12f, 1.52f, 0.0f);
			ActiveCamera.transform.localRotation = Quaternion.identity;
		}

		void Update()
		{
			Vector2 mouseAxis = userIO.GetMouseAxis();
			horizontalRot = mouseAxis.x;
		}

		public bool IsFreelookModeActive()
		{
			return true == this.enabled;
		}

		void LateUpdate()
		{
			Vector3 horizontalOrientation = (Vector3.up * horizontalRot).normalized;

			ActiveCamera.transform.RotateAround(TargetObject.position, horizontalOrientation, Mathf.Abs(horizontalRot));
			if (withinThreshold(anchorDirectionRightVector, ActiveCamera.transform.right))
			{
				// Rotated too far, rotate back
				ActiveCamera.transform.RotateAround(TargetObject.position, horizontalOrientation, -Mathf.Abs(horizontalRot));
			}
		}

		private static bool withinThreshold(Vector3 a, Vector3 b)
		{
			const float DELTA = 0.05f;
			return Vector3.Dot(a, b) < DELTA;
		}
	}
}