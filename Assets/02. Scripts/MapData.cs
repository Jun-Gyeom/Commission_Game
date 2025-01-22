using UnityEngine;

public enum MapSize
{
    None,
    Small,  // 2x2
    Medium, // 3x3
    Large   // 4x4
}

[CreateAssetMenu(fileName = "MapData", menuName = "Scriptable Objects/MapData")]
public class MapData : ScriptableObject
{
    public string mapName;
    public MapSize mapSize; 
}
