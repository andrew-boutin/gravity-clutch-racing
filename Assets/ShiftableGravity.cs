using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add this script to an object if you want to be able to change the direction of
// gravity for that object. There are four different directions of gravity, for
// now, that are available: up, down, left, and right.
public class ShiftableGravity : MonoBehaviour {
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

	// Use this for initialization
	void Start () {
		currentGravityDirection = StartingGravityDirection;
	}

	public void ResetGravityDirection() {
		currentGravityDirection = StartingGravityDirection;
	}

	public void SetGravityDirection(GravityDirection targetDirection) {
		currentGravityDirection = targetDirection;
	}

	public void FlipGravityDirection() {
		currentGravityDirection = oppositeGravityDirections [currentGravityDirection];
	}

	public void ShiftGravityClockwise() {
		currentGravityDirection = clockwiseGravityDirections [currentGravityDirection];
	}

	public void ShiftGravityCounterClockwise() {
		currentGravityDirection = counterClockwiseGravityDirections [currentGravityDirection];
	}
}
