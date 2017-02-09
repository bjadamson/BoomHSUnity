using UnityEngine;
using System.Collections;

public class CameraToMouseRotator : MonoBehaviour {

	public float turnMinAngle = 3.0f;
	public Transform playerTransform;
	public Transform cameraTransform;
	public Transform playerControllerTransform;

	[SerializeField]
	public float aimSpeed;

	private void LateUpdate()
	{
		if (Input.GetKey(KeyCode.Mouse1)) // For right click
			Aim();
	}
	private void Aim()
	{
		Vector3 delta = playerTransform.position - cameraTransform.position;
		delta.y = playerTransform.position.y; // don't loo down?

		if (Vector3.Magnitude(delta) >= turnMinAngle) {
			Quaternion rotation = Quaternion.LookRotation (Vector3.Normalize(delta), Vector3.up);
			playerTransform.rotation = rotation;

			playerControllerTransform.rotation = rotation;
		}
	}
}