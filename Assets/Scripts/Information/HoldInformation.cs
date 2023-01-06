using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldInformation : MonoBehaviour
{
    public TerrainInformation terrainInformation;
    public float terrainLength;
    public bool sampling;

    private ProkaryoteWorldSettings transposeSettings;

    void Start()
    {
        terrainInformation = new TerrainInformation();
        terrainLength = terrainInformation.GetTerrainSideLength();
        sampling = false;
        transposeSettings = GameObject.FindObjectOfType<ProkaryoteWorldSettings>();
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            if (sampling == false) { sampling = true; }
            else if (sampling == true) {sampling = false; }
        }

        //if (sampling == true && Input.GetMouseButton(0))
        //{
        //    transposeSettings.ShowSettings();
        //}

        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    if (transposeSettings.isShown == true)
        //    {
        //        transposeSettings.HideSettings();
        //    }
        //}
    }
}
