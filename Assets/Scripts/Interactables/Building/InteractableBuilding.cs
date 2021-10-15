using UnityEngine;

namespace Interactables.Building
{
    [CreateAssetMenu(fileName = "Building", menuName = "Interactable/Building", order = 0)]
    public class InteractableBuilding : BaseInteractable
    {
        public override void OnInteract()
        {
            Debug.Log($"Building Name: {name}");
        }
    }
}