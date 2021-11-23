using Grid;
using ScriptableObjects.GridItems;
using ScriptableObjects.GridItems.Interactables;
using ScriptableObjects.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldInteractable : MonoBehaviour
{
    [SerializeField] private GridItem gridItem;
    [SerializeField] private Renderer _renderer;

    private bool _selected = false;
    [HideInInspector] public Vector3 prevBuildingLoc;

    public GridItem GridItem => gridItem;

    private bool _highLight = false;
    private Color _prevColor;

    private GridManager _gridManager;

    private void Start()
    {
        prevBuildingLoc = transform.position;

        var gridManagerRef = FindObjectOfType<GridManagerRef>();
        _gridManager = gridManagerRef.Manager;

        Vector2Int worldInteractableGridPos = _gridManager.Grid.GetGridPos(transform.position);

        // if its empty then place it and set the grid locations to the item
        for (var x = worldInteractableGridPos.x; x < worldInteractableGridPos.x + gridItem.ItemSize.x; ++x)
        {
            for (var y = worldInteractableGridPos.y; y < worldInteractableGridPos.y + gridItem.ItemSize.y; ++y)
            {
                _gridManager.Grid.SetCellValue(x, y, this);
            }
        }

        if (_renderer == null)
        {
            _prevColor = gameObject.GetComponentInChildren<Renderer>().material.color;
            return;
        }

        _prevColor = _renderer.material.color;
    }

    private void Update()
    {
        if (_renderer == null)
        {
            gameObject.GetComponentInChildren<Renderer>().material.color = _highLight ? Color.red : _prevColor;
            return;
        }

        _renderer.material.color = _highLight ? Color.red : _prevColor;
    }

    public void OnInteract()
    {
        _highLight = !_highLight;
        _selected = !_selected;

        if (_selected)
        {
            Debug.Log($"Item Name: {gridItem.ItemName}");
            if (gridItem is InteractableBuilding building)
            {
                Debug.Log($"Building Price: {building.BuildingPrice}");
            }
        }

    }

    public void Drag(Vector3 mousePos)
    {
        transform.position = mousePos;
    }

    public void Select(Vector3 mousePos)
    {
        prevBuildingLoc = transform.position;
        //Debug.Log($"Pickup Pos: {mousePos}");
    }

    public void DeSelect(Vector3 mousePos)
    {
        //Debug.Log($"Drop Pos: {mousePos}");
    }
}