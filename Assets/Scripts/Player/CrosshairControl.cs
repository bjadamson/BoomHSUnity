using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairControl : MonoBehaviour {
	[SerializeField] private UserIO userIO;
	Animator anim;

	// Use this for initialization
	void Start () {
		this.anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (userIO.GetButtonDown ("Fire1")) {
			this.anim.SetTrigger ("Shoot");
		}
	}
}
