using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
	public class Shoot : MonoBehaviour
	{
		[SerializeField] private GameObject bulletStartAnchor;
		[SerializeField] private UserIO userIO;
		[SerializeField] private Camera kam;
		[SerializeField] private AudioSource shootSound;

		[SerializeField] private float distance = 10.0f;
		[SerializeField] public float BulletSpeed = 100.0f;

		void Update()
		{
			if (userIO.GetButtonDown("Fire1"))
			{
				shoot();
			}
		}

		void shoot()
		{
			GameObject bulletGO = (GameObject)Instantiate(Resources.Load("Bullet"), bulletStartAnchor.transform.position, bulletStartAnchor.transform.rotation);
			Debug.Assert(bulletGO != null);

			var pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
			Vector3 clickedLocationWorldSpace = kam.ScreenToWorldPoint(pos);
			bulletGO.transform.LookAt(clickedLocationWorldSpace);

			Rigidbody rb = bulletGO.GetComponent<Rigidbody>();
			rb.AddForce(bulletGO.transform.forward * BulletSpeed);

			Debug.DrawRay(bulletGO.transform.position, bulletGO.transform.forward * 30, Color.yellow);
			shootSound.Play();
		}
	}
}