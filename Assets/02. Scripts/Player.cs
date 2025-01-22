using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: SerializeField] public float MoveSpeed { get; private set; }
    public List<GameObject> DetectedInteractableGameObjects { get; private set; } = new List<GameObject>(); // 플레이어 근처에서 감지된 상호 작용 가능한 게임 오브젝트 리스트 
    
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    #region Unity Event
    private void Awake()
    {
        if (!TryGetComponent(out _rigidbody2D)) Debug.LogError("Player.cs: Player has no Rigidbody2D component");
        if (!TryGetComponent(out _animator)) Debug.LogError("Player.cs: Player has no Animator component"); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 감지된 객체가 상호 작용 가능한 게임 오브젝트인지 검사하여 리스트에 추가 
        if (other.gameObject.TryGetComponent(out IInteractable interactable))
            DetectedInteractableGameObjects.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 감지된 객체가 상호 작용 가능한 게임 오브젝트인지 검사하여 리스트에서 제거 
        if (other.gameObject.TryGetComponent(out IInteractable interactable))
            DetectedInteractableGameObjects.Remove(other.gameObject);
    }

    #endregion

    #region InputSystem

    private void OnMove(InputValue value)
    {
        Vector2 direction = new Vector2(value.Get<Vector2>().x, value.Get<Vector2>().y).normalized;
        Move(direction);
    }

    private void OnInteract()
    {
        GameObject interactableObject = GetNearestInteractableGameObject(DetectedInteractableGameObjects);
        if (interactableObject != null)
        {
            if (!interactableObject.TryGetComponent(out IInteractable interactable))
            {
                Debug.LogError("The gameObject has no IInteractable component");
                return;
            }
            Interact(interactable);
        }
    }

    #endregion

    public void Move(Vector2 direction)
    {
        _rigidbody2D.linearVelocity = direction * MoveSpeed;
    }

    public void Interact(IInteractable interactable)
    {
        interactable.Interact();
    }

    /// <summary>
    /// 플레이어와 가장 가까운 상호 작용 가능한 게임 오브젝트를 찾습니다. 
    /// </summary>
    /// <param name="interactableGameObjects">검색할 상호 작용 가능한 게임 오브젝트 리스트</param>
    /// <returns>플레이어와 가장 가까운 상호 작용 가능한 게임 오브젝트</returns>
    private GameObject GetNearestInteractableGameObject(List<GameObject> interactableGameObjects)
    {
        if (interactableGameObjects.Count == 0) return null;
        
        GameObject nearestInteractableGameObject = interactableGameObjects[0];

        foreach (var interactableGameObject in interactableGameObjects)
        {
            if (Vector2.Distance(interactableGameObject.transform.position, transform.position) <
                Vector2.Distance(nearestInteractableGameObject.transform.position, transform.position))
            {
                nearestInteractableGameObject = interactableGameObject;
            }
        }

        return nearestInteractableGameObject; 
    }
}
