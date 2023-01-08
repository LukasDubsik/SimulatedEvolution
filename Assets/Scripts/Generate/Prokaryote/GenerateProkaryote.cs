using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Diagnostics;

public class GenerateProkaryote
{
    public static MeshData GenerateMesh(string instructionsName)
    {
        string[] lines = System.IO.File.ReadAllLines(@"Instructions\" + instructionsName);

        Vector3[] vertices = new Vector3[1];
        int[] triangles = new int[1];

        int verticeCount = 0;
        int triangleCount = 0;

        for (int i=0; i<lines.Length; i++)
        {
            if (lines[i].Substring(0, 8) == "VERTICES") { vertices = new Vector3[int.Parse(lines[i].Substring(9, 14))]; }
            if (lines[i].Substring(0, 9) == "TRIANGLES") { vertices = new Vector3[int.Parse(lines[i].Substring(10, 15))]; }
            if (lines[i][0] == 'V') 
            { 
                vertices[verticeCount] = new Vector3(int.Parse(lines[i].Split(' ')[1]), int.Parse(lines[i].Split(' ')[2]), int.Parse(lines[i].Split(' ')[3]));
                verticeCount += 1;
            }
            if (lines[i][0] == 'T')
            {
                triangles[triangleCount] = int.Parse(lines[i].Split(' ')[1]);
                triangles[triangleCount + 1] = int.Parse(lines[i].Split(' ')[2]);
                triangles[triangleCount + 2] = int.Parse(lines[i].Split(' ')[3]);
                triangleCount += 3;
            }
        }

        MeshData mesh = new MeshData(1, 1); //The vertice count or dimension are redundant
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        return mesh;
    }

    public static void RunProkaryoteGenerate()
    {
        //CurrentDirectory
        string path = Directory.GetCurrentDirectory();
        //The executable folder and file, where the c++ code for generating script for blender is
        path = path + "\\Assets\\Scripts\\Generate\\Prokaryote\\Executable\\ProkaryoteBody.exe";
        //Run the script (currently without arguments)
        UnityEngine.Debug.Log(0);
        var process = Process.Start(path);
        process.WaitForExit();
        UnityEngine.Debug.Log(1);
        //Run the code in blender
        Process.Start("cd C:\\Program Files\\Blender Foundation\\Blender 3.1 && blender --background --python C://Users//-//Desktop//test1.txt");
        UnityEngine.Debug.Log(2);
    }
}
