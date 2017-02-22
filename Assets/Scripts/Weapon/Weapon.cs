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

		public string PrefabPath;
		public GameObject BulletShootAnchor;
		public GameObject PlayerBackWeaponSlot;
	
		public uint AmmoCount = 5;

		private AudioSource ShootSound;
		private AudioSource ClipEmptySound;
		private AudioSource ClipFull;
		private AudioSource ReloadSound;

		void Start() {
			GameObject go = (GameObject)Instantiate(Resources.Load(PrefabPath), transform);
			Debug.Assert(go != null);
			go.transform.SetParent(transform);

			float scaleFactor = 0.34203f;
			go.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

			this.BulletShootAnchor = new GameObject("BulletShootAnchor");
			this.BulletShootAnchor.transform.SetParent(transform);
			this.BulletShootAnchor.transform.localPosition = new Vector3(0.008f, 0, 0.265f);
			this.BulletShootAnchor.transform.localRotation = Quaternion.identity;
			this.BulletShootAnchor.transform.localScale = Vector3.one;

			this.ClipEmptySound = gameObject.AddComponent<AudioSource>();
			this.ClipEmptySound.clip = Resources.Load<AudioClip>("audio/clip_empty");

			this.ClipFull = gameObject.AddComponent<AudioSource>();
			this.ClipFull.clip = Resources.Load<AudioClip>("audio/clip_full");

			this.ShootSound = gameObject.AddComponent<AudioSource>();
			this.ShootSound.clip = Resources.Load<AudioClip>("audio/gunshoot");

			this.ReloadSound = gameObject.AddComponent<AudioSource>();
			this.ReloadSound.clip = Resources.Load<AudioClip>("audio/reload");
		}

		public void shoot()
		{
			if (this.AmmoCount == 0)
			{
				this.ClipEmptySound.Play();
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

		public void playClipFullSound() {
			this.ClipFull.Play();
		}

		public void playReloadAnimation() {
			this.ReloadSound.Play();
		}

		public void reloadAmmo() {
			this.AmmoCount = this.MaximumAmmoCount;
		}

		public bool isClipFull() {
			return this.AmmoCount == this.MaximumAmmoCount;
		}

		public void stopAnimations() {
			this.ClipEmptySound.Stop();
			this.ClipFull.Stop();
			this.ReloadSound.Stop();
			this.ShootSound.Stop();
		}
	}
}