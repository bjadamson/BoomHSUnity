using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	[SerializeField] public Transform player;
	[SerializeField] public Transform playerHead;
	[SerializeField] private float smooth;
	private float mouseSensitivity = 1.0f;

	private Vector3 cameraSmoothVelocity = Vector3.zero;
	private bool freeLookMode = false;
	private float verticalRot = 0.0f;
	private float horizontalRot = 0.0f;

	private Vector3 prevPosition = new Vector3();
	private Quaternion prevRotation = new Quaternion();

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse3))
		{
			// Immediately capture the "right" vector for comparisons used when calculating how far
			// the freelook camera can pan.
			freeLookMode = true;
			prevPosition = transform.localPosition;
			prevRotation = transform.localRotation;
		}
		else if (Input.GetKeyUp(KeyCode.Mouse3))
		{
			freeLookMode = false;
			transform.localPosition = prevPosition;
			transform.localRotation = prevRotation;
		}
		Vector2 mouseAxis = getMouseAxis();
		verticalRot = mouseAxis.y;
		horizontalRot = mouseAxis.x;
	}


	void LateUpdate()
	{
		if (freeLookMode)
		{
			freelookTransformUpdate();
		}
		else
		{
			normalTransformUpdate();
		}
	}

	private void freelookTransformUpdate()
	{
		Vector3 horizontalOrientation = (Vector3.up * horizontalRot).normalized;

		transform.RotateAround(player.position, horizontalOrientation, Mathf.Abs(horizontalRot));
		if (withinThreshold(player.right, transform.right))
		{
			// Rotated too far, rotate back
			transform.RotateAround(player.position, horizontalOrientation, -Mathf.Abs(horizontalRot));
		}
	}

	private void normalTransformUpdate()
	{
		player.RotateAround(player.position, Vector3.up, horizontalRot);
		transform.RotateAround(playerHead.position, playerHead.right, -verticalRot);

		if (withinThreshold(transform.up, Vector3.up))
		{
			// Rotated too far, rotate back
			transform.RotateAround(playerHead.position, playerHead.right, verticalRot);
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