using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProkaryoteWorldSettings : MonoBehaviour
{
    public bool isShown;
    GameObject[] UiElements = new GameObject[3];

    void Start()
    {
        isShown = false;
        //Create list of all elements presented in this settings
        //sampleSizeField = GameObject.Find("SampleDimensions").GetComponent<TMP_InputField>();
        //sampleSizeField.text = sampleSize.ToString();
        UiElements[0] = GameObject.Find("PanelTranspose");
        UiElements[1] = GameObject.Find("Create");
        UiElements[2] = GameObject.Find("Back");
    }

    public void ShowSettings()
    {
        //Show all setting elements
    }

    public void HideSettings()
    {
        //Hide all elements
    }

    public void LoadNewLevel()
    {
        //Loads the Prokaryote level and constructs the enviroment
    }
}
