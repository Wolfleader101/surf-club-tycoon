using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private int gridWidth = 10;
        [SerializeField] private int gridHeight = 10;
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private Vector3 origin = new Vector3(0, 0, 0);
        [SerializeField] private GameObject ground;
        
        [SerializeField] private bool debugLines = true;

        private Grid<GridItem> _grid;

        public Grid<GridItem> Grid => _grid;

        public int GridWidth => gridWidth;
        public int GridHeight => gridHeight;
        public float CellSize => cellSize;

        private void Awake()
        {
            _grid = new Grid<GridItem>(gridWidth, gridHeight, cellSize, origin);
            if(ground == null) CreateGround();
        }

        private void OnDrawGizmos()
        {
            _grid ??= new Grid<GridItem>(gridWidth, gridHeight, cellSize, origin);
            if (debugLines) _grid.DrawDebugLines();
        }

        private void CreateGround()
        {
            var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.position = Vector3.zero;
            plane.transform.localScale = new Vector3((float) gridWidth / 10, 1, (float) gridHeight / 10);
            plane.name = "Ground";
            plane.layer = LayerMask.NameToLayer("Ground");
        }
    }
}