using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour {
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        if (!DialogueController.sharedInstance.dialogueActive)
        {
            float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * 150f;
            float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * 150f;
            rb.velocity = new Vector3(x, y);
        }
    }
}
