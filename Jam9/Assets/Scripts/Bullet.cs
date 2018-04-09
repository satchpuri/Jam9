using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for Player bullets: handles bullet movement and rendering
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : MonoBehaviour {
	[SerializeField]
	private float speed = 10f;
	private GameObject sprite;

	void Start () {
		sprite = this.gameObject.transform.GetChild (0).gameObject;
	}
		
	void Update () {
		// Always move in the foward direction
		transform.Translate (speed * Vector3.forward * Time.deltaTime);
		transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);

		// "lock" Y-rotation of sprite child
		sprite.transform.localRotation = Quaternion.Euler(new Vector3 (transform.rotation.z, transform.rotation.x, 0f));

	}

	// Despawn off camera
	void OnBecameInvisible() {
		Destroy (gameObject);
	}

	// Destroy when hits something else
	void OnCollisionEnter (Collision other) {
		// does not destroy if it is a pick-up item
		if (other.gameObject.tag != "Key" ||
			other.gameObject.tag != "Pickup"
		) 
			Destroy (gameObject);
		
		// handle what happens to the other object in their respective scripts
	}

}
