using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ui
{
	public class CrosshairControl : MonoBehaviour
	{
		[SerializeField] private GameObject hitIndicator;
		private readonly float hitIndicatorVisibleTime = 0.25f;

		private Animator anim;

		// Use this for initialization
		void Start()
		{
			this.anim = GetComponent<Animator>();

			hitIndicator.SetActive(false);
		}
	
		public void animateShooting()
		{
			anim.SetTrigger("Shoot");
		}

		public void showThenHideHitIndicator() {
			hitIndicator.SetActive(true);
			Invoke("hideHitIndicator", hitIndicatorVisibleTime);
		}

		private void hideHitIndicator() {
			hitIndicator.SetActive(false);
		}
	}
}