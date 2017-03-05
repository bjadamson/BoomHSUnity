using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class BulletSparks
{
	public static void SpawnHitLight(GameObject gameObject, Light hitLight, Vector3 pos) {

		var transform = gameObject.transform;
		Light bulletHitLight = MonoBehaviour.Instantiate(hitLight, pos, Quaternion.identity);
		MonoBehaviour.Destroy(bulletHitLight.gameObject, 0.13f);
	}
}