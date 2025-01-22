using UnityEngine;

public class UIView : MonoBehaviour
{
    public bool IsLock { get; set; }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (!IsLock)
        {
            gameObject.SetActive(false);
        }
    }
}
