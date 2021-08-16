using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
[DisallowMultipleComponent]
public sealed class PlayerSelectionController : MonoBehaviour
{
    public List<PlayerControlledEntity> m_SelectedEntities;

    private void Awake()
    {
        m_SelectedEntities = new List<PlayerControlledEntity>();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Keyboard.current.leftCtrlKey.isPressed)
            {
                
            }
        }
    }

    private PlayerControlledEntity GetEntityUnderMouse ()
    {
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out PlayerControlledEntity entity))
            {
                return entity;
            }
        }

        return null;
    }    
}
