using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "None Playable Character Data", menuName = "Scriptable Objects/None Playable Character Data")]
public class NonePlayableCharacterData : CharacterData
{
    public GameObject nonePlayableCharacterPrefab;  // 캐릭터 NPC 프리팹 
    public List<string> dialogIDList;               // 캐릭터 대화 ID 리스트 
}
