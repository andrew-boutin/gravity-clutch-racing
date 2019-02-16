using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Provides AI behavior. For example this would decide when to
// turn, shift the gravity, etc.
[RequireComponent(typeof(Respawnable))]
[RequireComponent(typeof(VehicleController))]
public class AIController : MonoBehaviour {
	private Respawnable respawnable;
	private GravityShiftable gravityShiftable;
	private VehicleController vehicleController;

	private int counter = 0;

	// Use this for initialization
	void Start () {
		// TODO: Requires?
		respawnable = GetComponent<Respawnable> ();
		gravityShiftable = GetComponent<GravityShiftable> ();
		vehicleController = GetComponent<VehicleController> ();
	}
	
	// Update is called once per frame
	void Update () {
		// TODO: Implement some logic that would utilize the following
		// respawnable.Respawn ();
		// gravityShiftable.FlipGravityDirection ();
		// vehicleController.MoveRight ();
		if(counter >= 0 && counter < 25){
			vehicleController.MoveRight ();
		} else if(counter >= 25 && counter < 50){
			vehicleController.MoveLeft ();
		}

		// For testing out the AI controller.
		if (counter == 40) {
			vehicleController.ApplyBrake ();
		} else if (counter == 45) {
			vehicleController.ReleaseBrake ();
		}else if(counter >= 50){
			counter = 0;
		}

		counter++;
	}
}
