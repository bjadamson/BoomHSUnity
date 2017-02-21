using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
	public class Bullet : MonoBehaviour
	{
		public float speed = 50.0f;

		public Vector3 Forward = Vector3.zero;
		void Update()
		{
			//transform.position += Forward * Time.deltaTime * speed;
		}
	}
}