using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace camera
{
	public class ThirdPerson : MonoBehaviour
	{
		[SerializeField] public Transform player;
		[SerializeField] public Transform playerHead;

		private Freelook freelook;
		private float verticalRot = 0.0f;
		private float horizontalRot = 0.0f;

		void Start() {
			freelook = GetComponentInParent<Freelook>();
			Debug.Assert(freelook != null);
		}

		void Update()
		{
			Vector2 mouseAxis = getMouseAxis();
			verticalRot = mouseAxis.y;
			horizontalRot = mouseAxis.x;
		}

		void LateUpdate()
		{
			if (!freelook.IsFreelookModeActive())
			{
				normalTransformUpdate();
			}
		}

		private void normalTransformUpdate()
		{
			transform.RotateAround(playerHead.position, playerHead.right, -verticalRot);

			if (withinThreshold(transform.up, Vector3.up))
			{
				// Rotated too far, rotate back
				transform.RotateAround(playerHead.position, playerHead.right, verticalRot);
			}
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
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