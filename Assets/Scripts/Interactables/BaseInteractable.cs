using UnityEngine;

namespace Interactables
{
    //[CreateAssetMenu(fileName = "FILENAME", menuName = "Interactables", order = 0)]
    public abstract class BaseInteractable : ScriptableObject
    {
        [SerializeField] protected GameObject gameObject;
        public abstract void OnInteract();
    }
}