using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetProkaryoteLevel
{

    public static void GenerateEnclosure(float enclosureSize)
    {
        MeshDisplay meshDisplay = GameObject.FindObjectOfType<MeshDisplay>();
        AssignMaterials assignMaterials = GameObject.FindObjectOfType<AssignMaterials>();

        MeshData cube = GenerateShapes.GenerateCube(new Vector3(0, 0, 0), enclosureSize);

        Material material = assignMaterials.enclosureMaterial;

        meshDisplay.DrawMesh(cube, material, "ProkaryoteEnclosure");
    }
}
