using System;
using System.Collections;
using System.Collections.Generic;
using Interactables;
using Interactables.Building;
using UnityEngine;

public class WorldInteractable : MonoBehaviour
{
    [SerializeField] private BaseInteractable interactable;

    [HideInInspector] public bool selected = false;

    private bool _highLight = false;
    private Color _prevColor;

    private void Start()
    {
        _prevColor = gameObject.GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        gameObject.GetComponent<Renderer>().material.color = _highLight ? Color.red : _prevColor;
        // if (selected)
        // {
        //     Drag();
        // }
    }

    public void OnInteract()
    {
        _highLight = !_highLight;

        if (selected)
        {
            Debug.Log($"Interactable Name: {interactable.InteractableName}");
            if (interactable is InteractableBuilding building)
            {
                Debug.Log($"Building Price: {building.BuildingPrice}");
            }
        }
    }

    public void Drag(Vector3 mousePos)
    {
        gameObject.transform.position = mousePos;
    }

    public void PickUp(Vector3 mousePos)
    {
        Debug.Log($"Pickup Pos: {mousePos}");
    }

    public void Drop(Vector3 mousePos)
    {
        Debug.Log($"Drop Pos: {mousePos}");
    }
}