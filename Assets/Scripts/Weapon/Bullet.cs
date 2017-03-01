using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
	public float LifetimeIfNoCollisions = 3.0f;
	private float timeWhenToDestroy = 0.0f;

	private ParticleSystem particleSystem;

	void Start() {
		timeWhenToDestroy = Time.time + LifetimeIfNoCollisions;

		particleSystem = Resources.Load<ParticleSystem>("Prefabs/Effects/BulletHitSparks");
		Debug.Assert(particleSystem != null);
	}

	void Update() {
		if (timeWhenToDestroy > Time.time)
		{
			return;
		}

		removeSelfFromScene();
	}
		
	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Enemy")
		{
			BulletSparks.SpawnSparks(gameObject, this.particleSystem);
		}
	}

	private void removeSelfFromScene() {
		Destroy(gameObject);
	}
}