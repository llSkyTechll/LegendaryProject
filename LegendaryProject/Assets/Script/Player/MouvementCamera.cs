using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementCamera : MonoBehaviour {

    public Transform lookAt;
    public Transform camTransform;
    public bool PlayerNotFocused=false; 
    private Transform playerTransform;

    //private Camera cam;

    private const float Y_ANGLE_MIN = 10.0f;
    private const float Y_ANGLE_MAX = 50.0f;


    private float distance = 5.0f;
    public float currentX = 0.0f;
    private float currentY = 0.0f;
    private float preInteractCurrentX = 0.0f;
    private float preInteractCurrentY = 0.0f;
    private float sensivityX = 10.0f;
    private float sensivityY = 4.0f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        camTransform = transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (CanMoveCamera())
        {
            currentX += Input.GetAxis("Mouse X") * sensivityX;
            currentY -= Input.GetAxis("Mouse Y") * sensivityY; // pouvoir modifier avec les options plus tard += ou -=
            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        }
    }

    private bool CanMoveCamera()
    {
        return !PlayerNotFocused && !PauseMenu.gameIsPaused &&
            !GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<KeyPressPanel>().GetInventoryIsOpen() &&
            !GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetIsDead();
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }

    public void ChangeFocus(Transform newFocus)
    {
        preInteractCurrentX = currentX;
        preInteractCurrentY = currentY;
        distance = 3f;
        lookAt = newFocus;
        PlayerNotFocused = true;
    }

    public void ResetFocus()
    {
        currentX = preInteractCurrentX;
        currentY = preInteractCurrentY;
        distance = 5f;
        lookAt = playerTransform;
        PlayerNotFocused = false;
    }
}
