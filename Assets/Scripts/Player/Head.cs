using UnityEngine;

namespace player
{
	public class Head : MonoBehaviour
	{
		[SerializeField] private GameObject playerGO;

		void Update() {
			var headOffset = new Vector3(0.0f, 1.0f, 0.0f);
			transform.position = playerGO.transform.position + headOffset;
		}
	}
}