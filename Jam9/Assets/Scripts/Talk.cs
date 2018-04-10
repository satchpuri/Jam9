using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : MonoBehaviour {
    public string[] text;
    bool playerNear;

	void Start () {
        playerNear = false;
	}
	

	void Update () {
		if (Input.GetKeyDown(DialogueController.DialogueButton) && playerNear && !DialogueController.sharedInstance.dialogueActive && Time.timeScale != 0f)
        {
            DialogueController.sharedInstance.ShowDialogue(text, transform.position);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            DialogueController.sharedInstance.ShowSpeechIndicator(gameObject.transform.position);
            playerNear = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            DialogueController.sharedInstance.HideSpeechIndicator(gameObject.transform.position);
            playerNear = false;
        }
    }
}
