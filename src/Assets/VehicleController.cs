using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Exposes functionality to control a vehicle.
[RequireComponent(typeof(GravityShiftable))]
[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour {
	private GravityShiftable gravityShiftable;
	private Rigidbody rb;

	private bool brake = false;

	private bool rotate = false;

	private bool grounded = false;

	[Tooltip("Used to determine how fast the vehicle brakes.")]
	[SerializeField]
	private float BrakeDamper = 0.95f;

	[Tooltip("Used to determine when the vehicle's forward velocity should zero out when braking has almost stopped the vehicle.")]
	[SerializeField]
	private float BrakeBreakPoint = 0.5f;

	[Tooltip("Used for determining how fast the vehicle moves forward.")]
	[SerializeField]
	private float ForwardSpeedFactor = 20.0f;

	[Tooltip("Used for determining how fast the vehicle can turn left and right.")]
	[SerializeField]
	private float TurnSpeedFactor = 10.0f;

	[Tooltip("Affects how fast the vehicle rotates when gravity shifts.")]
	[SerializeField]
	private float RotationSpeed = 5.0f;

	private Quaternion targetRotation;

	// Use this for initialization
	void Start () {
		gravityShiftable = GetComponent<GravityShiftable> ();
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {
		// Handle rotating the vehicle.
		if (rotate) {
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed);

			if (transform.rotation == targetRotation) {
				rotate = false;
			}
		}

		// Bail out early if the vehicle isn't on the ground.
		if (!grounded) {
			return;
		}

		// We're either braking or defaulting to forward movement.
		if (brake) {
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z * BrakeDamper);

			// Remove all forward velocity if the current value is very close to 0 so we don't slowly
			// approach 0 forever.
			if (Mathf.Abs(rb.velocity.z) < BrakeBreakPoint) {
				rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
			}
		} else {
			rb.AddForce(Vector3.forward * rb.mass * ForwardSpeedFactor);
		}
	}

	public void MoveRight() {
		if (grounded) {
			rb.AddForce(transform.right * rb.mass * TurnSpeedFactor);
		}
	}

	public void MoveLeft() {
		if (grounded) {
			rb.AddForce ((-transform.right) * rb.mass * TurnSpeedFactor);
		}
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

	public void SetGrounded() {
		grounded = true;
	}

	public void SetDeGrounded() {
		grounded = false;
	}
}
