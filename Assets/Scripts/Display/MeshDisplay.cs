using UnityEngine;
using System.Collections;

public class MeshDisplay : MonoBehaviour
{
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    GameObject meshObject;
    MeshCollider meshCollider;

    public void DrawMesh(MeshData meshData, Material material, string meshName)
    {
        meshObject = new GameObject(meshName);
        meshRenderer = meshObject.AddComponent<MeshRenderer>();
        meshFilter = meshObject.AddComponent<MeshFilter>();
        meshCollider = meshObject.AddComponent(typeof(MeshCollider)) as MeshCollider;

        meshRenderer.material = material;
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshCollider.sharedMesh = meshData.CreateMesh();        
    }

    public void DrawBorder(MeshData[] meshData, Material materialPlatform, Material materialEnclosure)
    {
        var platform = GameObject.Find("Platform");
        for (int i=0; i < 5; i++)
        {
            meshObject = new GameObject("TerrainPlatform" + i);
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshCollider = meshObject.AddComponent(typeof(MeshCollider)) as MeshCollider;

            meshRenderer.material = materialPlatform;
            meshFilter.sharedMesh = meshData[i].CreateMesh();
            meshCollider.sharedMesh = meshData[i].CreateMesh();

            meshObject.transform.parent = platform.transform;
        }

        var enclosure = GameObject.Find("Enclosure");
        for (int i = 5; i < 10; i++)
        {
            meshObject = new GameObject("TerrainEnclosure" + (i-5));
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshCollider = meshObject.AddComponent(typeof(MeshCollider)) as MeshCollider;

            meshRenderer.material = materialEnclosure;
            meshFilter.sharedMesh = meshData[i].CreateMesh();
            meshCollider.sharedMesh = meshData[i].CreateMesh();

            meshObject.transform.parent = enclosure.transform;
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }

}