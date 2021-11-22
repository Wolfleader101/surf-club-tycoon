using System;
using ScriptableObjects.Managers;
using UnityEngine;

namespace Grid
{
    public class GridManagerRef : MonoBehaviour
    {
        [SerializeField] private GridManager manager;

        public GridManager Manager => manager;
        private void OnDrawGizmos()
        {
            manager.DrawGizmos();
        }
    }
}