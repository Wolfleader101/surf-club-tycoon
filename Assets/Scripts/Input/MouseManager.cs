using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask objectsLayerMask;
    [SerializeField] private GameObject cursorFollow;

    private Vector2 _mousePos;
    private Vector3 _mouseWorldPos;
    private WorldInteractable _selectedInteractable;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FollowCursor();
        DragObject();
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        _mousePos = context.ReadValue<Vector2>();
    }

    public void OnMouseClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SelectObject();
        }
    }

    private void FollowCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, groundLayerMask))
        {
            _mouseWorldPos = hit.point;
            cursorFollow.transform.position = _mouseWorldPos;
        }
    }

    private void SelectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, objectsLayerMask))
        {
            var hitObject = hit.collider.gameObject;
            var worldInteractable = hitObject.GetComponent<WorldInteractable>();
            if (worldInteractable != null)
            {
                _selectedInteractable = worldInteractable.selected == false ? worldInteractable : null;
                worldInteractable.selected = !worldInteractable.selected;
                
                worldInteractable.OnInteract();
                
                if (worldInteractable.selected)
                {
                    worldInteractable.Select(_mouseWorldPos);
                }
                else
                {
                    worldInteractable.DeSelect(_mouseWorldPos);
                }
                

            }
            
        }
    }

    private void DragObject()
    {
        if (_selectedInteractable != null && _selectedInteractable.selected)
        {
            _selectedInteractable.Drag(_mouseWorldPos + new Vector3(0, 0.5f, 0));
        }
    }
}