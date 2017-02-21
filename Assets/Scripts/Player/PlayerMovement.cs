using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] private float movementSpeed = 10.0f;
	[SerializeField] private float jumpForce = 500.0f;
	[SerializeField] private UserIO userIO;
	[SerializeField] private Camera kam;
	private PlayerAnimate animator;

	void Start() {
		animator = GetComponent<PlayerAnimate>();
	}

	void FixedUpdate () {
		float timeMultiplier = Time.deltaTime * movementSpeed;
		float horizontalAxis = userIO.GetAxis ("Horizontal");
		float verticalAxis = userIO.GetAxis("Vertical");

		float horizontal = horizontalAxis * timeMultiplier;
		float vertical = verticalAxis * timeMultiplier;

		Vector3 movement = (transform.forward * vertical) + (transform.right * horizontal);
		transform.Translate(movement, Space.World);

		bool strafeAnimation = Mathf.Abs(horizontalAxis) > 0.25f;
		animator.movementUpdates(horizontal, vertical, strafeAnimation);

		if (userIO.GetKeyDown(KeyCode.Space))
		{
			jump();
		}
	}

	private void jump() {
		GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
	}
}
