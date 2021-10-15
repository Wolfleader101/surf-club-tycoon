using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class FreeCam : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 2f;
    [SerializeField] private MouseManager inputManager;

    private float _mouseX;
    private float _mouseY;

    private float _xRotation;
    private float _yRotation;

    private bool _canRotate = false;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (_canRotate)
        {
            Cursor.lockState = CursorLockMode.Locked;
            _xRotation -= _mouseY;
            _yRotation -= _mouseX;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            _yRotation = Mathf.Clamp(_yRotation, -180f, 180f);

            transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (_canRotate)
        {
            var dir = context.ReadValue<Vector2>();
            _mouseX = -(dir.x * rotateSpeed * Time.deltaTime);
            _mouseY = dir.y * rotateSpeed * Time.deltaTime;
        }
    }

    public void OnRotateDown(InputAction.CallbackContext context)
    {
        if (!(context.interaction is HoldInteraction)) return;
        _canRotate = context.started || context.performed;
        inputManager.isRotating = _canRotate;
    }
}