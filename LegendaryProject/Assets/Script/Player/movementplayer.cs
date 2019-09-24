using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementplayer : MonoBehaviour {
    private CharacterController characterController;
    private Rigidbody rdb;
    private float currentX = 0.0f;
    private float momentum = 0.0f;
    private Animator animator;
    private float sensivity;
    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
        rdb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        sensivity = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouvementCamera>().sensivityX;
    }
	
	// Update is called once per frame
	void Update () {
        if (characterController.isGrounded)
        {
            momentum = 0;
        }
        if (Input.GetAxis("Jump") != 0)
        {
            if (characterController.isGrounded)
            {
                momentum = 3;
                animator.Play("Jump");
            }
        }
        
        currentX += Input.GetAxis("Mouse X")*sensivity;
        processMovementInput();
      
    }
    void processMovementInput()
    { 
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (momentum > 0)
        {
            transform.Translate(0, momentum/9.8f, 0);
            momentum-= Time.deltaTime*9.8f;
        }
        Vector3 movement = new Vector3(horizontal,0,vertical);
        Vector3 ajustedMovement = transform.TransformDirection(movement);
        characterController.SimpleMove(ajustedMovement * Time.deltaTime * 500);
        if (ajustedMovement!= new Vector3())
        {
            animator.SetBool("Walking", true);
            Quaternion rotation = Quaternion.Euler(0, currentX, 0);
            transform.rotation = rotation;
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }
}
