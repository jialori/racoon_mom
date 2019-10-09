﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private bool useController = true;

    // Constants for clamping camera angles and establishing boundaries for rotation
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    // Initialization of variables
    public Transform lookAt;
    public Transform camTransform;

    private Camera cam;

    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 10f;
    private float sensitivityX = 5.0f;
    private float sensitivityY = 5.0f;

    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        // Ensure rotation values correspond with movement of mouse (will need to change to fit controller later)
        currentX += GetXAxis() * sensitivityX;
        currentY += GetYAxis() * sensitivityY;

        // Clamp camera rotation
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private float GetXAxis() 
    {
        if (useController) 
        {
            return Input.GetAxis("RightJoystickX");
        } 
        else 
        {
            return Input.GetAxis("Mouse X");
        }
    }

    private float GetYAxis() 
    {
        if (useController) 
        {
            return Input.GetAxis("RightJoystickY");
        } 
        else 
        {
            return Input.GetAxis("Mouse Y");
        }
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
