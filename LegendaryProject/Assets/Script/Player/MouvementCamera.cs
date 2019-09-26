using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementCamera : MonoBehaviour {

    public Transform lookAt;
    public Transform camTransform;

    private Camera cam;

    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;


    private float distance = 5.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float sensivityX = 10.0f;
    private float sensivityY = 4.0f;

    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;

    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X")*sensivityX;
        currentY -= Input.GetAxis("Mouse Y")*sensivityY; // pouvoir modifier avec les options plus tard += ou -=

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
