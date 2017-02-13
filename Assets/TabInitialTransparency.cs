using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpaqueTab : MonoBehaviour {
	private Button button;

	void Start () {
		button = GetComponent<Button> ();
		makeOpaque ();
	}

	void makeOpaque () {
		Color color = button.image.color;
		color.a = 1.0f;
		button.image.color = color;
	}
}