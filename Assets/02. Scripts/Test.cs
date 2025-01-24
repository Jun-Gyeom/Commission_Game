using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            UIManager.Instance.OpenUIView("Select UI");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            UIManager.Instance.OpenUIView("Escape Menu UI");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            UIManager.Instance.OpenUIView("Setting UI");
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.CloseTopUIView();
        }
    }
}
