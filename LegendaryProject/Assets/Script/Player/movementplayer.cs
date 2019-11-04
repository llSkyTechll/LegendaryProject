using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementplayer : MonoBehaviour {
    private CharacterController characterController;
    private Vector3 movementY = Vector3.zero;
    private Vector3 movementXZ= Vector3.zero;
    private float currentX = 0.0f;
    private Animator animator;
    private float sensivity;
    public float distanceGround;
    public bool isGrounded = false;
    public float SPEED = 300;
    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
        distanceGround = characterController.bounds.extents.y;
        animator = GetComponentInChildren<Animator>();
        sensivity = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouvementCamera>().sensivityX;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckIfInAir();
        if (CanMove())
        {
            currentX += Input.GetAxis("Mouse X") * sensivity;
            if (isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    movementY.y = 3;
                    animator.Play("Jump");
                }
            }
            else
            {
                movementY.y -= 9.8f * Time.deltaTime;
            }

            movementXZ = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (movementXZ.x != 0 || movementXZ.z != 0)
            {
                animator.SetBool("Walking", true);
                Quaternion rotation = Quaternion.Euler(0, currentX, 0);
                transform.rotation = rotation;
            }
            else
            {
                animator.SetBool("Walking", false);
            }
            Vector3 ajustedMovementXZ = gameObject.transform.TransformDirection(movementXZ);
            if (Input.GetAxis("Sprint") != 0)
            {
                ajustedMovementXZ = ajustedMovementXZ.normalized * SPEED * 2.5f;
                if (animator.GetBool("Walking"))
                {
                    animator.SetBool("Running", true);
                }
                else
                {
                    animator.SetBool("Running", false);
                }
            }
            else
            {
                ajustedMovementXZ = ajustedMovementXZ.normalized * SPEED;
                animator.SetBool("Running", false);
            }

            Vector3 ajustedMovement = ajustedMovementXZ + movementY;
            characterController.Move(ajustedMovement * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
        }

    }

    private static bool CanMove()
    {
        return !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouvementCamera>().PlayerNotFocused &&
            !GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<KeyPressPanel>().GetInventoryIsOpen() &&
            !GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetIsDead();
    }

    void LateUpdate()
    {
        if (isGrounded && movementY.y <0)
        {
            movementY.y = 0;
        }
    }

    void CheckIfInAir()
    {
        isGrounded=Physics.Raycast(transform.position, -Vector3.up, distanceGround + 0.1f/*, 9*/);
    }
}
