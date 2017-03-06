using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.PyroParticles;

namespace environment
{
	public class MeteorFactory : MonoBehaviour
	{
		private float TimeBetweenSpawns = 1.3f;
		private float InitialSpeed = 140.0f;
		private float MeteorSizeMultiplier = 10.0f;
		[SerializeField] private Transform PlayerTransform;
		[SerializeField] private GameObject DynamicRoot;

		private float timeToSpawnNextEnemy = 0.0f;

		void Start()
		{
			spawn();
		}

		void Update()
		{
			if (timeToSpawnNextEnemy > Time.time)
			{
				return;
			}
			spawn();
		}

		private void spawn()
		{
			GameObject GO = (GameObject)Instantiate(Resources.Load("Prefabs/Meteor"), DynamicRoot.transform);
			Debug.Assert(GO != null);
			GO.name = "meteor";

			GO.transform.localPosition = Vector3.zero;
			GO.transform.localRotation = Quaternion.identity;

			var scaleRng = Random.Range(1.0f, 10.0f);
			GO.transform.localScale = Vector3.one * MeteorSizeMultiplier * scaleRng;

			GO.transform.position = transform.position;
			GO.GetComponent<Meteor>().DynamicRoot = DynamicRoot;

			GO.GetComponent<SphereCollider>().radius = 0.5f;

			var rng = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * 10;
			GO.GetComponent<Rigidbody>().velocity = (transform.position - PlayerTransform.position + rng).normalized * -InitialSpeed;

			// always last
			resetSpawnTimer();
		}

		void resetSpawnTimer()
		{
			timeToSpawnNextEnemy = Time.time + TimeBetweenSpawns;
		}
	}

}