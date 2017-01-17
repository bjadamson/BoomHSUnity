using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
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
	float maxAngle = 30.0f;

	private Vector3 cameraSmoothVelocity = Vector3.zero;
	private bool freeLookMode = false;
	private Vector3 prevPosition = new Vector3();
	private Quaternion prevRotation = new Quaternion();

	Vector2 prevMousePosition;

	void Start() {
		prevMousePosition = Input.mousePosition;
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Mouse3)) {
			freeLookMode = true;
			prevPosition = transform.position;
			prevRotation = transform.rotation;
			Debug.Log ("");
		} else if (Input.GetKeyUp(KeyCode.Mouse3)) {
			freeLookMode = false;
			transform.position = prevPosition;
			transform.rotation = prevRotation;
			Debug.Log ("");
		}
	}

	void LateUpdate() {
		Vector3 playerOffset = followTransform.position;

		Vector3 lookDirection = playerOffset - transform.position;
		lookDirection.y = 0;
		lookDirection.Normalize ();
		Vector3 targetPosition = playerOffset + (distanceAway.x * Vector3.right) + (followTransform.up * distanceAway.y) - (lookDirection * distanceAway.z);

		if (freeLookMode) {
			snapToPosition (targetPosition);
			float horizontal = Input.GetAxis ("Mouse X") * rotationSpeed;
			float currentlyRotated = Quaternion.Angle(transform.rotation, Quaternion.AngleAxis(0, Vector3.up));

			transform.RotateAround (transform.position, Vector3.up, horizontal);

			float cr2 = Quaternion.Angle(transform.rotation, Quaternion.AngleAxis(0, Vector3.up));
			if (cr2 > maxAngle) {
				transform.RotateAround (transform.position, Vector3.up, -horizontal);
			}
		} else {
			smoothToPosition (transform.position, targetPosition);
			transform.LookAt (followTransform);
		}
		prevMousePosition = Input.mousePosition;
	}

	private void snapToPosition(Vector3 targetPos) {
		transform.position = targetPos;
	}

	private void smoothToPosition(Vector3 fromPos, Vector3 toPos) {
		transform.position = Vector3.SmoothDamp (fromPos, toPos, ref cameraSmoothVelocity, cameraSmoothDampTime);
	}
}