using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TransparencyManager : MonoBehaviour {
	[SerializeField] private CanvasGroup chatCanvasGroup;
	[SerializeField] private ScrollBackgroundAndHandle scrollBackground;

	public void makeOpaque() {
		chatCanvasGroup.alpha = 1.0f;
		scrollBackground.makeOpaque ();
	}

	public void makeTransparent() {
		chatCanvasGroup.alpha = 0.55f;
		scrollBackground.makeTransparent ();
	}
}