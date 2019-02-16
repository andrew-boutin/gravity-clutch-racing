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
	private float RotationSpeed = 5.0f;

	private Quaternion targetRotation;

	// Use this for initialization
	void Start () {
		gravityShiftable = GetComponent<GravityShiftable> ();
		rb = GetComponent<Rigidbody> ();
		brake = false;
		rotate = false;
	}

	void FixedUpdate() {
		// We're either braking or defaulting to forward movement.
		if (brake) {
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z * BrakeDamper);

			// Remove all forward velocity if the current value is very close to 0 so we don't slowly
			// approach 0 forever.
			if (Mathf.Abs(rb.velocity.z) < BrakeBreakPoint) {
				rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
			}
		} else {
			// TODO: Only do this if "grounded".
			rb.AddForce(Vector3.forward * rb.mass * ForwardSpeedFactor);
		}

		// Handle rotating the vehicle.
		if (rotate) {
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed);

			if (transform.rotation == targetRotation) {
				rotate = false;
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

	// This will rotate the vehicle so the wheels are orientated with the direction of gravity.
	private void rotateWithGravity() {
		targetRotation = gravityShiftable.GetCurrentRotation ();
		rotate = true;
	}

	// Changing gravity is considered part of the vehicle's functionality since each
	// vehicle does this independently.
	public void InvertGravity() {
		gravityShiftable.InvertGravityDirection ();
		rotateWithGravity ();
	}

	public void ShiftGravityClockwise() {
		gravityShiftable.ShiftGravityClockwise ();
		rotateWithGravity ();
	}

	public void ShiftGravityCounterClockwise() {
		gravityShiftable.ShiftGravityCounterClockwise ();
		rotateWithGravity ();
	}
}
