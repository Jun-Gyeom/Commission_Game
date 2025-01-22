using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeReference] public List<MapData> MapDatas { get; private set; } = new List<MapData>();                      // 맵 데이터 리스트
    [field: SerializeReference] public List<CharacterData> CharacterDatas { get; private set; } = new List<CharacterData>();    // 캐릭터 데이터 리스트
    [field: SerializeReference] public MapData SelectedMap { get; private set; } = null;                                        // 선택된 맵
    [field: SerializeReference] public CharacterData SelectedCharacter { get; private set; } = null;                            // 선택된 캐릭터
    public DateTime CurrentTime { get; private set; } = new DateTime();                                                         // 현재 시간

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SelectMap(MapData map)
    {
        
    }
    
    public void SelectCharacter(CharacterData character)
    {
        
    }

    public void UpdateTime(DateTime time)
    {
        
    }

    public void ChangeScene(string sceneName)
    {
        
    }
}
