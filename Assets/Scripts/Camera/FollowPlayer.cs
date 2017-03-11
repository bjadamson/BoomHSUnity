using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace camera
{
	public class FollowPlayer : MonoBehaviour
	{
		[SerializeField] public Transform followTarget;
		[SerializeField] public float FollowDistance;
		[SerializeField] private float AdsZoomFovDelta;

		public void zoomIn()
		{
			Camera.main.fieldOfView -= AdsZoomFovDelta;
		}

		public void zoomOut()
		{
			Camera.main.fieldOfView += AdsZoomFovDelta;
		}

		void Update()
		{
			transform.position = followTarget.position - (followTarget.forward * FollowDistance);
			transform.LookAt(followTarget);
		}
	}
}