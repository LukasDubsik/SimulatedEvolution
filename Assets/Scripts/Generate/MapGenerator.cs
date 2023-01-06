using UnityEngine;
using System.Collections;
using System.Linq;

public class MapGenerator : MonoBehaviour
{

    public int mapChunkSize;
    [Range(0, 6)]
    public int levelOfDetail;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public float heightPlatform;
    public float heightEnclosure;
    public int seed;
    public Vector2 offset;

    public bool useSmokers;
    float[,] smokersMap;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] regions;

    public Material materialTerrain;
    public Material materialPlatform;
    public Material materialEnclosure;

    public void GenerateMap()
    {
        smokersMap = Gauss.GenerateFalloffMap(mapChunkSize, 10, 3, 0.3f);
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];

        if (useSmokers)
        {
            for (int y = 0; y < mapChunkSize; y++)
            {
                for (int x = 0; x < mapChunkSize; x++)
                {
                    noiseMap[x, y] = noiseMap[x, y] + smokersMap[x, y];
                }
            }
        }

        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapChunkSize + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        //Generate individual meshes in this level and draw them
        MapDisplay display = FindObjectOfType<MapDisplay>();

        MapDestroy.DestroyPrevious();

        var terrainMesh = MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail, heightPlatform);
        display.DrawMesh(terrainMesh, materialTerrain);

        MeshData[] platform = BorderGenerator.GenerateMesh(terrainMesh, noiseMap.GetLength(0), 0);
        MeshData[] enclosure = BorderGenerator.GenerateMesh(terrainMesh, noiseMap.GetLength(0), heightEnclosure);

        var border = new MeshData[platform.Length + enclosure.Length];
        platform.CopyTo(border, 0);
        enclosure.CopyTo(border, platform.Length);

        display.DrawBorder(border, materialPlatform, materialEnclosure);
    }

    public void GenerateSmokers()
    {
        smokersMap = Gauss.GenerateFalloffMap(mapChunkSize, 4, 1, 2);
    }

    void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
        
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}
