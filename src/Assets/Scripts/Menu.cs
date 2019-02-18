using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
	private bool paused;
	private float timeScale;

	[Tooltip("The object that is used as a pause menu.")]
	[SerializeField]
	private GameObject pausedMenuObject;

	// Use this for initialization
	void Start () {
		pausedMenuObject.SetActive (false);
		paused = false;
		timeScale = Time.timeScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("p")) {
			
			if (paused) {
				Time.timeScale = timeScale;
				pausedMenuObject.SetActive(false);
			} else {
				timeScale = Time.timeScale;
				Time.timeScale = 0f;
				pausedMenuObject.SetActive(true);
			}

			paused = !paused;
		}
	}
}
