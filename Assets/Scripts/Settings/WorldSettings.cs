using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WorldSettings : MonoBehaviour
{
    public float sampleDistance;
    public int sampleSize;
    public float lightIntensity;
    public int waterOption;

    public TMP_InputField sampleSizeField;
    public TMP_InputField sampleDistanceField;
    public TMP_InputField lightIntensityField;
    public TMP_Dropdown waterDropdown;

    void Start()
    {
        sampleDistance = 20;
        sampleSize = 200;
        lightIntensity = 0.8f;
        waterOption = 1;
        
        sampleSizeField = GameObject.Find("SampleDimensions").GetComponent<TMP_InputField>();
        sampleSizeField.text = sampleSize.ToString();

        sampleDistanceField = GameObject.Find("SampleDistance").GetComponent<TMP_InputField>();
        sampleDistanceField.text = sampleDistance.ToString();

        lightIntensityField = GameObject.Find("FlashlightIntensity").GetComponent<TMP_InputField>();
        lightIntensityField.text = lightIntensity.ToString();

        waterDropdown = GameObject.Find("WaterView").GetComponent<TMP_Dropdown>();
        waterDropdown.value = waterOption;
    }

    // Cube dimension function
    public void SetDimensions(string dimensions)
    {
        double dim = double.Parse(dimensions);
        if (dim < 0 || dim > 1000) { sampleSizeField.text = sampleSize.ToString(); }
        else { sampleSize = (int)Math.Round(dim); }
    }

    // Cube distance sampling distance value
    public void SetDistance(string distance)
    {
        float dis = float.Parse(distance);
        if (dis < 0 || dis > 100) { sampleDistanceField.text = sampleDistance.ToString(); }
        else { sampleDistance = dis; }
    }

    //Set Flashlight intensity in percentage, from 0 to 1
    public void SetFlashlightIntensity(string intensity)
    {
        float inten = float.Parse(intensity);
        if (inten < 0 || inten > 1) { lightIntensityField.text = lightIntensity.ToString(); }
        else { lightIntensity = inten; }
        //lightIntensity = inten;
    }

    //Set water view, 0 = On, 1 = Off
    public void SetWaterView(int option)
    {
        waterOption = waterDropdown.value;
    }

}
