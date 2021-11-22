using UnityEngine;

namespace ScriptableObjects.GridItems
    {
        public abstract class GridItem : ScriptableObject
        {
            [SerializeField] protected string itemName;
            public string ItemName => itemName;
            
            [SerializeField] protected Vector2Int itemSize;
            public Vector2Int ItemSize => itemSize;
            
            public Vector2Int gridPos { get; set; }
        }
    }
