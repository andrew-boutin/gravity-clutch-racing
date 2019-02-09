using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
	private Cameratized cameratized;
	private Respawnable respawnable;
	private GravityShiftable gravityShiftable;

	// Use this for initialization
	void Start () {
		cameratized = GetComponent<Cameratized> ();
		respawnable = GetComponent<Respawnable> ();
		gravityShiftable = GetComponent<GravityShiftable> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (cameratized) {
			if (Input.GetKeyDown ("c")) {
				cameratized.NextCamera ();
			}
		}
		if (respawnable) {
			if (Input.GetKeyDown ("r")) {
				respawnable.Respawn ();
			}
		}
		if (gravityShiftable) {
			if (Input.GetKeyDown ("g")) {
				gravityShiftable.ShiftGravityClockwise ();
			}
		}
	}
}
