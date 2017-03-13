using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
	private CursorStatus cursorStatus = CursorStatus.NormalMode;

	enum CursorStatus
	{
		NormalMode,
		InputMode
	}

	public void toggleCursor()
	{
		if (cursorStatus == CursorStatus.NormalMode)
		{
			unlockCursor();
		}
		else
		{
			lockCursor();
		}
	}

	public void unlockCursor()
	{
		SetCursorState(CursorLockMode.None);
		cursorStatus = CursorStatus.InputMode;
	}

	public void lockCursor()
	{
		SetCursorState(CursorLockMode.Locked);
		cursorStatus = CursorStatus.NormalMode;
	}

	private void SetCursorState(CursorLockMode mode)
	{
		Cursor.lockState = mode;
		Cursor.visible = (CursorLockMode.Locked != mode);
	}
}