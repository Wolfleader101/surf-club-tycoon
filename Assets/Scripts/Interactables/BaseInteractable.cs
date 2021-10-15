using UnityEngine;

namespace Interactables
{
    //[CreateAssetMenu(fileName = "FILENAME", menuName = "Interactables", order = 0)]
    public abstract class BaseInteractable : ScriptableObject, IInteractable
    {
        public void OnInteract()
        {
            throw new System.NotImplementedException();
        }
    }
}