using UnityEngine;

namespace Interactables.Building
{
    [CreateAssetMenu(fileName = "Building", menuName = "Interactable/Building")]
    public class InteractableBuilding : BaseInteractable
    {
        [SerializeField] private Transform prefab;
        [SerializeField] private string buildingName;
        [SerializeField] private float buildingPrice;
        public float BuildingPrice => buildingPrice;
    }
}