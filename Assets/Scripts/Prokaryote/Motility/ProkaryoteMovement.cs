using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProkaryoteMovement : MonoBehaviour
{
    public void MoveProkaryote(string gameObjectName) //Then gene of this bacteria should be imported, to inquire parameters
    {
        GameObject prokaryote = GameObject.Find(gameObjectName);
        Mesh mesh = prokaryote.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        float time_step = (float)Math.Pow(10, -6);

        //Gets brownian motion, currently only liquid is water, so its parameters are used
        ReturnBrown brownianMotion = BrownianMotion.MoveBody(vertices, (float)Math.Pow(10, -18), prokaryote.transform.position, (float)Math.Pow(10, -10), 997, 0.0180158f, 374.15f, 300, 3.3428f * (float)Math.Pow(10, 25), (float)Math.Pow(10, -6), 1);
        //Compute flagella motility, if available
        //Compute motion due to pilli, if available
        Vector3 drag = BrownianMotion.Drag(997, brownianMotion.transposition_a * time_step, new Vector3(0,0,0));
        Debug.Log(brownianMotion.transposition_a[0] * (float)Math.Pow(time_step, 2));
        prokaryote.transform.position += brownianMotion.transposition_a * (float)Math.Pow(time_step, 2);
    }
}
