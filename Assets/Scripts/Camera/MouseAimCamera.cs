using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAimCamera : MonoBehaviour {
    public Transform head;

	[SerializeField]
	private Transform followTransform;

	[SerializeField]
    public float rotationSpeed = 5;

    Vector3 offset;


	/*

     
    void Start() {
		offset = followTransform.transform.position - transform.position;
    }

	void OnEnable() {
		prevPosition = transform.position;
		prevRotation = transform.rotation;

		Debug.Log ("OnEnable");
	}

	void OnDisable() {
		transform.position = prevPosition;
		transform.rotation = prevRotation;
		Debug.Log ("OnDisable");
	}

	void Update() {
		//if (Input.GetKeyDown (KeyCode.Mouse3) || Input.GetKeyUp (KeyCode.Mouse3)) {
			//freeLookMode = true;// ();
		//}
	}
     
    void LateUpdate() {
		transform.position = followTransform.transform.position - offset.normalized;
		transform.LookAt (followTransform);

		float angleTurned = Vector3.Angle (transform.forward, head.forward);
		Debug.Log ("angleTurned '" + angleTurned + "'");

		float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
		var maybeNewRotation = transform.localRotation * Quaternion.AngleAxis (horizontal, Vector3.up);
		if (angleTurned + horizontal <= 140.0f) {
			//transform.RotateAround (head.position, Vector3.up, horizontal);
		}
    }*/
}