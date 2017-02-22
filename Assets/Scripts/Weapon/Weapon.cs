using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player;

namespace weapon
{
	public class Weapon : MonoBehaviour
	{
		[SerializeField] public Camera Kamera;

		[SerializeField] public float BulletDistance = 10.0f;
		[SerializeField] public float BulletSpeed = 100.0f;
		[SerializeField] public uint MaximumAmmoCount = 30;

		public GameObject BulletShootAnchor;
		public GameObject PlayerBackWeaponSlot;
	
		public uint AmmoCount = 5;

		private AudioSource ShootSound;

		void Start() {
			GameObject go = (GameObject)Instantiate(Resources.Load("Weapons/Ak-47"), transform);
			Debug.Assert(go != null);
			go.transform.SetParent(transform);

			float scaleFactor = 0.34203f;
			go.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

			this.BulletShootAnchor = new GameObject("BulletShootAnchor");
			this.BulletShootAnchor.transform.SetParent(transform);
			this.BulletShootAnchor.transform.localPosition = new Vector3(0.008f, 0, 0.265f);
			this.BulletShootAnchor.transform.localRotation = Quaternion.identity;
			this.BulletShootAnchor.transform.localScale = Vector3.one;

			this.ShootSound = gameObject.AddComponent<AudioSource>();
			this.ShootSound.clip = Resources.Load<AudioClip>("audio/gunshoot");
		}

		public void shoot()
		{
			if (this.AmmoCount == 0)
			{
				return;
			}
			GameObject bulletGO = (GameObject)Instantiate(Resources.Load("Bullet"), BulletShootAnchor.transform.position, BulletShootAnchor.transform.rotation);
			Debug.Assert(bulletGO != null);

			var pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.BulletDistance);
			Vector3 clickedLocationWorldSpace = this.Kamera.ScreenToWorldPoint(pos);
			bulletGO.transform.LookAt(clickedLocationWorldSpace);

			Rigidbody rb = bulletGO.GetComponent<Rigidbody>();
			rb.AddForce(bulletGO.transform.forward * BulletSpeed);

			Debug.DrawRay(bulletGO.transform.position, bulletGO.transform.forward * 30, Color.yellow);
			this.ShootSound.Play();

			--this.AmmoCount;
		}

		public void reload() {
			this.AmmoCount = this.MaximumAmmoCount;
		}
	}
}