using UnityEngine;

public class Character : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log($"Interact to Character: {gameObject.name}");
    }
}
