using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles movement of aiming directional indicator object in player
public class DirectionIndicator : MonoBehaviour {

	void Start () {}

	void Update () {
		float x = Input.GetAxisRaw ("RHorizontal") * 0.3f;
		float y = Input.GetAxisRaw ("RVertical") * 0.3f;

		transform.localPosition = new Vector3(x, y, transform.localPosition.z);
	}
}
