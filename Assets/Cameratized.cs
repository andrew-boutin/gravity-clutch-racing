using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add this to an object if you want to be able to control the cameras
// on it.
public class Cameratized : MonoBehaviour {
	// TODO: Track starting camera and start with that one enabled
	// TODO: Reset to starting camera
	private Camera[] cameras;

	private int curCameraIdx = 0;

	private void Awake() {
		cameras = FindObjectsOfType<Camera>();
	}

	public void NextCamera() {
		if(cameras.Length == 0) {
			Debug.Log("No cameras found!");
			return;
		} else if (cameras.Length == 1) {
			Debug.Log ("Only one camera found!");
			return;
		}

		cameras [curCameraIdx].enabled = false;
		curCameraIdx++;

		if(curCameraIdx >= cameras.Length){
			curCameraIdx = 0;
		}

		cameras [curCameraIdx].enabled = true;
	}
}
