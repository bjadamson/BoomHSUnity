using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : MonoBehaviour {
	private Animator anim;

	[SerializeField]
	private Transform playerTransform;

	[SerializeField]
	private float followSpeed = 3.0f;

	[SerializeField]
	private float followDistance = 10.0f;

	[SerializeField]
	private float attackDistance = 5.0f;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Update () {
		float distance = Vector3.Distance (transform.position, playerTransform.position);

		if (distance < followDistance) {
			activeState (distance);
		} else {
			idleState ();
		}
	}

	private void activeState(float distance) {
		Vector3 delta = playerTransform.position - transform.position;
		delta.y = 0; // We won't want to fall over

		rotateTowardsPlayer (delta);
		anim.SetBool ("isIdle", false);
		if (distance > attackDistance) {
			moveTowardsPlayer (delta.normalized);
			anim.SetBool ("isWalking", true);
			anim.SetBool ("isAttacking", false);
		} else {
			anim.SetBool ("isAttacking", true);
			anim.SetBool ("isWalking", false);
		}
	}

	private void idleState() {
		anim.SetBool ("isIdle", true);
		anim.SetBool ("isWalking", false);
		anim.SetBool ("isAttacking", false);
	}

	private void rotateTowardsPlayer(Vector3 delta) {
		Quaternion lookRotation = Quaternion.LookRotation (delta, Vector3.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, 0.1f);
	}

	private void moveTowardsPlayer(Vector3 direction) {
		transform.position = Vector3.MoveTowards (transform.position, playerTransform.position, followSpeed * Time.deltaTime);
	}
}