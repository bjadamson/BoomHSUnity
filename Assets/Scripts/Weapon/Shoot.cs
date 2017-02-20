using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
	public class Shoot : MonoBehaviour
	{
		[SerializeField] private GameObject gun;
		[SerializeField] private UserIO userIO;
		[SerializeField] private Camera kam;

		void Update()
		{
			if (userIO.GetButtonDown("Fire1"))
			{
				shoot();
			}
			else
			{

				var ray = kam.ScreenPointToRay(Input.mousePosition);
				Vector3 point = ray.origin + ray.direction;

				Vector3 directionFromGunToPoint = (point - gun.transform.position).normalized;
				Debug.DrawRay(gun.transform.position, directionFromGunToPoint * 30, Color.yellow);
			}
		}

		void shoot()
		{
			GameObject bulletGO = (GameObject)Instantiate(Resources.Load("Sphere"));
			Debug.Assert(bulletGO != null);
			//bulletGO.transform.SetParent(gun.transform);
			bulletGO.transform.position = gun.transform.position;

			var ray = kam.ScreenPointToRay(Input.mousePosition);
			Vector3 point = ray.origin + ray.direction;

			Vector3 directionFromGunToPoint = (point - gun.transform.position).normalized;
			Debug.DrawRay(gun.transform.position, directionFromGunToPoint * 30, Color.magenta);

			Bullet bullet = bulletGO.AddComponent<Bullet>();
			bullet.Forward = directionFromGunToPoint;
			bulletGO.transform.Translate(bullet.Forward * 5); // move bullet away from player
		}
	}
}