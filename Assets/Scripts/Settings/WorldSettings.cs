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

    public TMP_InputField sampleSizeField;
    public TMP_InputField sampleDistanceField;

    void Start()
    {
        sampleDistance = 20;
        sampleSize = 200;
        
        sampleSizeField = GameObject.Find("SampleDimensions").GetComponent<TMP_InputField>();
        sampleSizeField.text = sampleSize.ToString();

        sampleDistanceField = GameObject.Find("SampleDistance").GetComponent<TMP_InputField>();
        sampleDistanceField.text = sampleDistance.ToString();
    }

    // Cube dimension function
    public void SetDimensions(string dimensions)
    {
        double dim = double.Parse(dimensions);
        if (dim < 0 || dim > 1000) { sampleSizeField.text = sampleSize.ToString(); }
        else { sampleSize = (int)Math.Round(dim); }
    }

    // Cube distance sampling distanc value
    public void SetDistance(string distance)
    {
        float dis = float.Parse(distance);
        if (dis < 0 || dis > 100) { sampleDistanceField.text = sampleDistance.ToString(); }
        else { sampleDistance = dis; }
    }

}
