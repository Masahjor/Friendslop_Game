using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public void GenerateMap() 
    { 
    float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);


        MapDisplay display = MapGenerator.FindAnyObjectByType<MapDisplay> ();
        display.DrawNoiseMap (noiseMap);

    }
}
