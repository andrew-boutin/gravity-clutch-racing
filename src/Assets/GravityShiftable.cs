using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add this script to an object if you want to be able to change the direction of
// gravity for that object. There are four different directions of gravity: up,
// down, left, and right. Gravity is simulated through applying forces to a
// rigidbody.
[RequireComponent(typeof(Rigidbody))]
public class GravityShiftable : MonoBehaviour {
	// TODO: Notify when gravity shifts in case something is in charge of rotating the object etc.?
	// down Quaternion.Euler (0, 90, 0); up Quaternion.Euler (0, 90, 180);
	// left Quaternion.Euler (0, 90, -90); right Quaternion.Euler (0, 90, 90);

	private Rigidbody rb;

	public enum GravityDirection { Down, Up, Left, Right };

	public GravityDirection StartingGravityDirection = GravityDirection.Down;

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
		currentGravityDirection = StartingGravityDirection;
	}

	public void SetGravityDirection(GravityDirection targetDirection) {
		currentGravityDirection = targetDirection;
	}

	public void InvertGravityDirection() {
		currentGravityDirection = oppositeGravityDirections [currentGravityDirection];
	}

	public void ShiftGravityClockwise() {
		currentGravityDirection = clockwiseGravityDirections [currentGravityDirection];
	}

	public void ShiftGravityCounterClockwise() {
		currentGravityDirection = counterClockwiseGravityDirections [currentGravityDirection];
	}

	// Expose a method that can be invoked by a UnityEvent.
	public void OnRespawn() {
		ResetGravityDirection ();
	}
}
