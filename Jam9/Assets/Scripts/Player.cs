using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player class: goes on top most parent of Player object, handles all player input
public class Player : MonoBehaviour {
	int HP = 3;
	float moveSpeed = 3f;
    private bool isHoldingObj;
    private GameObject heldObject;

	void Start () {
        isHoldingObj = false;
        heldObject = null;
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

        //move held object, set y slightly higher so it's above their head
        if(isHoldingObj == true)
        {
            heldObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        }
	}

    //pickup object
    void OnTriggerEnter2D(Collider2D other)
    {
        //When adding other objects to pick up, add their tags to the checks as well
        if (other.gameObject.tag == "Key" && isHoldingObj == false)
        {
            isHoldingObj = true;
            heldObject = other.gameObject;
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //if touching a door and holding a key, open door
        if (other.gameObject.tag == "Door" && isHoldingObj == true && heldObject.tag == "Key")
        {
            isHoldingObj = false;
            Destroy(heldObject);
            heldObject = null;
            other.collider.GetComponent<Door>().OpenDoor();
        }
    }


}
