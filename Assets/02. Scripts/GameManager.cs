using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeReference] public List<PlayableCharacterData> CharacterDatas { get; private set; } 
        = new List<PlayableCharacterData>();                                                                                    // 캐릭터 데이터 리스트
    [field: SerializeReference] public List<MapData> MapDatas { get; private set; } = new List<MapData>();                      // 맵 데이터 리스트
    [field: SerializeReference] public PlayableCharacterData SelectedCharacter { get; private set; } = null;                    // 선택된 캐릭터
    [field: SerializeReference] public MapData SelectedMap { get; private set; } = null;                                        // 선택된 맵
    public DateTime CurrentTime { get; private set; } = new DateTime();                                                         // 현재 시간

    [Header("Scene Management")] 
    public const string BaseScene = "01. Base";
    public const string MainScene = "02. Main";
    public const string GameScene = "03. Game";
    [field: SerializeField] public float ChangeSceneFadeInDuration { get; private set; }                                        // 씬 변경 시 페이드인 지속시간
    [field: SerializeField] public float ChangeSceneFadeOutDuration { get; private set; }                                       // 씬 변경 시 페이드아웃 지속시간
    [field: SerializeField] public string CurrentSceneName { get; private set; }                                                // 현재 씬 이름
    [field: SerializeField] public string CurrentMapSceneName { get; private set; }                                             // 현재 맵 씬 이름
    public bool IsSceneLoading { get; private set; }                                                                            // 씬이 변경되고 있는지 여부 

    [SerializeField] private string _initialSceneName = MainScene;                                                              // 초기 씬 이름 

    private void Start()
    {
        if (SceneManager.loadedSceneCount < 2)
            StartCoroutine(LoadSceneAsync(_initialSceneName, () =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(_initialSceneName));
                CurrentSceneName = _initialSceneName;
            }));
        else
        {
            CurrentSceneName = SceneManager.GetSceneAt(1).name;
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
        }
    }

    public void StartGame()
    {
        if (SelectedCharacter == null || SelectedMap == null)
        {
            Debug.LogError("GameManager: Character or map are no selected");
            return;
        }
        
        ChangeScene(GameScene, ChangeSceneFadeInDuration, ChangeSceneFadeOutDuration, () =>
        {
            UIManager.Instance.CloseAllUIView();
            
            // 맵 씬 로드 
            if (CurrentMapSceneName != SelectedMap.mapSceneName)
            {
                if (!String.IsNullOrEmpty(CurrentMapSceneName))
                {
                    StartCoroutine(UnLoadSceneAsync(CurrentMapSceneName));
                }

                StartCoroutine(LoadSceneAsync(SelectedMap.mapSceneName));
                CurrentMapSceneName = SelectedMap.mapSceneName;
            }

            // 플레이어 생성 
            Instantiate(SelectedCharacter.playableCharacterPrefab);
            
            // NPC 임의 위치에 생성 
            
        });
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void SelectCharacter(PlayableCharacterData character)
    {
        bool hasCharacter = false;
        foreach (var characterData in CharacterDatas)
        {
            if (characterData == character)
                hasCharacter = true;
        }
        
        if (hasCharacter)
            SelectedCharacter = character;
        else
            Debug.LogError("GameManager: Can't select this character.");
    }
    
    public void SelectMap(MapData map)
    {
        bool hasMap = false;
        foreach (var mapData in MapDatas)
        {
            if (mapData == map)
                hasMap = true;
        }
        
        if (hasMap)
            SelectedMap = map;
        else
            Debug.LogError("GameManager: Can't select this map.");
    }

    public void UpdateTime(DateTime time)
    {
        
    }

    private void ChangeScene(string sceneName, float fadeInDuration, float fadeOutDuration, Action action = null)
    {
        if (IsSceneLoading)
        {
            Debug.LogError("GameManager: Can't load the scene. Scene is loading now");
            return;
        }
        
        IsSceneLoading = true; 
        UIManager.Instance.FadeIn(fadeInDuration, () =>
        {
            if (CurrentSceneName != null)
                StartCoroutine(UnloadCurrentSceneAndLoadNewScene(sceneName, () =>
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
                    CurrentSceneName = sceneName;
                    action?.Invoke();
                }));
            else
                StartCoroutine(LoadSceneAsync(sceneName, () =>
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
                    CurrentSceneName = sceneName;
                    action?.Invoke();
                }));
            
            UIManager.Instance.FadeOut(fadeOutDuration, () => IsSceneLoading = false);
        });
    }

    /// <summary>
    /// 매개변수의 이름을 가진 씬을 Additive 모드로 비동기 로드 합니다. 
    /// </summary>
    /// <param name="sceneName">로드할 씬의 이름</param>
    /// <param name="action">씬 로드 후 실행할 델리게이트</param>
    private IEnumerator LoadSceneAsync(string sceneName, Action action = null)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if (loadOperation == null) yield break;
        while (!loadOperation.isDone)
        {
            yield return null; 
        }
        action?.Invoke();
    }
    
    /// <summary>
    /// 매개변수의 이름을 가진 씬을 비동기 언로드 합니다. 
    /// </summary>
    /// <param name="sceneName">언로드할 씬의 이름</param>
    private IEnumerator UnLoadSceneAsync(string sceneName)
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneName);
        if (unloadOperation == null) yield break;
        while (!unloadOperation.isDone)
        {
            yield return null;
        }    
    }

    /// <summary>
    /// 기존 씬 비동기 언로드 후 새로운 씬을 Additive 모드로 로드합니다. 
    /// </summary>
    /// <param name="sceneName">로드할 씬의 이름</param>
    /// <param name="action">씬 로드 후 실행할 델리게이트</param>
    private IEnumerator UnloadCurrentSceneAndLoadNewScene(string sceneName, Action action = null)
    {
        // 기존 씬 언로드
        if (CurrentSceneName != null)
        {
            yield return StartCoroutine(UnLoadSceneAsync(CurrentSceneName));
        }

        // 새로운 씬 로드
        yield return LoadSceneAsync(sceneName, action);
    }
}
