using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
	private bool paused;
	private float timeScale;

	public GameObject menu;

	// Use this for initialization
	void Start () {
		menu.SetActive (false);
		paused = false;
		timeScale = Time.timeScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("p")) {
			
			if (paused) {
				Time.timeScale = timeScale;
				menu.SetActive(false);
			} else {
				timeScale = Time.timeScale;
				Time.timeScale = 0f;
				menu.SetActive(true);
			}

			paused = !paused;
		}
	}
}
