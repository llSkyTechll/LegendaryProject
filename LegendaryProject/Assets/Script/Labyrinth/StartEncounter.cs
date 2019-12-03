using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEncounter : MonoBehaviour {
    public Oscillation oscillation;
    private bool Triggered = false;
	// Use this for initialization
	public void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<movementplayer>() != null && !Triggered)
        {
            oscillation.DoorUp();
            GameObject.FindObjectOfType<Spawner>().StartSpawner();
            Triggered = true;
        }
    }
}
