using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class BulletSparks
{
	public static void SpawnSparks(GameObject gameObject, ParticleSystem particleSystem) {

		var transform = gameObject.transform;
		ParticleSystem bulletSparks = MonoBehaviour.Instantiate(particleSystem, transform.position, transform.rotation);
		MonoBehaviour.Destroy(bulletSparks.gameObject, 0.52f);
	}

	public static void SpawnHitLight(GameObject gameObject, Light hitLight, Vector3 pos) {

		var transform = gameObject.transform;
		Light bulletHitLight = MonoBehaviour.Instantiate(hitLight, pos, Quaternion.identity);
		MonoBehaviour.Destroy(bulletHitLight.gameObject, 0.13f);
	}
}