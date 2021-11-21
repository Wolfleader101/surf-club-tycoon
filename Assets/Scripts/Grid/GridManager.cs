using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Grid
{
    [ExecuteInEditMode]
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private int gridWidth = 10;
        [SerializeField] private int gridHeight = 10;
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private Vector3 origin = new Vector3(0, 0, 0);

        [SerializeField] private bool debugLines = true;

        private Grid<GridItem> _grid;

        public Grid<GridItem> Grid => _grid;

        public int GridWidth => gridWidth;
        public int GridHeight => gridHeight;
        public float CellSize => cellSize;

        private void Awake()
        {
            _grid = new Grid<GridItem>(gridWidth, gridHeight, cellSize, origin);
        }

        private void OnDrawGizmos()
        {
            _grid ??= new Grid<GridItem>(gridWidth, gridHeight, cellSize, origin);
            if (debugLines) _grid.DrawDebugLines();
        }
    }
}