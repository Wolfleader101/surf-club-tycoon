using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

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
        Ray ray = mainCamera.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
        {
            this.transform.position = hit.point;
        }
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        _mousePos = context.ReadValue<Vector2>();
    }
}
