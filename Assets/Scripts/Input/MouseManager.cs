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
            cursorFollow.transform.position = hit.point;
        }
    }

    private void SelectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, objectsLayerMask))
        {
            Debug.Log(hit.point);
        }
    }
}