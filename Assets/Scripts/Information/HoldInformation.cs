using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HoldInformation : MonoBehaviour
{
    public TerrainInformation terrainInformation;
    [System.NonSerialized]
    public float terrainLength;
    public bool sampling;

    private ProkaryoteWorldSettings transposeSettings;
    private CameraMovement camera;
    private WorldSettings settings;

    float pastIntensity;

    void Start()
    {
        terrainInformation = new TerrainInformation();
        terrainLength = terrainInformation.GetTerrainSideLength();
        sampling = false;
        transposeSettings = GameObject.FindObjectOfType<ProkaryoteWorldSettings>();
        camera = GameObject.FindObjectOfType<CameraMovement>();
        settings = GameObject.FindObjectOfType<WorldSettings>();

        //Set up the enviroment, currently onlyone encviroment, midnight zone
        if (settings.waterOption == 0)
        {
            SetWorldVisual.SetUpWaterView(0.854f, 0.93f, true, (int)Math.Round((float)settings.lightIntensity * 40000), 0, 0.009f, 2f);
        }
        pastIntensity = (float)settings.lightIntensity;

        //GenerateProkaryote.RunProkaryoteGenerate(); //Tested generating files, passed
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            sampling = true;
        }

        if (Input.GetKey(KeyCode.Y))
        {
            sampling = false;
        }

        if (sampling == true && Input.GetMouseButton(0))
        {
            transposeSettings.ShowSettings();
            camera.StopCamera();
        }

        if (Input.GetKey(KeyCode.Escape) && transposeSettings.isShown == true)
        {
            transposeSettings.HideSettings();
            camera.StartCamera();
        }

        if (settings.waterOption == 0)
        {
            if (pastIntensity != (float)settings.lightIntensity)
            {
                SetWorldVisual.SetFlashlight(true, (int)Math.Round((float)settings.lightIntensity * 40000));
                pastIntensity = (float)settings.lightIntensity;
            }

            SetWorldVisual.SetUpWaterView(0.854f, 0.93f, true, (int)Math.Round((float)settings.lightIntensity * 40000), 0, 0.009f, 2f);
        }

        if (settings.waterOption == 1)
        {
            SetWorldVisual.TurnOffWaterView();
        }
    }
}
