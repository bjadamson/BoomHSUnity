using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour {
	[SerializeField] private GameObject backSlot;
	[SerializeField] private GameObject equippedSlot;
	[SerializeField] private GameObject weapon;
	[SerializeField] private UserIO userIO;

	void Update() {
		if (userIO.GetKeyDown (KeyCode.Alpha1)) {
			equip ();
		} else if (userIO.GetKeyDown (KeyCode.BackQuote)) {
			putAway ();
		}
	}

	void equip() {
		weapon.transform.SetParent (equippedSlot.transform);
		weapon.transform.localPosition = Vector3.zero;
		weapon.transform.localRotation = Quaternion.identity * Quaternion.AngleAxis(180, Vector3.up);
	}

	void putAway() {
		weapon.transform.SetParent (backSlot.transform);
		weapon.transform.localPosition = weapon.transform.parent.localPosition;
		weapon.transform.localRotation = weapon.transform.parent.localRotation;
	}
}
