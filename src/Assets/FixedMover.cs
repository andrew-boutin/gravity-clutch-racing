using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add this to an object if you want to easily give it constant movement in any
// direction. Set the movement deltas on each axis in the editor or through the
// exposed functions.
public class FixedMover : MonoBehaviour {
	[Tooltip("Fixed movement step on the X axis.")]
	[SerializeField]
	private float xMovementStep = 0.0f;

	[Tooltip("Fixed movement step on the Y axis.")]
	[SerializeField]
	private float yMovementStep = 0.0f;

	[Tooltip("Fixed movement step on the Z axis.")]
	[SerializeField]
	private float zMovementStep = 0.0f;

	void FixedUpdate() {
		transform.position += new Vector3(xMovementStep, yMovementStep, zMovementStep);
	}

	public void SetXMovementStep(float targetXMovementStep) {
		xMovementStep = targetXMovementStep;
	}

	public void SetYMovementStep(float targetYMovementStep) {
		yMovementStep = targetYMovementStep;
	}

	public void SetZMovementStep(float targetZMovementStep) {
		zMovementStep = targetZMovementStep;
	}

	public void SetTargetMovement(Vector3 targetMovement) {
		xMovementStep = targetMovement.x;
		yMovementStep = targetMovement.y;
		zMovementStep = targetMovement.z;
	}
}
