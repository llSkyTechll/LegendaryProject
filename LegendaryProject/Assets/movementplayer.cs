using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementplayer : MonoBehaviour {
    private CharacterController characterControleur;
    
	// Use this for initialization
	void Start () {
        characterControleur = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update () {

        processMovementInput();

    }
    void processMovementInput()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal,0,vertical);
        Vector3 ajustedMovement = transform.TransformDirection(movement);
        characterControleur.SimpleMove(ajustedMovement * Time.deltaTime * 500);
    }
}
