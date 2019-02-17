using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add this to an object if you want to be able to control the cameras
// on it.
public class Cameratized : MonoBehaviour {
	[Tooltip("Determines which camera is used to start with.")]
	[SerializeField]
	private Camera defaultCamera;

	private Camera[] cameras;

	private int curCameraIdx = 0;

	void Start() {
		cameras = FindObjectsOfType<Camera>();

		// Make defaultCamera the index 0 camera if set, default to the main camera if not
		if (cameras.Length > 1) {
			Camera startingCamera = (defaultCamera) ? defaultCamera : Camera.main;
			for (int i = 0; i < cameras.Length; i++) {
				Camera curCamera = cameras [i];
				if (curCamera == startingCamera) {
					cameras [i] = cameras [0];
					cameras [0] = curCamera;
					curCamera.enabled = true;
				} else {
					curCamera.enabled = false;
				}
			}
		}
	}

	public void NextCamera() {
		if(cameras.Length <= 1) {
			return;
		}

		int oldIdx = curCameraIdx;
		curCameraIdx++;

		if(curCameraIdx >= cameras.Length){
			curCameraIdx = 0;
		}

		cameras [curCameraIdx].enabled = true;
		cameras [oldIdx].enabled = false;
	}

	public void ResetCamera() {
		if(cameras.Length <= 1) {
			return;
		}

		cameras [0].enabled = true;
		cameras [curCameraIdx].enabled = false;
		curCameraIdx = 0;
	}
}
