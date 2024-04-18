using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
	public float speed;
	public int damage;
	public GameObject target;
	public Vector3 startPosition;
	public Vector3 targetPosition;
	private float distance;
	private float startTime;
	private Transform SpriteTransform;

	void Start() {
		startTime = Time.time;
		distance = Vector2.Distance(startPosition, targetPosition);
		SpriteTransform = gameObject.GetComponentInChildren<Transform>();
	}

	void Update() {
		if (target == null) {
			Destroy(gameObject);
			return;
		}
		targetPosition = target.transform.position;
		RotateIntoMoveDirection();
		float timeInterval = Time.time - startTime;
		gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);
		if (gameObject.transform.position.Equals(targetPosition)) {
			if (target != null) {
				HealthBar healthBar = target.GetComponentInChildren<HealthBar>();
				healthBar.currentHealth -= Mathf.Max(damage, 0);
				if (healthBar.currentHealth <= 0) {
					Destroy(target);
				}
			}
			Destroy(gameObject);
		}
	}

	void RotateIntoMoveDirection() {
		Vector3 newStartPosition = transform.position;
		Vector3 newEndPosition = targetPosition;
		Vector3 newDirection = (newEndPosition - newStartPosition);
		float x = newDirection.x;
		float y = newDirection.y;
		float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;
		SpriteTransform.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
	}
}
