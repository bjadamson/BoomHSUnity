using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
	[SerializeField]
	public Camera mainCamera;

	[SerializeField]
	private Vector3 distanceAway = new Vector3 (0, 0, 2);

	[SerializeField]
	private float smooth;

	[SerializeField]
	private Transform followTransform;

	[SerializeField]
	private float cameraSmoothDampTime = 0.1f;

	[SerializeField]
	float rotationSpeed = 5;

	[SerializeField]
	float maxAngle = 70.0f;

	public Transform head;

	private Vector3 cameraSmoothVelocity = Vector3.zero;
	private bool freeLookMode = false;

	private Vector3 prevPosition = new Vector3();
	private Quaternion prevRotation = new Quaternion();

	void Update() {
		if (Input.GetKeyDown (KeyCode.Mouse3)) {
			freeLookMode = true;
			prevPosition = transform.localPosition;
			prevRotation = transform.localRotation;
			Debug.Log ("");
		} else if (Input.GetKeyUp(KeyCode.Mouse3)) {
			freeLookMode = false;
			transform.localPosition = prevPosition;
			transform.localRotation = prevRotation;
			Debug.Log ("");
		}
	}

	void LateUpdate() {
		Vector3 playerPosition = followTransform.position;

		Vector3 lookDirection = playerPosition - transform.position;
		lookDirection.y = 0;
		lookDirection.Normalize ();
		Debug.DrawRay (playerPosition, lookDirection * 20, Color.white);

		// debug
		Debug.DrawRay(playerPosition, followTransform.forward * 20, Color.black);

		if (freeLookMode) {
			Vector3 targetPosition = playerPosition - (lookDirection * 2);
			float horizontal = Input.GetAxis ("Mouse X");// * rotationSpeed;
			float vertical = Input.GetAxis ("Mouse Y");// * rotationSpeed;

			Debug.Log ("tp camera's local/world position: '" + transform.localPosition + "'/'" + transform.position + "'");
			//transform.RotateAround (followTransform.position, Vector3.up, horizontal);

			Vector3 movement = new Vector3 (0.0f, horizontal, 0);
			transform.RotateAround (followTransform.position, movement.normalized, 5);

			const float X_DELTA = 0.05f;
			bool perpendicular = Vector3.Dot (head.right, transform.right) < X_DELTA;
			//if (perpendicular) {
				// Rotated too far, rotate back
			//	transform.RotateAround (followTransform.position, Vector3.up, -horizontal);
			//}

			//transform.RotateAround (followTransform.position, Vector3.left, vertical);
			const float Y_DELTA = 0.10f;
			bool perpendicular2 = Vector3.Dot (head.up, transform.up) < (Y_DELTA);
			//if (perpendicular2) {
				// Rotated too far, rotate back
			//	transform.RotateAround (followTransform.position, Vector3.left, -vertical);
			//}
		}
	}
}