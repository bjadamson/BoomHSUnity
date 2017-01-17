using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomHS : MonoBehaviour {
	
	void Start () {
		SetCursorState(CursorLockMode.Locked);
	}

	private void SetCursorState (CursorLockMode mode) {
		Cursor.lockState = mode;
		Cursor.visible = (CursorLockMode.Locked != mode);
	}
}