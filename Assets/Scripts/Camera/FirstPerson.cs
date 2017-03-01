using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace camera
{
	public class FirstPerson : MonoBehaviour
	{
		[SerializeField] public Transform player;

		private Freelook freelook;
		private float horizontalRot = 0.0f;
		private float verticalRot = 0.0f;

		void Start() {
			freelook = GetComponentInParent<Freelook>();
			Debug.Assert(freelook != null);
		}

		void Update()
		{
			Vector2 mouseAxis = getMouseAxis();
			horizontalRot = mouseAxis.x;// * mouseSensitivity;
			verticalRot = mouseAxis.y;// * mouseSensitivity;
		}

		void LateUpdate()
		{
			if (!freelook.IsFreelookModeActive())
			{
				normalTransformUpdate();
			}
		}

		private Vector2 getMouseAxis()
		{
			const float multiplier = 150.0f;
			float horizontal = Input.GetAxis("Mouse X");
			float vertical = Input.GetAxis("Mouse Y");
			return new Vector2(horizontal * Time.deltaTime * multiplier, vertical * Time.deltaTime * multiplier);
		}

		private void normalTransformUpdate()
		{
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, player.localEulerAngles.y, transform.localEulerAngles.z);
			transform.RotateAround(transform.position, transform.right, -verticalRot);

			if (withinThreshold(transform.up, Vector3.up))
			{
				// Rotated too far, rotate back
				transform.RotateAround(transform.position, transform.right, verticalRot);
			}
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
		}

		private static bool withinThreshold(Vector3 a, Vector3 b)
		{
			const float DELTA = 0.05f;
			return Vector3.Dot(a, b) < DELTA;
		}
	}
}