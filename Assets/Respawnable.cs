using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add this to an object if you want it to have the ability to be respawned.
public class Respawnable : MonoBehaviour {
	// TODO: Could also reset gravity on respawn if ShiftableGravity present.
	// TODO: If using a rigidbody would also have to `rb.velocity = Vector3.zero;`.

	private Vector3 spawnLocation;
	private Quaternion spawnRotation;

	// Use this for initialization
	void Start () {
		spawnLocation = this.transform.position;
		spawnRotation = this.transform.rotation;
	}

	public void Respawn() {
		this.transform.position = spawnLocation;
		this.transform.rotation = spawnRotation;
	}

	public void UpdateRespawn(Vector3 targetSpawnLocation, Quaternion targetSpawnRotation) {
		spawnLocation = targetSpawnLocation;
		spawnRotation = targetSpawnRotation;
	}
}
