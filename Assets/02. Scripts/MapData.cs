using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Sprite mapIcon; 
    public MapSize mapSize;
    public string mapSceneName; // 맵이 구현된 씬의 이름  
}
