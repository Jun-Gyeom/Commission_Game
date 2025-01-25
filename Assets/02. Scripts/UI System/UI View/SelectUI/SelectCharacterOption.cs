using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterOption : MonoBehaviour
{
    public PlayableCharacterData CharacterData { get; set; }                        // 캐릭터 데이터 
    public bool IsSelected { get; private set; }                                    // 선택되었는지 여부 
    
    [field: Header("Components")]
    [field: SerializeField] public Button SelectButton { get; private set; }        // 버튼 컴포넌트 
    [field: SerializeField] public Image CharacterIconImage { get; private set; }   // 캐릭터 아이콘 이미지 컴포넌트 
    [field: SerializeField] public TMP_Text CharacterNameText { get; private set; } // 캐릭터 이름 텍스트 컴포넌트 

    [Header("Highlighted Border")]
    [SerializeField] private GameObject _highlightedBorderGameObject;               // 강조 테두리 게임오브젝트

    public void Select()
    {
        IsSelected = true;
        
        // 강조 테두리 활성화 
        _highlightedBorderGameObject.SetActive(true);
        
        // 데이터상 캐릭터 선택 
        GameManager.Instance.SelectCharacter(CharacterData);
    }
    
    public void Deselect()
    {
        IsSelected = false;
        
        // 강조 테두리 비활성화 
        _highlightedBorderGameObject.SetActive(false);
    }
}