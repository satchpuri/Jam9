using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player class: goes on top most parent of Player object, handles all player input
// Player Prefab:
//	Player
//		Sprite
//		DirectionIndicator
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
	public int playerNumber = 0;
	[HideInInspector]
	public bool isDead = false;		// use for UI/gamestate calls

	[SerializeField]
	private int HP = 3;

	Rigidbody2D rb;
	[SerializeField]
	private float moveSpeed = 150f;

    private bool isHoldingObj;
    private GameObject heldObject;

	private GameObject aimIndicator;
	private Vector3 aimDirection;
	public GameObject bulletPrefab;

	private bool keyboard_Debug = true;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
        isHoldingObj = false;
        heldObject = null;

		// gets the child object AimIndicator
		aimIndicator = this.gameObject.transform.GetChild (1).gameObject;	// DirectionIndicator GameObject should be 1
	}

    // Player input check
    void CheckInput()
    {
		float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed;
		float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * moveSpeed;
		rb.velocity = new Vector3(x, y);

		// For Keyboard
		// NOTE: In order for camera movement to work, needs to be moving with the Controller (using rigidbody, not Translate())
		if (keyboard_Debug) {
			if (Input.GetKey (KeyCode.W)) {
				transform.Translate (0, 3f * Time.deltaTime, 0, Space.World);
			}
			if (Input.GetKey (KeyCode.A)) {
				transform.Translate (-3f * Time.deltaTime, 0, 0, Space.World);
			}
			if (Input.GetKey (KeyCode.D)) {
				transform.Translate (3f * Time.deltaTime, 0, 0, Space.World);
			}
			if (Input.GetKey (KeyCode.S)) {
				transform.Translate (0, -3f * Time.deltaTime, 0, Space.World);
			}
		}
    }

	void Update () {
        if (DialogueController.sharedInstance != null)
        {
			// Disable player movement when dialogue is active
            if (!DialogueController.sharedInstance.dialogueActive)
                CheckInput();
			// Input for firing
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("R2"))
				FireAttack();
        }
        else
        {
            CheckInput();
        }

        //move held object, set y slightly higher so it's above their head
        if(isHoldingObj == true)
        {
            heldObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        }

		// When HP goes down to 0
		if (HP <= 0) {
			isDead = true;
			Die ();
			Debug.Log ("P" + playerNumber + " has died.");
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

		//  Enemy Collision
		if (other.gameObject.tag == "Enemy") {
			HP--;
			Debug.Log ("P" + playerNumber + " HP = " + HP);
		}
    }

	void FireAttack() {
		// rotation shuld be towards indicator
		// get current aimDirection, which is the localPosition of the indicator since it is in proximity to the center of the player object
		aimDirection = aimIndicator.transform.localPosition;
		//Debug.Log (aimDirection.magnitude);

		// Do not allow fire if indicator at or close to 0 position
		// .58 is about the greatest value of  magnitude  of the indicator from the center, so stick must be pretty much fully extended to be able to fire
		if (aimDirection.magnitude >= 0.58f)
			Instantiate (bulletPrefab, transform.localPosition, Quaternion.LookRotation(aimDirection));
	}

	// death sequence
	void Die(){
		Destroy (gameObject);
	}


}
