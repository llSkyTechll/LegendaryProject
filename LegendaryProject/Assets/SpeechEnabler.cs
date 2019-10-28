using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechEnabler : MonoBehaviour {

    private bool displayText;
    public Canvas canvas;
	// Use this for initialization
	void Start () {
        canvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

         canvas.enabled=GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouvementCamera>().PlayerNotFocused;

    }
}
