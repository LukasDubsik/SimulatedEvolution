using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProkaryoteWorldInformation : MonoBehaviour
{
    private ProkaryoteSettings settings;
    private ProkaryoteMovement movement;

    void Start()
    {
        settings = GameObject.FindObjectOfType<ProkaryoteSettings>();
        movement = GameObject.FindObjectOfType<ProkaryoteMovement>();

        SetProkaryoteLevel.GenerateEnclosure(100);
    }

    void Update()
    {
        movement.MoveProkaryote("b_11");
    }
}
