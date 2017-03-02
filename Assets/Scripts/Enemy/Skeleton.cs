using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo: move
using ui;

namespace enemy
{
	public class Skeleton : MonoBehaviour
	{
		private float followSpeed = 3.0f;
		private float followDistance = 10.0f;
		private float attackDistance = 5.0f;
		private float stayOnGroundAfterDeath = 10.0f;

		public int Health = 100;
		private bool isDeadOnGround = false;

		private Animator anim;
		private AudioSource audioSource;

		public CrosshairControl CrosshairControl;
		public Transform PlayerTransform;

		void Start()
		{
			anim = GetComponent<Animator>();
			audioSource = GetComponent<AudioSource>();
		}

		void Update()
		{
			if (isDead())
			{
				return;
			}
			float distance = Vector3.Distance(transform.position, PlayerTransform.position);

			if (distance < followDistance)
			{
				activeState(distance);
			}
			else
			{
				idleState();
			}
		}

		public void OnTriggerEnter(Collider collider)
		{
			if (isDead())
			{
				return;
			}

			if (collider.tag == "Bullet")
			{
				audioSource.Play();
				Damage(25);

				CrosshairControl.showThenHideHitIndicator();
			}
		}

		public void Damage(int amount)
		{
			this.Health -= amount;

			if (this.Health <= 0 && !isDeadOnGround)
			{
				kill();
			}
		}

		private void activeState(float distance)
		{
			Vector3 delta = PlayerTransform.position - transform.position;
			delta.y = 0; // We won't want to fall over

			rotateTowardsPlayer(delta);
			anim.SetBool("isIdle", false);
			if (distance > attackDistance)
			{
				moveTowardsPlayer(delta.normalized);
				anim.SetBool("isWalking", true);
				anim.SetBool("isAttacking", false);
			}
			else
			{
				anim.SetBool("isAttacking", true);
				anim.SetBool("isWalking", false);
			}
		}

		private void idleState()
		{
			anim.SetBool("isIdle", true);
			anim.SetBool("isWalking", false);
			anim.SetBool("isAttacking", false);
		}

		private void rotateTowardsPlayer(Vector3 delta)
		{
			Quaternion lookRotation = Quaternion.LookRotation(delta, Vector3.up);
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f);
		}

		private void moveTowardsPlayer(Vector3 direction)
		{
			transform.position = Vector3.MoveTowards(transform.position, PlayerTransform.position, followSpeed * Time.deltaTime);
		}

		private bool isDead()
		{
			return this.Health <= 0;
		}

		private void kill()
		{
			isDeadOnGround = true;

			GetComponent<Rigidbody>().isKinematic = true;
			anim.SetTrigger("Death");
			StartCoroutine(cleanup());
		}

		private IEnumerator cleanup()
		{
			yield return new WaitForSeconds(stayOnGroundAfterDeath);
			Destroy(gameObject);
		}
	}
}