using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player class: goes on top most parent of Player object, handles all player input
public class Player : MonoBehaviour {
	int HP = 3;
	float moveSpeed = 3f;

	void Start () {
		
	}

	void Update () {
		// Player input - LATER TO BE MAPPED TO CONTROLLER

		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (0, moveSpeed * Time.deltaTime, 0, Space.World);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (-moveSpeed * Time.deltaTime, 0, 0, Space.World);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (moveSpeed * Time.deltaTime, 0, 0, Space.World);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (0, -moveSpeed * Time.deltaTime, 0, Space.World);
		}
	}
}
