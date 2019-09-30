using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementplayer : MonoBehaviour {
    private CharacterController characterController;
    private float currentX = 0.0f;
    public float momentum = 0.0f;
    private Animator animator;
    private float sensivity;
    public float distanceGround;
    public bool isGrounded = false;
    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
        distanceGround = characterController.bounds.extents.y;
        animator = GetComponentInChildren<Animator>();
        sensivity = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouvementCamera>().sensivityX;
    }
	
	// Update is called once per frame
	void Update () {
        CheckIfInAir();
        if (Input.GetAxis("Jump") != 0 && isGrounded)
        {
                momentum = 0.15f;
                animator.Play("Jump");
                transform.Translate(0, momentum, 0);
        }
        
        currentX += Input.GetAxis("Mouse X")*sensivity;
        processMovementInput();
      
    }

    void LateUpdate()
    {
        if (isGrounded && momentum <0)
        {
            momentum = 0;
        }
    }

    void processMovementInput()
    { 
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (!isGrounded)
        {
            transform.Translate(0, momentum, 0);
            momentum -=0.0045f;
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

    void CheckIfInAir()
    {
        if (!Physics.Raycast(transform.position, -Vector3.up, distanceGround + 0.1f,9))
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }
    }
}
