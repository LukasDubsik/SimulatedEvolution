using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDestroy : MonoBehaviour
{
    public static void DestroyPrevious()
    {
        var terrain = GameObject.Find("Terrain");
        if (terrain != null) { DestroyImmediate(terrain); }

        for (int i = 0; i < 5; i++)
        {
            var platform = GameObject.Find("TerrainPlatform"+i);
            if (platform != null) { DestroyImmediate(platform); }
            var enclosure = GameObject.Find("TerrainEnclosure" + i);
            if (enclosure != null) { DestroyImmediate(enclosure); }
        }
    }

    public static void DestroySampleCube()
    {
        var cube = GameObject.Find("SampleCube");
        if (cube != null) { DestroyImmediate(cube); }
    }
}
