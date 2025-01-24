using System;
using Unity.VisualScripting;
using UnityEngine;

public class UIView : MonoBehaviour
{
    public bool IsLock { get; set; }

    protected virtual void Awake()
    {
        //Init();
    }

    private void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        if (!TryGetComponent(out RectTransform rectTransform))
        {
            Debug.LogError($"UIView({gameObject.name}) Has no RectTransform");
            return;
        }
        rectTransform.anchoredPosition = Vector2.zero;
        
        gameObject.SetActive(false);
    }
    
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        if (IsLock) Debug.Log($"UIView({gameObject.name}): UIView is lock");
        else gameObject.SetActive(false);
    }
}
