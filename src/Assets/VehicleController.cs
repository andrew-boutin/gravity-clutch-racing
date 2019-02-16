using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Exposes functionality to control a vehicle.
[RequireComponent(typeof(GravityShiftable))]
public class VehicleController : MonoBehaviour {
	private GravityShiftable gravityShiftable;
	private Rigidbody rb;

	private bool brake;

	// Use this for initialization
	void Start () {
		gravityShiftable = GetComponent<GravityShiftable> ();
		rb = GetComponent<Rigidbody> ();
		brake = false;
	}

	void FixedUpdate() {
		if (brake) {
			rb.velocity = rb.velocity * 0.95f;
		} else {
			rb.AddForce(Vector3.forward * rb.mass * 20);
		}
	}

	public void MoveRight() {
		// TODO: Vector relative to the transform instead of fixed
//		rb.AddForce(Vector3.right * rb.mass * 10);
		rb.AddForce(transform.right * rb.mass * 10);
	}

	public void MoveLeft() {
		// TODO: Vector relative to the transform instead of fixed
//		rb.AddForce(Vector3.left * rb.mass * 10);
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
		// TODO: Will need to make this a slow rotation over time instead of instant.
		Quaternion targetQuaternion = gravityShiftable.GetCurrentQuaternion ();
		this.transform.rotation = targetQuaternion;
	}
}
