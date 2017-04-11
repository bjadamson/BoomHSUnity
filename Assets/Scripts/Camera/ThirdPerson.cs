using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace camera
{
	public class ThirdPerson : MonoBehaviour
	{
        private static readonly float MAX_Y_ANGLE = 60.0f;

		[SerializeField] private UserIO userIO;
        [SerializeField] private CameraController kameraController;
        [SerializeField] public GameObject playerGO;
		[SerializeField] public Transform headTransform;
        [SerializeField] private float FollowDistance;
        [SerializeField] private Vector2 CenterOffset;

        private Freelook freelook;

		// state
		private float verticalRot = 0.0f;

		void Start()
		{
            kameraController = GetComponentInParent<CameraController>();
            Debug.Assert(kameraController != null);

            freelook = GetComponentInParent<Freelook>();
			Debug.Assert(freelook != null);
		}

		void LateUpdate()
		{
            // 1) Move camera to desired distance behind player.
            transform.position = headTransform.position - (headTransform.forward * FollowDistance);

            // 2) Update the vertical rotation state
            this.verticalRot = calculateVerticalRotationAngle(userIO.GetMouseAxis(), this.verticalRot);

            // 3) If our transform is locked, or we are in freelook mode, then no more modification should be made to the camera's transform, so just exit early.
            {
                bool dontUpdateTransform = kameraController.isTransformLocked() || freelook.IsFreelookModeActive();
                if (dontUpdateTransform)
                {
                    return;
                }
            }

            // 4) Look at the player.
            transform.LookAt(headTransform.position, transform.up);

            // 5) Move camera according to the CameraOffset
            transform.position += (transform.up * CenterOffset.y);
            transform.position += (transform.right * CenterOffset.x);

            // 6) Rotate the camera up/down
            transform.RotateAround(headTransform.position, headTransform.right, verticalRot);

            // 7) Set the rotation Z axix to zero. The camera shouldn't ever rotate around the Z axis, that doesn't yield a good user experience.
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
		}

        private static float calculateVerticalRotationAngle(Vector2 mouseAxis, float previousRotation)
        {
            // Calculate new rotation as the addition of the y-component of the mouseAxis and the previous rotation.
            float newRotation = previousRotation - mouseAxis.y;
            bool useNewRotation = Mathf.Abs(newRotation) <= MAX_Y_ANGLE;

            // We use the new rotation if it is less than the maximum allowed angle.
            return useNewRotation ? newRotation : previousRotation;
        }
    }
}