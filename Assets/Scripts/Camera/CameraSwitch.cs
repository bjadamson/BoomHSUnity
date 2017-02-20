using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {
	[SerializeField] private Camera thirdPerson;
	[SerializeField] private Camera firstPerson;

	void Start () {
		third ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.T)) {
			if (firstPerson.gameObject.activeSelf) {
				third ();
			} else {
				first ();
			}
		}
	}

	private void first() {
		firstPerson.gameObject.SetActive (true);
		firstPerson.transform.localRotation = thirdPerson.transform.localRotation;

		thirdPerson.gameObject.SetActive (false);
	}

	private void third() {
		thirdPerson.gameObject.SetActive (true);
		thirdPerson.transform.localRotation = firstPerson.transform.localRotation;

		firstPerson.gameObject.SetActive (false);
	}
}
