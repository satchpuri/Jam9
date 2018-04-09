using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

	public bool activated;
	public Door gemDoor;
	private Transform childSpr;
	private int activeTriggers;

	// Use this for initialization
	void Start () {
		activated = false;
		childSpr = this.gameObject.transform.GetChild(0);
		activeTriggers = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//if a player steps on the button, or a rock is placed on the button
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Rock")
		{
			activeTriggers++;
			CheckActiveTriggers ();
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		//if a player steps off the button, or a rock is taken off the button
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Rock")
		{
			activeTriggers--;
			CheckActiveTriggers ();
		}
	}

	private void CheckActiveTriggers()
	{
		//if any are active, shrink gem sprite and keep door open
		if (activeTriggers > 0) {
			activated = true;
			childSpr.localScale = new Vector3 (1f, 0.75f, 1f);
			gemDoor.OpenDoor ();
		} else if (activeTriggers == 0) {
			//if not, sprite goes back to regular size, door closes
			activated = false;
			childSpr.localScale = new Vector3 (1f, 1f, 1f);
			gemDoor.CloseDoor ();
		}
	}
}
