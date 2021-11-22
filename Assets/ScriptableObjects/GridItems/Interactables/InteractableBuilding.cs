using System;
using Attributes;
using UnityEngine;

namespace ScriptableObjects.GridItems.Interactables
{

    [CreateAssetMenu(fileName = "Building", menuName = "Interactable/Building")]
    public class InteractableBuilding : GridItem
    {
        [SerializeField] private Transform prefab;
        
        [FormattedPrice]
        [SerializeField] private float buildingPrice;
        
        
        public float BuildingPrice => buildingPrice;
    }
}