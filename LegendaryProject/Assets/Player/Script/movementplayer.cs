using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementplayer : MonoBehaviour {
    private CharacterController characterControleur;
    private Rigidbody rdb;
    private float currentX = 0.0f;
    private float momentum = 0.0f;
    // Use this for initialization
    void Start () {
        characterControleur = GetComponent<CharacterController>();
        rdb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Jump") != 0)
        {
            momentum = 10;
        }
        currentX += Input.GetAxis("Mouse X");
        processMovementInput();
      
    }
    void processMovementInput()
    { 
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (momentum > 0)
        {
            transform.Translate(0, momentum/10, 0);
            momentum--;
        }
        Vector3 movement = new Vector3(horizontal,0,vertical);
        Vector3 ajustedMovement = transform.TransformDirection(movement);
        characterControleur.SimpleMove(ajustedMovement * Time.deltaTime * 500);
        if (ajustedMovement!= new Vector3())
        {
            Quaternion rotation = Quaternion.Euler(0, currentX, 0);
            transform.rotation = rotation;
        }
    }
}
