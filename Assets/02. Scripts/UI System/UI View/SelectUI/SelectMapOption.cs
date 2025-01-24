using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectMapOption : MonoBehaviour
{
    public MapData MapData { get; set; }                                            // 맵 데이터 
    public bool IsSelected { get; private set; }                                    // 선택되었는지 여부 
    
    [field: Header("Components")]
    [field: SerializeField] public Button SelectButton { get; private set; }        // 버튼 컴포넌트 
    [field: SerializeField] public Image MapIconImage { get; private set; }         // 맵 아이콘 이미지 컴포넌트 
    [field: SerializeField] public TMP_Text MapNameText { get; private set; }       // 맵 이름 텍스트 컴포넌트 

    [Header("Highlighted Border")]
    [SerializeField] private GameObject _highlightedBorderGameObject;               // 강조 테두리 게임오브젝트

    public void Select()
    {
        IsSelected = true;
        
        // 강조 테두리 활성화 
        _highlightedBorderGameObject.SetActive(true);
        
        // 데이터상 맵 선택 
        GameManager.Instance.SelectMap(MapData);
    }
    
    public void Deselect()
    {
        IsSelected = false;
        
        // 강조 테두리 비활성화 
        _highlightedBorderGameObject.SetActive(false);
    }
}
