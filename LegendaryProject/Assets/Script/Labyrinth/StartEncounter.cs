using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEncounter : MonoBehaviour {
    public Oscillation oscillation;
	// Use this for initialization
	public void OnTriggerEnter(Collider collider)
    {
        oscillation.DoorUp();
    }
}
