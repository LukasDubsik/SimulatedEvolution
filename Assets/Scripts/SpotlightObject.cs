using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotObject : MonoBehaviour
{

    private CameraMovement camera;

    void Start()
    {
        //camera = GameObject.FindObjectOfType<CameraMovement>();
        //this.transform.position = camera.cameraPosition;
        //this.transform.forward = camera.cameraDirection;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = camera.cameraPosition;
        //this.transform.forward = camera.cameraDirection;
    }

    public void UpadtePosition(Vector3 direction , Vector3 position)
    {
        this.transform.position = position;
        this.transform.forward = direction;
    }
}
