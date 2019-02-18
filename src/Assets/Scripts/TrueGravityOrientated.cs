using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this to an object if you want it to remain orientated to true gravity. For
// example it can allow a UI element to always stay orientated towards original gravity
// even if the camera is rotating.
public class TrueGravityOrientated : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		Camera currentCamera = Camera.current;

		// Sometimes Camera.current is briefly null so avoid errors in the logs
		if (currentCamera) {
			Transform target = currentCamera.transform;

			// Inverse the z axis since the camera is rotating in the opposite direction
			// that we want.
			transform.eulerAngles = new Vector3 (0f, 0f, -target.eulerAngles.z);
		}
	}
}
