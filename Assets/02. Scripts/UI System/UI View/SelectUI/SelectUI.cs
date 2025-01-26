using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUI : UIView
{ 
    [Header("Select Option Prefabs")]
    [SerializeField] private GameObject _selectCharacterOptionPrefab;                                   // 캐릭터 선택 옵션 프리팹
    [SerializeField] private GameObject _selectMapOptionPrefab;                                         // 맵 선택 옵션 프리팹

    [Header("Content Transforms")] 
    [SerializeField] private Transform _characterContentTransform;                                      // 캐릭터 선택 옵션 프리팹 생성할 계층 위치
    [SerializeField] private Transform _mapContentTransform;                                            // 맵 선택 옵션 프리팹 생성할 계층 위치
    
    [Header("Buttons")]
    [SerializeField] private Button _selectCharacterAndMapButton;                                       // 캐릭터 & 맵 선택 완료 버튼 

    private List<SelectCharacterOption> _selectCharacterOptions = new List<SelectCharacterOption>();    // 캐릭터 선택 옵션 리스트
    private List<SelectMapOption> _selectMapOptions = new List<SelectMapOption>();                      // 맵 선택 옵션 리스트

    #region Unity Event
    protected override void Awake()
    {
        base.Awake();

        _selectCharacterAndMapButton.onClick.AddListener(() =>
        {
            GameManager.Instance.StartGame();
        });
    }

    public override void Init()
    {
        base.Init();

        CreateSelectCharacterOption();
        CreateSelectMapOption();
    }
    
    #endregion

    /// <summary>
    /// 선택 가능한 캐릭터 선택지를 동적으로 생성합니다.
    /// </summary>
    private void CreateSelectCharacterOption()
    {
        foreach (var characterData in GameManager.Instance.CharacterDatas)
        {
            GameObject selectCharacterOptionGameObject = Instantiate(_selectCharacterOptionPrefab, _characterContentTransform);
            selectCharacterOptionGameObject.TryGetComponent(out SelectCharacterOption selectCharacterOption);
            selectCharacterOption.CharacterData = characterData;
            selectCharacterOption.CharacterIconImage.sprite = characterData.characterIcon;
            selectCharacterOption.CharacterNameText.text = characterData.characterName;
            selectCharacterOption.SelectButton.onClick.AddListener(() => SetSelectedCharacterOption(selectCharacterOption));
            _selectCharacterOptions.Add(selectCharacterOption);
        }
    }
    
    /// <summary>
    /// 선택 가능한 맵 선택지를 동적으로 생성합니다.
    /// </summary>
    private void CreateSelectMapOption()
    {
        foreach (var mapData in GameManager.Instance.MapDatas)
        {
            GameObject selectMapOptionGameObject = Instantiate(_selectMapOptionPrefab, _mapContentTransform);
            selectMapOptionGameObject.TryGetComponent(out SelectMapOption selectMapOption);
            selectMapOption.MapData = mapData; 
            selectMapOption.MapIconImage.sprite = mapData.mapIcon;
            selectMapOption.MapNameText.text = mapData.mapName;
            selectMapOption.SelectButton.onClick.AddListener(() => SetSelectedMapOption(selectMapOption));
            _selectMapOptions.Add(selectMapOption);
        }
    }
    
    /// <summary>
    /// 매개변수의 캐릭터 옵션을 선택하고 나머지 캐릭터 옵션을 선택 해제합니다. 
    /// </summary>
    /// <param name="selectedCharacterOption">선택할 캐릭터 옵션</param>
    private void SetSelectedCharacterOption(SelectCharacterOption selectedCharacterOption)
    {
        // 리스트에 해당 매개변수 인스턴스가 있는지 확인
        bool hasOption = false;
        foreach (var selectCharacterOption in _selectCharacterOptions)
        {
            if (selectCharacterOption == selectedCharacterOption)
                hasOption = true;
        }

        if (hasOption)
        {
            // 선택되어 있는 모든 옵션을 선택 해제한 후 매개변수의 옵션 선택 
            foreach (var selectCharacterOption in _selectCharacterOptions)
            {
                if (selectCharacterOption.IsSelected)
                {
                    selectCharacterOption.Deselect();
                }
            }
            selectedCharacterOption.Select();
        }
    }
    
    /// <summary>
    /// 매개변수의 맵 옵션을 선택하고 나머지 맵 옵션을 선택 해제합니다. 
    /// </summary>
    /// <param name="selectedMapOption">선택할 맵 옵션</param>
    private void SetSelectedMapOption(SelectMapOption selectedMapOption)
    {
        // 리스트에 해당 매개변수 인스턴스가 있는지 확인
        bool hasOption = false;
        foreach (var selectMapOption in _selectMapOptions)
        {
            if (selectMapOption == selectedMapOption)
                hasOption = true;
        }

        if (hasOption)
        {
            // 선택되어 있는 모든 옵션을 선택 해제한 후 매개변수의 옵션 선택 
            foreach (var selectMapOption in _selectMapOptions)
            {
                if (selectMapOption.IsSelected)
                {
                    selectMapOption.Deselect();
                }
            }
            selectedMapOption.Select();
        }
    }
}
