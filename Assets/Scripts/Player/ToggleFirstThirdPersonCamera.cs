using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFirstThirdPersonCamera : MonoBehaviour {
	public Camera first;
	public Camera third;

	bool cameraSwitch = false;

	void Start() {
		first.enabled = true;
		third.enabled = false;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.T)) {
			cameraSwitch = !cameraSwitch;
		}
		if (cameraSwitch) {
			first.enabled = false;
			third.enabled = true;
		} else {
			first.enabled = true;
			third.enabled = false;
		}
	}
}
