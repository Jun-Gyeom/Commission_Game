using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [field: SerializeField] public List<UIView> UIViews { get; private set; } = new List<UIView>();
    public bool IsFading { get; private set; }
    
    [SerializeField] private GameObject _fadeGameObject;
    [SerializeField] private Image _fadeImage;
    
    private Stack<UIView> _uiViewStack = new Stack<UIView>();

    // TODO - Lock 기능 추가 
    public void OpenUIView(string uiViewName)
    {
        // 최상단 UI View 숨기기 
        if (_uiViewStack.Count != 0) 
            _uiViewStack.Peek().Hide();
        
        // 리스트에서 UI View 검색  
        UIView newUIView = null;
        foreach (var uiView in UIViews)
        {
            if (uiView.gameObject.name == uiViewName)
            {
                newUIView = uiView;
                break;
            }
        }
        
        // 해당 이름의 UI View가 존재하지 않는지 확인
        if (!newUIView)
        {
            Debug.LogError($"UIView of this name not found: {uiViewName}");
            return;
        }
        
        // 새로운 UI View 표시 
        newUIView.Show();

        // 새로운 UI View 스택에 추가 
        _uiViewStack.Push(newUIView);
    }

    public void CloseTopUIView()
    {
        // 스택이 비어있는지 확인 
        if (_uiViewStack.Count == 0)
            return;

        // 최상단 UI View 숨기기 
        _uiViewStack.Peek().Hide();

        // 최상단 UI View 스택에서 제거 
        _uiViewStack.Pop();
        
        // 현재 스택의 최상단 UI View 표시 
        if (_uiViewStack.Count != 0)
            _uiViewStack.Peek().Show();
    }

    public void CloseAllUIView()
    {
        for (int i = 0; i < _uiViewStack.Count; i++)
        {
            // 최상단 UI View 숨기기 
            _uiViewStack.Peek().Hide();

            // 최상단 UI View 스택에서 제거 
            _uiViewStack.Pop();
        }
    }

    public void FadeIn(float duration, Action action)
    {
        IsFading = true;
        _fadeImage.color = new Color(0f, 0f, 0f, 0f);
        _fadeGameObject.SetActive(true);
        _fadeImage.DOFade(1f, duration)
            .OnComplete(() =>
            {
                action?.Invoke();
                IsFading = false;
            });
    }

    public void FadeOut(float duration, Action action)
    {
        IsFading = true;
        _fadeImage.color = new Color(0f, 0f, 0f, 1f);
        _fadeGameObject.SetActive(true);
        _fadeImage.DOFade(0f, duration)
            .OnComplete(() =>
            {
                action?.Invoke();
                _fadeGameObject.SetActive(false);
                IsFading = false;
            });
    }
}
