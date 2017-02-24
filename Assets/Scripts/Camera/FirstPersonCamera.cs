﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
	[SerializeField] public Transform player;
	[SerializeField] private float smooth;
	[SerializeField] private float mouseSensitivity = 1.0f;

	private float freelookHrot = 0;
	private float freelookTemp = 0;
	private bool freeLookMode = false;

	private float horizontalRot = 0.0f;
	private float verticalRot = 0.0f;

	private Vector3 prevPosition = new Vector3();
	private Quaternion prevRotation = new Quaternion();

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse3))
		{
			// Immediately capture the "right" vector for comparisons used when calculating how far
			// the freelook camera can pan.
			freelookHrot = horizontalRot;
			freelookTemp = horizontalRot;
			freeLookMode = true;
			
			prevPosition = transform.localPosition;
			prevRotation = transform.localRotation;
		}
		else if (Input.GetKeyUp(KeyCode.Mouse3))
		{
			freeLookMode = false;
			transform.localPosition = prevPosition;
			transform.localRotation = prevRotation;

			horizontalRot = 0;
		}
			
		Vector2 mouseAxis = getMouseAxis();
		if (freeLookMode)
		{
			freelookTemp += mouseAxis.x * mouseSensitivity;
		}
		else
		{
			horizontalRot = mouseAxis.x;// * mouseSensitivity;
			verticalRot = mouseAxis.y;// * mouseSensitivity;
		}
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

	private Vector2 getMouseAxis()
	{
		const float multiplier = 150.0f;
		float horizontal = Input.GetAxis("Mouse X");
		float vertical = Input.GetAxis("Mouse Y");
		return new Vector2(horizontal * Time.deltaTime * multiplier, vertical * Time.deltaTime * multiplier);
	}

	private void freelookTransformUpdate()
	{
		var hrot = Mathf.Clamp(freelookTemp, freelookHrot - 180.0f, freelookHrot + 180.0f);
		transform.localRotation = Quaternion.Euler(0, hrot, 0);
	}

	private void normalTransformUpdate()
	{
		player.RotateAround (player.position, Vector3.up, horizontalRot);
		transform.RotateAround(transform.position, transform.right, -verticalRot);

		if (withinThreshold(transform.up, Vector3.up))
		{
			// Rotated too far, rotate back
			transform.RotateAround(transform.position, transform.right, verticalRot);
		}
	}

	private static bool withinThreshold(Vector3 a, Vector3 b)
	{
		const float DELTA = 0.05f;
		return Vector3.Dot(a, b) < DELTA;
	}
}