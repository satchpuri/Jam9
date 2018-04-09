using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour {
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    void CheckInput()
    {
        float x = Input.GetAxisRaw("RHorizontal") * Time.deltaTime * 150f;
        float y = Input.GetAxisRaw("RVertical") * Time.deltaTime * 150f;
        Debug.Log(y);
        rb.velocity = new Vector3(x, y);
    }

    void FixedUpdate()
    {
        if (DialogueController.sharedInstance != null)
        {
            if (!DialogueController.sharedInstance.dialogueActive)
            {
                CheckInput();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            CheckInput();
        }
    }
}
