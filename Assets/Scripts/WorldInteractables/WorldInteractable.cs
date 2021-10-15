using System;
using System.Collections;
using System.Collections.Generic;
using Interactables;
using UnityEngine;

public class WorldInteractable : MonoBehaviour
{
    [SerializeField] private BaseInteractable interactable;

    [HideInInspector]public bool selected = false;
    
    private bool _highLight = false;
    private Color _prevColor;

    private void Start()
    {
        _prevColor = gameObject.GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        gameObject.GetComponent<Renderer>().material.color = _highLight ? Color.red : _prevColor;
    }

    public void OnInteract()
    {
        interactable.OnInteract();
        _highLight = !_highLight;
    }

    public void Drag(Vector3 mousePos)
    {
        gameObject.transform.position = mousePos;
    }
}
