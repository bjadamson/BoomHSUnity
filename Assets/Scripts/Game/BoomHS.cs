using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoomHS : MonoBehaviour {
	[SerializeField] private CanvasGroup chatCanvasGroup;
	[SerializeField] private CanvasGroup chatScrollbarGroup;
	
	void Start () {
		lockCursor ();
	}

	void Update() {
		// TODO: we can't do this during an Update() loop like this, we should probably make this an event handler for every time the mouse moves.
		// That would be a LOT less computation.
		if (!EventSystem.current.IsPointerOverGameObject ()) {
			chatCanvasGroup.alpha = 0.55f;
			chatScrollbarGroup.alpha = 0;
		} else {
			chatCanvasGroup.alpha = 1;
			chatScrollbarGroup.alpha = 1;
		}
	}

	public void unlockCursor() {
		SetCursorState(CursorLockMode.Confined);
	}
		
	public void lockCursor() {
		SetCursorState(CursorLockMode.Locked);
	}

	private void SetCursorState (CursorLockMode mode) {
		Cursor.lockState = mode;
		Cursor.visible = (CursorLockMode.Locked != mode);
	}
}