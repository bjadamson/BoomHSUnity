using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private float multiplier = 150.0f;
	[SerializeField] private UserIO userIO;
	[SerializeField] private Camera kam;

	void FixedUpdate () {
		float horizontal = userIO.GetAxis ("Horizontal") * Time.deltaTime * multiplier;
		float vertical = userIO.GetAxis("Vertical") * Time.deltaTime * multiplier;

		Vector3 movement = (transform.forward * vertical) + (transform.right * horizontal);
		transform.Translate(movement, Space.World);
	}
}
