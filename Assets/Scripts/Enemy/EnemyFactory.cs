using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ui;

namespace enemy
{
	public class EnemyFactory : MonoBehaviour
	{
		[SerializeField] private float TimeBetweenSpawns = 0.8f;
		[SerializeField] private Transform PlayerTransform;
		[SerializeField] private UIManager uiManager;
		[SerializeField] private GameObject DynamicRoot;

		private float timeToSpawnNextEnemy = 0.0f;

		void Start()
		{
			resetSpawnTimer();
		}

		void Update()
		{
			if (timeToSpawnNextEnemy > Time.time)
			{
				return;
			}

			spawnSkeleton();
		}

		private void spawnSkeleton()
		{
			GameObject skeletonGO = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/Skeleton"), DynamicRoot.transform);
			Debug.Assert(skeletonGO != null);

			skeletonGO.transform.localPosition = Vector3.zero;
			skeletonGO.transform.localRotation = Quaternion.identity;
			skeletonGO.transform.localScale = Vector3.one * 10;

			skeletonGO.tag = "Enemy";
			skeletonGO.transform.position = transform.position;
			skeletonGO.GetComponent<Skeleton>().PlayerTransform = PlayerTransform;
			skeletonGO.GetComponent<Skeleton>().uiManager = uiManager;

			// always last
			resetSpawnTimer();
		}

		void resetSpawnTimer()
		{
			timeToSpawnNextEnemy = Time.time + TimeBetweenSpawns;
		}
	}
}