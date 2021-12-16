using System;
using Attributes;
using UnityEngine;

namespace ScriptableObjects.GridItems.Interactables
{

    [CreateAssetMenu(fileName = "Building", menuName = "Interactable/Building")]
    public class InteractableBuilding : GridItem
    {
        [SerializeField] private Transform prefab;
        public Transform Prefab => prefab;
        
        [SerializeField, FormattedPrice] private float buildingPrice;
        
        public float BuildingPrice => buildingPrice;
    }
}