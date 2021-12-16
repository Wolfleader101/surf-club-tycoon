using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.GridItems;
using ScriptableObjects.GridItems.Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] private MouseManager mouseManager;
    [SerializeField] private InteractableBuilding building;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        // instantiate a ghost object of the building
        // make the mouse manager drag it around etc

        if (mouseManager.selectedInteractable != null) return;
        var spawnedBuilding = Instantiate(building.Prefab, mouseManager.mouseWorldPos, Quaternion.identity).gameObject;
        Debug.Log(spawnedBuilding);
        var worldInteractable = spawnedBuilding.GetComponent<WorldInteractable>();
        mouseManager.SetSelectedBuilding(worldInteractable);


        // on ghost object drop
        // try and build the building (e.g checking for space etc)
        //get the current economies money
        //if they have enough money
        // purchase and place the building
    }
}
