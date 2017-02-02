using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	private float multiplier = 150.0f;

	void FixedUpdate () {
		Vector3 forward = transform.forward;
		//Vector3 right = transform.right;

	//	float horizontal = Input.GetAxis ("Horizontal") * Time.deltaTime * multiplier;
		//float vertical = Input.GetAxis ("Vertical") * Time.deltaTime * multiplier;

		//transform.Translate (forward * 20);
		//transform.Translate (right * horizontal);


		float horizontal = Input.GetAxis ("Horizontal") * Time.deltaTime * multiplier;
		float vertical = Input.GetAxis("Vertical") * Time.deltaTime * multiplier;
		//Vector3 movement = new Vector3(horizontal, 0.0f, vertical);

		//transform.rotation = Quaternion.LookRotation(movement);
		Debug.Log("vertical is '" + vertical + "'");
		Debug.DrawRay (transform.position, forward * 20, Color.white);
		Debug.DrawRay (transform.position, forward * 20 * vertical, Color.red);

		Vector3 movement = (transform.forward * vertical) + (transform.right * horizontal);
		transform.Translate(movement, Space.World);
	}
}
