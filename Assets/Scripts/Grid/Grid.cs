using UnityEngine;

namespace Grid
{
    public class Grid<T>
    {
        private int _width;
        private int _height;
        private float _cellSize;
        private Vector3 _origin;
        private T[,] _gridArray;

        public Grid(int width, int height, float cellSize, Vector3 origin, bool debugOnly = false)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _origin = origin;
            _gridArray = new T[width, height];

            for (int x = 0; x < _gridArray.GetLength(0); ++x)
            {
                for (int y = 0; y < _gridArray.GetLength(1); ++y)
                {
                    // if (debugOnly)
                    // {
                    //     Gizmos.color = Color.cyan;
                    //     Gizmos.DrawLine(GetWorldPos(x, y), GetWorldPos(x, y + 1));
                    //     Gizmos.DrawLine(GetWorldPos(x, y), GetWorldPos(x + 1, y));
                    // }
                }
            }
            

            // if (debugOnly)
            // {
            //     Gizmos.DrawLine(GetWorldPos(0, height), GetWorldPos(width, height));
            //     Gizmos.DrawLine(GetWorldPos(width, 0), GetWorldPos(width, height));
            // }
        }

        public void DrawDebugLines()
        {
            for (int x = 0; x < _gridArray.GetLength(0); ++x)
            {
                for (int y = 0; y < _gridArray.GetLength(1); ++y)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawLine(GetWorldPos(x, y), GetWorldPos(x, y + 1));
                    Gizmos.DrawLine(GetWorldPos(x, y), GetWorldPos(x + 1, y));
                }
            }

            Gizmos.DrawLine(GetWorldPos(0, _height), GetWorldPos(_width, _height));
            Gizmos.DrawLine(GetWorldPos(_width, 0), GetWorldPos(_width, _height));
        }

        public Vector3 GetWorldPos(int x, int y)
        {
            return new Vector3(x, 0, y) * _cellSize + _origin;
        }

        public Vector2Int GetGridPos(Vector3 worldPos)
        {
            var x = Mathf.FloorToInt((worldPos - _origin).x / _cellSize);
            var y = Mathf.FloorToInt((worldPos - _origin).z / _cellSize);
            return new Vector2Int(x, y);
        }

        public void SetCellValue(int x, int y, T value)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height) _gridArray[x, y] = value;
        }

        public void SetCellValue(Vector3 worldPos, T value)
        {
            Vector2Int gridPos = GetGridPos(worldPos);
            SetCellValue(gridPos.x, gridPos.y, value);
        }

        public T GetCellValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height) return _gridArray[x, y];

            return default(T);
        }

        public T GetCellValue(Vector3 worldPos)
        {
            Vector2Int gridPos = GetGridPos(worldPos);
            return GetCellValue(gridPos.x, gridPos.y);
        }
    }
}