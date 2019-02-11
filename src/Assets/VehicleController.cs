using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Exposes functionality to control a vehicle.
[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour {
	private Rigidbody rb;

	private bool brake;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		brake = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		if (brake) {
			rb.velocity = rb.velocity * 0.95f;
		} else {
			rb.AddForce(Vector3.forward * rb.mass * 20);
		}
	}

	public void MoveRight() {
		rb.AddForce(Vector3.right * rb.mass * 10);
	}

	public void MoveLeft() {
		rb.AddForce(Vector3.left * rb.mass * 10);
	}

	public void ApplyBrake() {
		brake = true;
	}

	public void ReleaseBrake() {
		brake = false;
	}
}
