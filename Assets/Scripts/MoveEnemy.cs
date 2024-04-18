using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : GameManagerBehavior {
	[HideInInspector]
	public GameObject[] waypoints;
	private int currentWaypoint = 0;
	private float lastWaypointSwitchTime;
	public float speed = 1.0f;
	public Transform SpriteTransform;

	void Start() {
		lastWaypointSwitchTime = Time.time;
		SpriteTransform = GetComponentInChildren<SpriteRenderer>().transform;
	}

	void Update() {
		Vector3 startPosition = waypoints[currentWaypoint].transform.position;
		Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;

		float pathLength = Vector3.Distance(startPosition, endPosition);
		float totalTimeForPath = pathLength / speed;
		float currentTimeOnPath = Time.time - lastWaypointSwitchTime;

		gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

		if (gameObject.transform.position.x.Equals(endPosition.x) && gameObject.transform.position.y.Equals(endPosition.y)) {
			if (currentWaypoint < waypoints.Length - 2) {
				currentWaypoint++;
				lastWaypointSwitchTime = Time.time;
				RotateIntoMoveDirection();
			}
			else {
				Destroy(gameObject);
			}
		}
	}

	void RotateIntoMoveDirection() {
		Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;
		Vector3 newEndPosition = waypoints[currentWaypoint + 1].transform.position;
		Vector3 newDirection = (newEndPosition - newStartPosition);
		float x = newDirection.x;
		float y = newDirection.y;
		float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;
		SpriteTransform.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
	}
}
