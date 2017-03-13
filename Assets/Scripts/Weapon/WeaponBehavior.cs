using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using player;

namespace weapon
{
	public class WeaponBehavior : MonoBehaviour
	{
		public string PrefabPath;
		public Sprite Icon;
		[SerializeField] private AudioSource ShootSound;
		[SerializeField] private AudioSource ClipEmptySound;
		[SerializeField] private AudioSource ClipFull;
		[SerializeField] private AudioSource ReloadSound;
		[SerializeField] private GameObject BulletShootAnchor;
		[SerializeField] private float ScaleFactor;

		void Start()
		{
			if (PrefabPath == null)
			{
				// Item doesn't have a model
				return;
			}
			GameObject go = (GameObject)Instantiate(Resources.Load(PrefabPath), transform);
			Debug.Assert(go != null);
			go.transform.SetParent(transform);
			go.transform.localScale = new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);
		}

		public void shoot(float bulletDistance, float bulletSpeed, bool piercingRound)
		{
			Debug.Assert(PrefabPath != null);
			GameObject bulletGO = (GameObject)Instantiate(Resources.Load("Prefabs/Bullet"), BulletShootAnchor.transform.position, BulletShootAnchor.transform.rotation);
			Debug.Assert(bulletGO != null);

			var mousePos = Input.mousePosition;
			mousePos.z = bulletDistance;
			Vector3 clickedLocationWorldSpace = Camera.main.ScreenToWorldPoint(mousePos);
			bulletGO.transform.LookAt(clickedLocationWorldSpace);

			Rigidbody rb = bulletGO.GetComponent<Rigidbody>();
			rb.AddForce(bulletGO.transform.forward * bulletSpeed, ForceMode.Impulse);

			bulletGO.GetComponent<Bullet>().PiercingRound = piercingRound;

			Debug.DrawRay(bulletGO.transform.position, bulletGO.transform.forward * 30, Color.green);
			this.ShootSound.Play();
		}

		public void playClipEmptySound()
		{
			this.ClipEmptySound.Play();
		}

		public void playClipFullSound()
		{
			this.ClipFull.Play();
		}

		public void playReloadSound()
		{
			this.ReloadSound.Play();
		}

		public void stopAnimations()
		{
			this.ClipEmptySound.Stop();
			this.ClipFull.Stop();
			this.ReloadSound.Stop();
			this.ShootSound.Stop();
		}

		//public void hideFromFpsCamera()
		//{
		//	gameObject.layer = 8; // player layer
		//}

		//public void showToFpsCamera()
		//{
		//	gameObject.layer = 0;
		//}
	}
}