using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
	public class CrosshairControl : MonoBehaviour
	{
		Animator anim;

		// Use this for initialization
		void Start()
		{
			this.anim = GetComponent<Animator>();
		}
	
		public void animate()
		{
			anim.SetTrigger("Shoot");
		}
	}
}