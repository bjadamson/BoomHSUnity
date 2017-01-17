using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
	[SerializeField]
	public Transform player;

	[SerializeField]
	private float smooth;

	[SerializeField]
	private Transform followTransform;

	private Vector3 cameraSmoothVelocity = Vector3.zero;
	private bool freeLookMode = false;

	private Vector3 prevPosition = new Vector3();
	private Quaternion prevRotation = new Quaternion();

	void Update() {
		if (Input.GetKeyDown (KeyCode.Mouse3)) {
			freeLookMode = true;
			prevPosition = transform.localPosition;
			prevRotation = transform.localRotation;
		} else if (Input.GetKeyUp(KeyCode.Mouse3)) {
			freeLookMode = false;
			transform.localPosition = prevPosition;
			transform.localRotation = prevRotation;
		}
	}

	void LateUpdate() {
		if (freeLookMode) {
			updateLateFreelookCameraTransform ();
		} else {
			updateLateThirdPersonCameraTransform ();
		}
	}

	private bool withinThreshold(Vector3 a, Vector3 b) {
		const float DELTA = 0.05f;
		return Vector3.Dot (a, b) < DELTA;
	}

	private Vector2 getMouseAxis() {
		const float multiplier = 150.0f;
		float horizontal = Input.GetAxis ("Mouse X");
		float vertical = Input.GetAxis ("Mouse Y");
		return new Vector2(horizontal * Time.deltaTime * multiplier, vertical * Time.deltaTime * multiplier);
	}

	void updateLateFreelookCameraTransform ()
	{
		Vector3 playerPosition = followTransform.position;

		Vector3 lookDirection = playerPosition - transform.position;
		lookDirection.y = 0;
		lookDirection.Normalize ();

		float horizontal = getMouseAxis().x;
		Vector3 horizontalOrientation = (Vector3.up * horizontal).normalized;


		transform.RotateAround (followTransform.position, horizontalOrientation, Mathf.Abs(horizontal));
		if (withinThreshold (player.right, transform.right)) {
			// Rotated too far, rotate back
			transform.RotateAround (followTransform.position, horizontalOrientation, -Mathf.Abs(horizontal));
		}
	}

	void updateLateThirdPersonCameraTransform ()
	{
		Vector2 mouseAxis = getMouseAxis ();
		player.RotateAround (player.position, Vector3.up, mouseAxis.x);

		float vertical = mouseAxis.y;
		transform.RotateAround (followTransform.position, followTransform.right, -vertical);
		if (withinThreshold (player.up, transform.up)) {
			// Rotated too far, rotate back
			transform.RotateAround (followTransform.position, followTransform.right, vertical);
		}
	}
}