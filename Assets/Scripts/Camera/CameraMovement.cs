using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
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

    public GenerateSampleCube generateCube = new GenerateSampleCube();
    private HoldInformation information;
    private WorldSettings settings;

    void Start()
    {
        cameraDirection = this.transform.forward;
        myRigidbody = GetComponent<Rigidbody>();
        information = GameObject.FindObjectOfType<HoldInformation>();
        settings = FindObjectOfType<WorldSettings>();
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

        //Get direction for creating cube
        if (information.sampling == true)
        {
            cameraDirection = this.transform.forward;
            cameraDirection *= settings.sampleDistance;
            cameraDirection += this.transform.position;
            generateCube.DecideOnGenerateMesh(cameraDirection);
        }
        else { MapDestroy.DestroySampleCube(); }
    }


}
