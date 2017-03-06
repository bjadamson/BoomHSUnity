using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace environment
{
	public class Meteor : MonoBehaviour
	{
		public GameObject DynamicRoot;

		private AudioSource audioSource;
		private MeshRenderer meshRenderer;
		private TrailRenderer trailRenderer;

		// state
		private bool collided = false;

		void Start()
		{
			audioSource = GetComponent<AudioSource>();
			meshRenderer = GetComponent<MeshRenderer>();
			trailRenderer = GetComponent<TrailRenderer>();
		}

		void OnCollisionEnter(Collision collision)
		{
			if (collision.collider.name == "meteor")
			{
				Debug.Log("Hit a meteor");
			}
			if (collision.collider.name == "meteor")
			{
				return;
			}

			meshRenderer.enabled = false;
			trailRenderer.enabled = false;

			MonoBehaviour.Destroy(gameObject, 3.25f);
			audioSource.Play();

			spawnFlames(collision);
			spawnMiniMeteors();
		}

		private void spawnMiniMeteors()
		{
			for (int i = 0; i < 3; ++i)
			{
				GameObject GO = (GameObject)Instantiate(Resources.Load("Prefabs/MeteorFragment"), DynamicRoot.transform);
				Debug.Assert(GO != null);
				GO.name = "meteor"; // MeteorFragment??

				GO.transform.localPosition = Vector3.zero;
				GO.transform.localRotation = Quaternion.identity;
				GO.transform.localScale = Vector3.one;

				// Spawn on what we hit
				GO.transform.position = transform.position;

				GO.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
			}
		}

		private void spawnFlames(Collision c)
		{
			GameObject GO = (GameObject)Instantiate(Resources.Load("Prefabs/MeteorImpactAreaFlame"), DynamicRoot.transform);
			Debug.Assert(GO != null);

			GO.transform.localPosition = Vector3.zero;
			GO.transform.localRotation = Quaternion.identity;
			GO.transform.localScale = Vector3.one;

			// Spawn where we hit
			GO.transform.position = c.contacts[0].point;
			Destroy(GO, 5.0f);
		}
	}
}