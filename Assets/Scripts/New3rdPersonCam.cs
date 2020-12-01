using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New3rdPersonCam : MonoBehaviour
{
    [Header("CameraAttributes")]
    public Transform Target;
    public float TargetDist;
    public float MouseSensitivity;

    private float yAxis;
    private float xAxis;
    private Vector2 vertMax = new Vector2(-20, 65);

    public float rotSmoothTime = .12f;
    private Vector3 rotSmoothVel;
    Vector3 currentRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        xAxis += Input.GetAxis("Mouse X") * MouseSensitivity;
        yAxis -= Input.GetAxis("Mouse Y") * MouseSensitivity;
        yAxis = Mathf.Clamp(yAxis, vertMax.x, vertMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(yAxis, xAxis), ref rotSmoothVel, rotSmoothTime);

        transform.eulerAngles = currentRotation;

        transform.position = Target.position - transform.forward * TargetDist;
        if (Time.timeScale == 0)
        {
            Cursor.visible = true;
        }
    } 
}
