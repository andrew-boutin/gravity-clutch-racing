using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Add this to an object if you want it to have the ability to be respawned.
public class Respawnable : MonoBehaviour {
	private Vector3 spawnLocation;
	private Quaternion spawnRotation;
	private Rigidbody rb;
	private GravityShiftable gravityShiftable;

	[SerializeField]
	private UnityEvent onRespawnEvent;

	void Start () {
		spawnLocation = this.transform.position;
		spawnRotation = this.transform.rotation;
		rb = GetComponent<Rigidbody> ();
		gravityShiftable = GetComponent<GravityShiftable> ();
	}

	public void Respawn() {
		this.transform.position = spawnLocation;
		this.transform.rotation = spawnRotation;

		if (rb) {
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}

		// Let anything registered as a listener know
		onRespawnEvent.Invoke();
	}

	public void UpdateRespawn(Vector3 targetSpawnLocation, Quaternion targetSpawnRotation) {
		spawnLocation = targetSpawnLocation;
		spawnRotation = targetSpawnRotation;
	}
}
