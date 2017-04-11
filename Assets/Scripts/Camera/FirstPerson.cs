using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace camera
{
	public class FirstPerson : MonoBehaviour
	{
		[SerializeField] private UserIO userIO;
        [SerializeField] private CameraController kameraController;
		[SerializeField] public GameObject playerGO;
		[SerializeField] public Transform headTransform;
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

        private void Update()
        {
            Vector2 mouseAxis = userIO.GetMouseAxis();
            verticalRot = mouseAxis.y;
        }

        void LateUpdate()
		{
			if (kameraController.isTransformLocked())
			{
				return;
			}

            transform.position = headTransform.position;

            if (!freelook.IsFreelookModeActive())
			{
				// 1) set camera's horizontal rotation to match the player's rotation.
				var newRot = transform.localEulerAngles;
				newRot.y = playerGO.transform.localEulerAngles.y;
				transform.localEulerAngles = newRot;

				// 2) rotate the camera up/down
				transform.RotateAround(transform.position, transform.right, -verticalRot);

                Vector3 a = transform.forward;
                Vector3 b = Vector3.forward;
                a.x = b.x;
                a.z = b.z;
                float yAngleDifference = Vector3.Angle(a, b);
                Debug.Log("angle: '" + yAngleDifference + "'");

                // 3) If we rotated up/down too far, rotate back.
                if (yAngleDifference > 45.0f)
                {
					// Rotated too far, rotate back
					transform.RotateAround(transform.position, transform.right, verticalRot);
				}

				// 4) The camera shouldn't ever rotate around the Z axis, that doesn't yield a good user experience.
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
			}
		}
    }
}