using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProkaryote : MonoBehaviour
{
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Vector3 cameraDirection;
    private Vector3 positionMove;
    private bool plus;
    Rigidbody myRigidbody;

    [Header("Camera Settings")]
    public float cameraSpeed = 2.0f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    void Start()
    {
        cameraDirection = this.transform.forward;
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Move camera
        if (Input.GetKey(KeyCode.W))
        {
            cameraDirection = this.transform.forward;
            cameraDirection *= cameraSpeed / 50;
            myRigidbody.position += cameraDirection;
        }
        if (Input.GetKey(KeyCode.S))
        {
            cameraDirection = this.transform.forward;
            cameraDirection *= cameraSpeed / 50;
            myRigidbody.position -= cameraDirection;
        }
        if (Input.GetKey(KeyCode.A))
        {
            cameraDirection = this.transform.right;
            cameraDirection *= cameraSpeed / 50;
            myRigidbody.position -= cameraDirection;
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraDirection = this.transform.right;
            cameraDirection *= cameraSpeed / 50;
            myRigidbody.position += cameraDirection;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            cameraDirection = this.transform.up;
            cameraDirection *= cameraSpeed / 50;
            myRigidbody.position -= cameraDirection;
        }
        if (Input.GetKey(KeyCode.E))
        {
            cameraDirection = this.transform.up;
            cameraDirection *= cameraSpeed / 50;
            myRigidbody.position += cameraDirection;
        }

        //rotate the camera by mouse drag
        if (Input.GetMouseButton(1))
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}
