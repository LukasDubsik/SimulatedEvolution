using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDependency : MonoBehaviour
{
    private CameraMovement camera;

    void Start()
    {
        camera = GameObject.FindObjectOfType<CameraMovement>();
        this.transform.forward = camera.cameraDirection;
        this.transform.position = camera.cameraPosition;
    }

    void Update()
    {
        this.transform.forward = camera.cameraDirection;
        this.transform.position = camera.cameraPosition;
    }
}
