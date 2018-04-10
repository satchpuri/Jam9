using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {
    [SerializeField] int nextLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CameraSceneTransitioner.sharedInstance.Transition(nextLevel);
        }
    }
}
