using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add this to an object if you want to easily give it constant rotation in any
// direction. Set the rotation deltas on each axis in the editor or through the
// exposed functions.
public class FixedRotater : MonoBehaviour {
	[Tooltip("Fixed rotation step on the X axis.")]
	[SerializeField]
	private float xRotationStep = 0.0f;

	[Tooltip("Fixed rotation step on the Y axis.")]
	[SerializeField]
	private float yRotationStep = 0.0f;

	[Tooltip("Fixed rotation step on the Z axis.")]
	[SerializeField]
	private float zRotationStep = 0.0f;

	void FixedUpdate() {
		transform.Rotate (new Vector3 (xRotationStep, yRotationStep, zRotationStep), Space.Self);
	}

	public void SetXRotationStep(float targetXRotationStep) {
		xRotationStep = targetXRotationStep;
	}

	public void SetYRotationStep(float targetYRotationStep) {
		yRotationStep = targetYRotationStep;
	}

	public void SetZRotationStep(float targetZRotationStep) {
		zRotationStep = targetZRotationStep;
	}

	public void SetTargetRotation(Vector3 targetRotation) {
		xRotationStep = targetRotation.x;
		yRotationStep = targetRotation.y;
		zRotationStep = targetRotation.z;
	}
}
