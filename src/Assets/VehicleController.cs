using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Exposes functionality to control a vehicle.
[RequireComponent(typeof(GravityShiftable))]
[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour {
	private GravityShiftable gravityShiftable;
	private Rigidbody rb;

	private bool brake;

	private bool rotate;

	[SerializeField]
	private float BrakeDamper = 0.95f;

	[SerializeField]
	private float BrakeBreakPoint = 0.5f;

	[SerializeField]
	private float ForwardSpeedFactor = 20.0f;

	[SerializeField]
	private float TurnSpeedFactor = 10.0f;

	[SerializeField]
	private float RotationBreakPoint = 0.5f;

	[SerializeField]
	private float RotationSpeed = 0.003f;

	private Quaternion targetRotation;

	// Use this for initialization
	void Start () {
		gravityShiftable = GetComponent<GravityShiftable> ();
		rb = GetComponent<Rigidbody> ();
		brake = false;
		rotate = false;
	}

	void FixedUpdate() {
		if (brake) {
			rb.velocity = rb.velocity * BrakeDamper;

			if (Mathf.Abs(rb.velocity.z) < BrakeBreakPoint) {
				rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
			}
		} else {
			// TODO: Only do this if "grounded".
			rb.AddForce(Vector3.forward * rb.mass * ForwardSpeedFactor);
		}

		if (rotate) {
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.time * RotationSpeed);

			if (Mathf.Abs (transform.rotation.eulerAngles.z - targetRotation.eulerAngles.z) < RotationBreakPoint) {
				rotate = false;
				transform.rotation = targetRotation;
			}
		}
	}

	public void MoveRight() {
		rb.AddForce(transform.right * rb.mass * TurnSpeedFactor);
	}

	public void MoveLeft() {
		rb.AddForce((-transform.right) * rb.mass * TurnSpeedFactor);
	}

	public void ApplyBrake() {
		brake = true;
	}

	public void ReleaseBrake() {
		brake = false;
	}

	// This is a method that can be hooked up to an event that gets triggered when
	// the gravity direction is changed. This will rotate the vehicle so the wheels
	// are orientated with the new direction of gravity.
	public void RotateWithGravity() {
		targetRotation = gravityShiftable.GetCurrentRotation ();
		rotate = true;
	}

	// TODO: Expose functions to change gravity direction. Call gravityShiftable methods.
	// Then change rotation here instead of in RotateWithGravity. Can potentially remove
	// gravityShiftable from PlayerController and AIController.
}
