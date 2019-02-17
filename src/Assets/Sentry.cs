using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add this to an object and it will patrol between the provided waypoints.
public class Sentry : MonoBehaviour {

	[Tooltip("The positions in space to patrol between.")]
	[SerializeField]
	private List<Vector3> positions;

	[Tooltip("The speed at which the object patrols.")]
	[SerializeField]
	private float speed;

	private bool patrol = true;

	private int curIndex = 0;

	private Vector3 targetPosition;

	void Start() {
		if (positions.Count <= 1) {
			patrol = false;
		}

		transform.position = positions [curIndex];
		targetPosition = positions [curIndex + 1];
	}

	void FixedUpdate() {
		if (!patrol) {
			return;
		}

		transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);

		if (transform.position == targetPosition) {
			curIndex++;
			if (curIndex >= positions.Count) {
				curIndex = 0;
			}
			targetPosition = positions [curIndex];
		}
	}
}
