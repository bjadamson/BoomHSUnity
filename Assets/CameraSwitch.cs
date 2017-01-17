using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {
	private ThirdPersonCamera tps;
	public Camera mainCamera;

	private MouseAimCamera mouseAim;
	public Camera freelookCamera;

	void Start () {
		//tps = GetComponent<ThirdPersonCamera> ();
		//mouseAim = GetComponent<MouseAimCamera> ();

		//freelookCamera.enabled = false;
		//mouseAim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown (KeyCode.Mouse3) || Input.GetKeyUp (KeyCode.Mouse3)) {
		//	switchCameras ();
		//}
	}

	private void switchCameras() {
		//mainCamera.enabled ^= true;
		//tps.enabled ^= true;

		//freelookCamera.enabled ^= true;
		//mouseAim.enabled ^= true;
	}
}
