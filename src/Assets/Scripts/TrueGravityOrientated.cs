using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this to an object if you want it to remain orientated to true gravity. For
// example it can allow a UI element to always stay orientated towards original gravity
// even if the player is changing their direction of gravity (along with the camera's
// orientation).
public class TrueGravityOrientated : MonoBehaviour {
	// TODO: Currently the gravity arrow is updated based on the vehicle's rotation
	// which is checked every frame. The arrow updates as the vehicle rotates. This
	// wouldn't actually point to the current direction of gravity if the vehicle
	// didn't rotate with gravity. Also, the direction of gravity updates immediately
	// which isn't reflected by the arrow slowly rotating. Could utilize the onGravityShift
	// event to not have to poll for the change and update the arrow instantly. Can get
	// rotation info from the GravityShiftable script.
	private bool gravityShifting = false;

	// The transform that is rotating to new directions used to determine the offset needed
	// to rotate back towards original gravity.
	public Transform target;

	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(0f, 0f, -target.eulerAngles.z);
	}
}
