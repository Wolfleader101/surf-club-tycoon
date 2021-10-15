using UnityEngine;

namespace Interactables
{
    //[CreateAssetMenu(fileName = "FILENAME", menuName = "Interactables", order = 0)]
    public abstract class BaseInteractable : ScriptableObject
    {
        [SerializeField] protected string interactableName;
        public string InteractableName => interactableName;
        public abstract void OnInteract();

    }
}