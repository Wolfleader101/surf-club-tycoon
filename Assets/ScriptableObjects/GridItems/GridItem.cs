using UnityEngine;

namespace ScriptableObjects.GridItems
    {
        public abstract class GridItem : ScriptableObject
        {
            [SerializeField] protected string itemName;
            public string ItemName => itemName;
            
            public Vector2Int gridPos { get; set; }
        }
    }
