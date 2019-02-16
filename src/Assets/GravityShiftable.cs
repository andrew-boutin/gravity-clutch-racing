using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Add this script to an object if you want to be able to change the direction of
// gravity for that object. There are four different directions of gravity: up,
// down, left, and right. Gravity is simulated through applying forces to a
// rigidbody.
[RequireComponent(typeof(Rigidbody))]
public class GravityShiftable : MonoBehaviour {
	private Rigidbody rb;

	public enum GravityDirection { Down, Up, Left, Right };

	[SerializeField]
	private UnityEvent onGravityChange;

	[SerializeField]
	private GravityDirection StartingGravityDirection = GravityDirection.Down;

	private GravityDirection currentGravityDirection;

	private Dictionary<GravityDirection, GravityDirection> oppositeGravityDirections = new Dictionary<GravityDirection, GravityDirection>()
	{
		{ GravityDirection.Up, GravityDirection.Down },
		{ GravityDirection.Down, GravityDirection.Up },
		{ GravityDirection.Left, GravityDirection.Right },
		{ GravityDirection.Right, GravityDirection.Left }
	};

	private Dictionary<GravityDirection, GravityDirection> counterClockwiseGravityDirections = new Dictionary<GravityDirection, GravityDirection>()
	{
		{ GravityDirection.Up, GravityDirection.Left },
		{ GravityDirection.Down, GravityDirection.Right },
		{ GravityDirection.Left, GravityDirection.Down },
		{ GravityDirection.Right, GravityDirection.Up }
	};

	private Dictionary<GravityDirection, GravityDirection> clockwiseGravityDirections = new Dictionary<GravityDirection, GravityDirection>()
	{
		{ GravityDirection.Up, GravityDirection.Right },
		{ GravityDirection.Down, GravityDirection.Left },
		{ GravityDirection.Left, GravityDirection.Up },
		{ GravityDirection.Right, GravityDirection.Down }
	};

	private Dictionary<GravityDirection, Vector3> vectorFromDirection = new Dictionary<GravityDirection, Vector3>()
	{
		{ GravityDirection.Up, new Vector3 (0.0f, 9.81f, 0.0f) },
		{ GravityDirection.Down, new Vector3 (0.0f, -9.81f, 0.0f) },
		{ GravityDirection.Left, new Vector3 (9.81f, 0.0f, 0.0f) },
		{ GravityDirection.Right, new Vector3 (-9.81f, 0.0f, 0.0f) }
	};

	private Dictionary<GravityDirection, Quaternion> quaternionFromDirection = new Dictionary<GravityDirection, Quaternion>()
	{
		{ GravityDirection.Up, Quaternion.Euler (0, 0, 180) },
		{ GravityDirection.Down, Quaternion.Euler (0, 0, 0) },
		{ GravityDirection.Left, Quaternion.Euler (0, 0, 90) },
		{ GravityDirection.Right, Quaternion.Euler (0, 0, -90) }
	};

	// Use this for initialization
	void Start () {
		currentGravityDirection = StartingGravityDirection;

		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		Vector3 gravityVector = vectorFromDirection [currentGravityDirection];
		rb.AddForce(gravityVector * rb.mass);
	}
		
	public void ResetGravityDirection() {
		updateGravityDirection (StartingGravityDirection);
	}

	public void InvertGravityDirection() {
		updateGravityDirection (oppositeGravityDirections [currentGravityDirection]);
	}

	public void ShiftGravityClockwise() {
		updateGravityDirection (clockwiseGravityDirections [currentGravityDirection]);
	}

	public void ShiftGravityCounterClockwise() {
		updateGravityDirection (counterClockwiseGravityDirections [currentGravityDirection]);
	}

	// Change the current gravity direction to the desired one and invoke a Unity method
	// that others can subscribe to so they can have their own additional logic when
	// this changes.
	private void updateGravityDirection(GravityDirection targetGravityDirection) {
		currentGravityDirection = targetGravityDirection;
		onGravityChange.Invoke ();
	}

	// Expose a method that can be invoked by a UnityEvent to define gravity behavior on
	// respawn.
	public void OnRespawn() {
		ResetGravityDirection ();
	}

	// This will return the rotation that is oriented to the current direction of gravity.
	public Quaternion GetCurrentRotation() {
		return quaternionFromDirection[currentGravityDirection];
	}
}
