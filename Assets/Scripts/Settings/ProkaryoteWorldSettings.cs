using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class ProkaryoteWorldSettings : MonoBehaviour
{
    public bool isShown;

    private GameObject[] UiElements = new GameObject[3];
    private CameraMovement camera;
    private ProkaryoteSettings settingsProkaryote;

    void Start()
    {
        isShown = false;

        camera = GameObject.FindObjectOfType<CameraMovement>();
        settingsProkaryote = GameObject.FindObjectOfType<ProkaryoteSettings>();

        //Create list of all elements presented in this settings
        UiElements[0] = GameObject.Find("PanelTranspose");
        UiElements[1] = GameObject.Find("Create");
        UiElements[2] = GameObject.Find("Back");

        HideSettings();
    }

    public void ShowSettings()
    {
        //Show all setting elements
        UiElements[0].SetActive(true);
        UiElements[1].SetActive(true);
        UiElements[2].SetActive(true);

        isShown = true;
    }

    public void HideSettings()
    {
        //Hide all elements
        UiElements[0].SetActive(false);
        UiElements[1].SetActive(false);
        UiElements[2].SetActive(false);

        isShown = false;
    }

    //Binds to create button in ui
    public void LoadNewLevel()
    {
        //Loads the Prokaryote level and constructs the enviroment
        SceneManager.LoadScene(1);
    }

    //Binds to back button in ui
    public void BackButton()
    {
        HideSettings();
        camera.StartCamera();
    }
}
