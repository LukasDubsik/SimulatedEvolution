using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProkaryoteSettings : MonoBehaviour
{
    public float enclosureSize; //given in micrometers

    public TMP_InputField enclosureField;

    void Start()
    {
        enclosureSize = 100;

        enclosureField = GameObject.Find("EnclosureSize").GetComponent<TMP_InputField>();
        enclosureField.text = enclosureSize.ToString();
    }

    public void SetEnclosureSize(string size)
    {
        double dim = double.Parse(size);
        if (dim < 1 || dim > 1000) { enclosureField.text = enclosureSize.ToString(); }
        else { enclosureSize = (int)Math.Round(dim); }
    }
}
