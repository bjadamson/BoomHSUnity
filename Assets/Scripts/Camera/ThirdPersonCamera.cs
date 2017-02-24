using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	[SerializeField] public Transform player;
	[SerializeField] public Transform playerHead;
	[SerializeField] private float smooth;
	[SerializeField] private float mouseSensitivity = 5.0f;

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
		verticalRot = mouseAxis.y * mouseSensitivity;
		horizontalRot = mouseAxis.x * mouseSensitivity;
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

		Vector3 verticalOrientation = Vector3.right;

		Debug.DrawRay(playerHead.position, playerHead.right * 30, Color.red);
		Debug.DrawRay(playerHead.position, playerHead.forward * 30, Color.green);
		//transform.RotateAround(playerHead.position, verticalOrientation, verticalRot);

		//if (withinThreshold(playerHead.up, transform.up))
		//{
			// Rotated too far, rotate back
		//	transform.RotateAround(playerHead.position, verticalOrientation, -verticalRot);
		//}

		//Vector3 rot = transform.localRotation.eulerAngles;
		//rot.x -= verticalRot;

		//if (rot.x >= 0.0f && rot.x <= 360.0f)
		//{
		//	transform.localRotation = Quaternion.Euler(rot);
		//}

		//Vector3 pos = transform.localPosition;
		//pos.y += (-verticalRot) / 100;
		//transform.localPosition = pos;

		//transform.RotateAround(playerHead.position, verticalOrientation, -verticalRot);
		//if (withinThreshold(playerHead.up, transform.up))
		//{
			// Rotated too far, rotate back
		//	transform.RotateAround(playerHead.position, verticalOrientation, verticalRot);
		//}
	}

	private Vector2 getMouseAxis()
	{
		const float multiplier = 150.0f;
		float horizontal = Input.GetAxis("Mouse X");
		float vertical = Input.GetAxis("Mouse Y");
		return new Vector2(horizontal * Time.deltaTime * multiplier, vertical * Time.deltaTime * multiplier);
	}

	private bool withinThreshold(Vector3 a, Vector3 b)
	{
		const float DELTA = 0.05f;
		return Vector3.Dot(a, b) < DELTA;
	}
}