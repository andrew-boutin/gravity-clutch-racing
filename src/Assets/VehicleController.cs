using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Exposes functionality to control a vehicle.
[RequireComponent(typeof(GravityShiftable))]
public class VehicleController : MonoBehaviour {
	private GravityShiftable gravityShiftable;
	private Rigidbody rb;

	private bool brake;

	private bool rotate;

	private float rotationBreakPoint = 0.5f;

	private float rotationSpeed = 0.003f;

	private Quaternion targetQuaternion;

	// Use this for initialization
	void Start () {
		gravityShiftable = GetComponent<GravityShiftable> ();
		rb = GetComponent<Rigidbody> ();
		brake = false;
		rotate = false;
	}

	void FixedUpdate() {
		if (brake) {
			rb.velocity = rb.velocity * 0.95f;
		} else {
			rb.AddForce(Vector3.forward * rb.mass * 20);
		}

		if (rotate) {
			transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, Time.time * rotationSpeed);

			if (Mathf.Abs (transform.rotation.eulerAngles.z - targetQuaternion.eulerAngles.z) < rotationBreakPoint) {
				rotate = false;
				transform.rotation = targetQuaternion;
			}
		}
	}

	public void MoveRight() {
		rb.AddForce(transform.right * rb.mass * 10);
	}

	public void MoveLeft() {
		rb.AddForce((-transform.right) * rb.mass * 10);
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
		targetQuaternion = gravityShiftable.GetCurrentQuaternion ();
		rotate = true;
	}
}
