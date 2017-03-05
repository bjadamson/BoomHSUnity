using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
	public float LifetimeIfNoCollisions = 3.0f;
	public bool PiercingRound = false;
	private float timeWhenToDestroy = 0.0f;

	private ParticleSystem particleSystem;
	private Light hitLight;

	void Start() {
		timeWhenToDestroy = Time.time + LifetimeIfNoCollisions;

		particleSystem = Resources.Load<ParticleSystem>("Prefabs/Effects/BulletHitSparks");
		Debug.Assert(particleSystem != null);

		hitLight = Resources.Load<Light>("Prefabs/Effects/BulletHitLight");
		Debug.Assert(hitLight != null);
	}

	void Update() {
		if (timeWhenToDestroy > Time.time)
		{
			return;
		}

		removeSelfFromScene();
	}
		
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log("hit '" + collision.collider.name + "'");

		if (collision.collider.tag == "Enemy")
		{
			//BulletSparks.SpawnSparks(gameObject, this.particleSystem);

			if (!PiercingRound)
			{
				removeSelfFromScene();
			}
		}
		else if (collision.collider.name == "Terrain" || collision.collider.tag == "Enemy")
		{
			// From inside OnCollisionStay or OnCollisionEnter you can always be sure that contacts has at least one element.
			// https://docs.unity3d.com/ScriptReference/Collision-contacts.html
			BulletSparks.SpawnHitLight(gameObject, this.hitLight, collision.contacts[0].point);
			removeSelfFromScene();
		}
	}

	private void removeSelfFromScene() {
		Destroy(gameObject);
	}
}