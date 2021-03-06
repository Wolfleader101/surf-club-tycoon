using System;
using Grid;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ScriptableObjects.Managers
{
    [CreateAssetMenu(fileName = "Grid Manager", menuName = "Manager/Grid")]
    public class GridManager : SingletonScriptableObject<GridManager>
    {
        [SerializeField] private int gridWidth = 10;
        [SerializeField] private int gridHeight = 10;
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private Vector3 origin = new Vector3(0, 0, 0);
        [SerializeField] private GameObject ground;
        
        [SerializeField] private bool debugLines = true;
        
        [ShowInInspector]
        private Grid<WorldInteractable> _grid;

        public Grid<WorldInteractable> Grid => _grid;
        
        private void OnEnable()
        {
            _grid ??= new Grid<WorldInteractable>(gridWidth, gridHeight, cellSize, origin);
        }
        
        public void DrawGizmos()
        {
            _grid ??= new Grid<WorldInteractable>(gridWidth, gridHeight, cellSize, origin);
            if (debugLines) _grid.DrawDebugLines();
        }

        [Button("Create Ground Object")]
        private void CreateGround()
        {
           if(ground != null) Destroy(ground);
           
            var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.position = Vector3.zero;
            plane.transform.localScale = new Vector3((float) gridWidth / 10, 1, (float) gridHeight / 10);
            plane.name = "Ground";
            plane.layer = LayerMask.NameToLayer("Ground");
            ground = plane;

        }
    }
}