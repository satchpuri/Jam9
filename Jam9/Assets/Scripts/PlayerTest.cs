using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour {
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    float GetX()
    {
        if (Input.GetKey(KeyCode.A))
        {
            return -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            return 1;
        }
        return 0;
    }

    float GetY()
    {
        if (Input.GetKey(KeyCode.S))
        {
            return -1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            return 1;
        }
        return 0;
    }
    void CheckInput()
    {
        float x = GetX() * Time.deltaTime * 150f;//Input.GetAxisRaw("RHorizontal") * Time.deltaTime * 150f;
        float y = GetY() * Time.deltaTime * 150f;//Input.GetAxisRaw("RVertical") * Time.deltaTime * 150f;
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
