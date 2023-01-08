using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProkaryoteWorldInformation : MonoBehaviour
{
    private ProkaryoteSettings settings;

    void Start()
    {
        settings = GameObject.FindObjectOfType<ProkaryoteSettings>();

        SetProkaryoteLevel.GenerateEnclosure(100);
    }

    void Update()
    {
        
    }
}
