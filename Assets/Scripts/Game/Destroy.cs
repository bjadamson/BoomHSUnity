using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Destroy : MonoBehaviour {
	private ParticleSystem particleSystem;

	void Start() {
		particleSystem = Resources.Load<ParticleSystem>("Prefabs/Ultra Emissive Particles Shader/Prefabs/Sparks");
		Debug.Assert(particleSystem != null);
	}

	void OnTriggerEnter(Collider collider) {
		ParticleSystem bulletSparks = Instantiate(particleSystem, transform.position, transform.rotation);
		Destroy(bulletSparks.gameObject, 0.52f);
		Destroy(this.gameObject);
	}
}