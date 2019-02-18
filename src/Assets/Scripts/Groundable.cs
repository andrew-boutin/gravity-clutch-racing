using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Groundable : MonoBehaviour {
	[Tooltip("Minimum distance from the ground to be considered grounded.")]
	[SerializeField]
	private float groundClearance = 1.0f;

	[Tooltip("Event that gets triggered when initially not grounded.")]
	[SerializeField]
	private UnityEvent onDeGrounding;

	[Tooltip("Event that gets triggered when initially grounded.")]
	[SerializeField]
	private UnityEvent onGrounding;

	private RaycastHit hit;

	private bool grounded = false;

	void Start() {
		grounded = CheckIfGrounded ();
		if (grounded) {
			onGrounding.Invoke();
		} else {
			onDeGrounding.Invoke();
		}
	}

	// Update is called once per frame
	void Update () {
		bool wasGrounded = grounded;
		grounded = CheckIfGrounded ();

		// Fire off the appropriate Unity event if we're changing grounded states either way.
		if (grounded && !wasGrounded) {
			onGrounding.Invoke();
		} else if(!grounded && wasGrounded) {
			onDeGrounding.Invoke();
		}
	}

	public bool CheckIfGrounded() {
		Vector3 direction = transform.TransformDirection (Vector3.down);

		// This will see if there's anything in space that the ray casts out and hits.
		if (Physics.Raycast(transform.position, direction, out hit)) {
			// We're only interested if the hit was close enough.
			if (hit.distance < groundClearance) {
				return true;
			}
		}

		return false;
	}
}
