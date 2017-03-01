using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace camera
{
	public class FollowPlayer : MonoBehaviour
	{
		[SerializeField] public Transform followTarget;
		[SerializeField] private float followDistance = 1.6f;

		void Update()
		{
			transform.position = followTarget.position - (followTarget.forward * followDistance);
			transform.LookAt(followTarget);
		}
	}
}