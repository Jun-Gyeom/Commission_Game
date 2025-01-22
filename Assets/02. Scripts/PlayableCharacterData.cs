using UnityEngine;

[CreateAssetMenu(fileName = "Playable Character Data", menuName = "Scriptable Objects/Playable Character Data")]
public class PlayableCharacterData : CharacterData
{
    public GameObject playableCharacterPrefab;  // 캐릭터 플레이어 프리팹 
    public Sprite characterIcon;                // 캐릭터 아이콘
}
