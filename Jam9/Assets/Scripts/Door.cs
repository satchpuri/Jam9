using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private SpriteRenderer spriteR;
    public Sprite openSprite;
    public Sprite closedSprite;
    public Collider2D collider;

    // Use this for initialization
    void Start () {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        spriteR.sprite = closedSprite;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    //When opened, change sprite and disable collider
    public void OpenDoor()
    {
        spriteR.sprite = openSprite;
        collider.enabled = false;
    }
}
