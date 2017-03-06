using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace environment
{
	public class MeteorFragment : MonoBehaviour
	{
		private float FragmentLifetime = 5.0f;

		void Start() {
			Destroy(gameObject, FragmentLifetime);
		}
	}
}