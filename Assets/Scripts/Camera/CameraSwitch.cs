using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {
	[SerializeField] private ThirdPersonCamera thirdPerson;
	[SerializeField] private Camera firstPerson;
	[SerializeField] private GameObject player;

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
		thirdPerson.gameObject.SetActive (false);

		firstPerson.transform.localRotation = player.transform.localRotation;
	}

	private void third() {
		thirdPerson.gameObject.SetActive (true);
		firstPerson.gameObject.SetActive (false);

		thirdPerson.transform.localRotation = Quaternion.identity;
	}
}
