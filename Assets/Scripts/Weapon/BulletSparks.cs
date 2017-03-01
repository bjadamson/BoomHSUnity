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
}