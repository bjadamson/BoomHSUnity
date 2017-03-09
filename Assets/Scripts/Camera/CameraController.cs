using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace camera
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private ThirdPerson thirdPerson;
		[SerializeField] private FirstPerson firstPerson;

		[SerializeField] private GameObject player;
		[SerializeField] private GameObject playerHead;

		private Freelook freelookMode;

		void Start()
		{
			freelookMode = GetComponent<Freelook>();

			third();
			player.transform.localRotation = Quaternion.identity;
			thirdPerson.transform.localRotation = Quaternion.identity;
		}

		void Update()
		{
			if (!freelookMode.IsFreelookModeActive())
			{
				if (Input.GetKeyDown(KeyCode.T))
				{
					if (firstPerson.gameObject.activeSelf)
					{
						third();
					}
					else
					{
						first();
					}
				}
			}

			if (Input.GetKeyDown(KeyCode.Mouse3))
			{
				this.freelookMode.enabled = true;
				this.freelookMode.PreviousCameraRotation = getActiveCameraObject().transform.localRotation;
			}
			else if (Input.GetKeyUp(KeyCode.Mouse3))
			{
				this.freelookMode.enabled = false;
			}
		}

		public bool isFreelookMode() {
			return freelookMode.IsFreelookModeActive();
		}

		public bool isFirstPerson() {
			Debug.Assert(firstPerson.gameObject.activeSelf != thirdPerson.gameObject.activeSelf);
			return firstPerson.gameObject.activeSelf;
		}

		public void death() {
			thirdPerson.lockTransform();
			firstPerson.lockTransform();
		}

		public void revive() {
			thirdPerson.unlockTransform();
			firstPerson.unlockTransform();
		}

		public void first()
		{
			firstPerson.gameObject.SetActive(true);
			thirdPerson.gameObject.SetActive(false);

			freelookMode.ActiveCamera = firstPerson.gameObject;
			freelookMode.TargetObject = firstPerson.transform;

			firstPerson.unlockTransform();
		}

		public void third()
		{
			thirdPerson.gameObject.SetActive(true);
			firstPerson.gameObject.SetActive(false);

			freelookMode.ActiveCamera = thirdPerson.gameObject;
			freelookMode.TargetObject = playerHead.transform;

			thirdPerson.unlockTransform();
		}

		private GameObject getActiveCameraObject()
		{
			return firstPerson.gameObject.activeSelf ? firstPerson.gameObject : thirdPerson.gameObject;
		}
	}
}