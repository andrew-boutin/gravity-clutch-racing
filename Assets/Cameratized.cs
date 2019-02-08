using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add this to an object if you want to be able to control the cameras
// on it.
public class Cameratized : MonoBehaviour {
	private Camera[] cameras;

	private int curCameraIdx = 0;

	void Start() {
		cameras = FindObjectsOfType<Camera>();

		if (cameras.Length > 0) {
			Camera mainCamera = Camera.main;
			for (int i = 0; i < cameras.Length; i++) {
				Camera curCamera = cameras [i];
				if (curCamera == mainCamera) {
					cameras [i] = cameras [0];
					cameras [0] = curCamera;
				} else {
					curCamera.enabled = false;
				}
			}
		}
	}

	public void NextCamera() {
		if(cameras.Length == 0) {
			Debug.Log("No cameras found!");
			return;
		} else if (cameras.Length == 1) {
			Debug.Log ("Only one camera found!");
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
		cameras [0].enabled = true;
		cameras [curCameraIdx].enabled = false;
		curCameraIdx = 0;
	}
}
