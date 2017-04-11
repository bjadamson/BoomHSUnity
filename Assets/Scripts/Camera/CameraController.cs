using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace camera
{
	public class CameraController : MonoBehaviour
	{
        private static readonly float PLAYER_TURN_SMOOTHNESS = 0.6f;
        [SerializeField] private UserIO userIO;
		[SerializeField] private ThirdPerson thirdPerson;
		[SerializeField] private FirstPerson firstPerson;

		[SerializeField] private GameObject playerGO;
		[SerializeField] private GameObject playerHead;
        [SerializeField] private float AdsZoomFovDelta;
        private Freelook freelook;

        // state
        private bool lockTransform_ = false;

        void Start()
		{
			freelook = GetComponent<Freelook>();

			third();
			playerGO.transform.localRotation = Quaternion.identity;
			thirdPerson.transform.localRotation = Quaternion.identity;
		}

		void Update()
		{
			if (!freelook.IsFreelookModeActive())
			{
				if (userIO.GetKeyDown(KeyCode.T))
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
				this.freelook.enabled = true;
				this.freelook.PreviousCameraRotation = getActiveCameraObject().transform.localRotation;
			}
			else if (Input.GetKeyUp(KeyCode.Mouse3))
			{
				this.freelook.enabled = false;
			}

            if (lockTransform_)
            {
                return;
            }
            
            if (!freelook.IsFreelookModeActive())
            {
                // rotate around local axis
                Vector2 mouseAxis = userIO.GetMouseAxis();
                playerGO.transform.RotateAround(playerGO.transform.position, playerGO.transform.up, mouseAxis.x * PLAYER_TURN_SMOOTHNESS);
            }
        }

		public bool isFreelookMode() {
			return freelook.IsFreelookModeActive();
		}

		public bool isFirstPerson() {
			Debug.Assert(firstPerson.gameObject.activeSelf != thirdPerson.gameObject.activeSelf);
			return firstPerson.gameObject.activeSelf;
		}

		public void death() {
			lockTransform();
		}

		public void revive() {
			unlockTransform();
		}

		public void first()
		{
			firstPerson.gameObject.SetActive(true);
			thirdPerson.gameObject.SetActive(false);

			freelook.ActiveCamera = firstPerson.gameObject;
			freelook.TargetObject = firstPerson.transform;

			unlockTransform();
		}

		public void third()
		{
			thirdPerson.gameObject.SetActive(true);
			firstPerson.gameObject.SetActive(false);

			freelook.ActiveCamera = thirdPerson.gameObject;
			freelook.TargetObject = playerHead.transform;

            unlockTransform();
		}

        public void adsZoomIn()
		{
            Camera.main.fieldOfView -= AdsZoomFovDelta;
        }

		public void adsZoomOut()
		{
            Camera.main.fieldOfView += AdsZoomFovDelta;
        }

        public bool isTransformLocked()
        {
            return true == lockTransform_;
        }

        private void lockTransform()
        {
            lockTransform_ = true;
        }

        private void unlockTransform()
        {
            lockTransform_ = false;
        }

        private GameObject getActiveCameraObject()
		{
			return firstPerson.gameObject.activeSelf ? firstPerson.gameObject : thirdPerson.gameObject;
		}
	}
}