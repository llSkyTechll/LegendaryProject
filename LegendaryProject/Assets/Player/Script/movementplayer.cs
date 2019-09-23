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
        //if(Input.GetAxis("Mouse X") != 0)
        //{
        //    print(Input.GetAxis("Mouse X"));
        //}
        //if (Input.GetAxis("Mouse Y") != 0)
        //{
        //    print(Input.GetAxis("Mouse Y"));
        //}
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
