using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
	public class Compass : MonoBehaviour
	{

		[SerializeField]
		private Transform playerTransform;

		void Update()
		{
			float angle = 360 - playerTransform.rotation.eulerAngles.y;
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}
		
}