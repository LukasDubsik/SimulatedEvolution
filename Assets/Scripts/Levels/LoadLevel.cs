using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel
{
    public static void LoadProkaryote(float dimensions)
    {
        SceneManager.LoadScene("Prokaryote");
        VisualGenerator.GenerateEnclosure(dimensions);
    }

    public static void LoadWorldVisual()
    {

    }
}
