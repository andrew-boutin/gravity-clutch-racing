using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A controller exposed directly to the player through
// things like key inputs.
[RequireComponent(typeof(Cameratized))]
[RequireComponent(typeof(Respawnable))]
[RequireComponent(typeof(VehicleController))]
public class PlayerController : MonoBehaviour {
	private Cameratized cameratized;
	private Respawnable respawnable;
	private VehicleController vehicleController;

	// Use this for initialization
	void Start () {
		cameratized = GetComponent<Cameratized> ();
		respawnable = GetComponent<Respawnable> ();
		vehicleController = GetComponent<VehicleController> ();
	}
	
	// Update is called once per frame
	void Update () {
		// TODO: Should all of these be defined in InputManger in editor?
		if (Input.GetKeyDown ("c")) {
			cameratized.NextCamera ();
		}
		if (Input.GetKeyDown ("r")) {
			respawnable.Respawn ();
		}

		// Gravity controls
		if (Input.GetKeyDown ("up")) {
			vehicleController.InvertGravity ();
		} else if (Input.GetKeyDown ("right")) {
			vehicleController.ShiftGravityClockwise ();
		} else if (Input.GetKeyDown ("left")) {
			vehicleController.ShiftGravityCounterClockwise ();
		}

		// Vehicle movement
		// TODO: Potentially use an axis input instead for a/d
		if (Input.GetKey ("a")) {
			vehicleController.MoveLeft ();
		} else if(Input.GetKey("d")) {
			vehicleController.MoveRight ();
		}
			
		if(Input.GetKeyDown("s")) {
			vehicleController.ApplyBrake ();
		} else if (Input.GetKeyUp ("s")) {
			vehicleController.ReleaseBrake ();
		}
	}
}
