using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingHeld : MonoBehaviour {
    [System.NonSerialized]
    public bool beingHeld;
	// Use this for initialization
	void Start () {
        beingHeld = false;
	}
}
