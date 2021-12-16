using Grid;
using ScriptableObjects.Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using DG.Tweening;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask objectsLayerMask;
    [SerializeField] private GameObject cursorFollow;

    [SerializeField] private GridManager gridManager;

    [HideInInspector] public bool isRotating = false;


    private Vector2 _mousePos;
    private Vector3 _mouseWorldPos;
    private Vector2Int _mouseGridPos;
    private WorldInteractable _selectedInteractable;

    public Vector3 mouseWorldPos => _mouseWorldPos;
    public WorldInteractable selectedInteractable => _selectedInteractable;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        cursorFollow.SetActive(!isRotating);
        if (isRotating) return;
        FollowCursor();
        DragObject();
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        _mousePos = context.ReadValue<Vector2>();
    }

    public void OnMouseClick(InputAction.CallbackContext context)
    {
        if (!context.started || isRotating) return;
        // if you currently have an object selected 
        if (_selectedInteractable)
        {
            // attempt to drop the object
            DropObject();
        }
        else
        {
            // else see if u can select an object
            SelectObject();
        }
    }

    public void OnRotateDown(InputAction.CallbackContext context)
    {
        if (!(context.interaction is HoldInteraction)) return;
        isRotating = context.started || context.performed;
    }

    private void FollowCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, groundLayerMask))
        {
            _mouseWorldPos = hit.point;
            cursorFollow.transform.position = _mouseWorldPos;
            _mouseGridPos = gridManager.Grid.GetGridPos(_mouseWorldPos);
        }
    }

    private void DragObject()
    {
        if (_selectedInteractable)
        {
            _mouseWorldPos = gridManager.Grid.GetWorldPos(_mouseGridPos.x, _mouseGridPos.y);

            _selectedInteractable.Drag(_mouseWorldPos);
        }
    }

    private void SelectObject()
    {
        // otherwise if you dont have an object selected
        // see if there is a building where u clicked
        Ray ray = mainCamera.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, objectsLayerMask))
        {
            var hitObject = hit.collider.gameObject;
            var worldInteractable = hitObject.GetComponent<WorldInteractable>();
            if (worldInteractable != null)
            {
                _selectedInteractable = worldInteractable;
                worldInteractable.OnInteract();
                worldInteractable.Select(_mouseWorldPos);
            }
        }
    }

    public void SetSelectedBuilding(WorldInteractable worldInteractable)
    {
        if (_selectedInteractable != null) return;
        _selectedInteractable = worldInteractable;
        worldInteractable.OnInteract();
        worldInteractable.Select(_mouseWorldPos);
    }

    private void DropObject()
    {
        _selectedInteractable.OnInteract();
        
        // check if there is nothing else on its location (check for its size aswell)
        var position = _selectedInteractable.transform.position;
        position = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z));
        var objGridPos = gridManager.Grid.GetGridPos(position);
        var valid = ValidGridLocation(objGridPos, _selectedInteractable.GridItem.ItemSize, _selectedInteractable);

        
        // if there is something set it back to its previous position (save that in a temp var when selected)
        if (!valid)
        {
            _selectedInteractable.transform.position = _selectedInteractable.prevBuildingLoc;
        }
        else
        {
            // if its empty then set the grid locations to the item
            SetGridLocation(objGridPos, _selectedInteractable.GridItem.ItemSize, _selectedInteractable);

            // set the previous values to null
            var prevLocGridPos = gridManager.Grid.GetGridPos(_selectedInteractable.prevBuildingLoc);
            SetGridLocation(prevLocGridPos, _selectedInteractable.GridItem.ItemSize, null);
        }

        _selectedInteractable.DeSelect(_mouseWorldPos);

        _selectedInteractable = null;
    }

    private bool ValidGridLocation(int startingX, int startingY, int gridSizeX, int gridSizeY,WorldInteractable interactable)
    {
        for (var x = startingX; x < startingX + gridSizeX; ++x)
        {
            for (var y = startingY; y < startingY + gridSizeY; ++y)
            {
                if (gridManager.Grid.GetCellValue(x, y) == interactable || gridManager.Grid.GetCellValue(x, y) == null) continue;
                return false;
            }
        }

        return true;
    }

    private bool ValidGridLocation(Vector2Int position, Vector2Int gridSize, WorldInteractable interactable)
    {
        return ValidGridLocation(position.x, position.y, gridSize.x, gridSize.y, interactable);
    }

    private void SetGridLocation(int startingX, int startingY, int gridSizeX, int gridSizeY, WorldInteractable value)
    {
        for (var x = startingX; x < startingX + gridSizeX; ++x)
        {
            for (var y = startingY; y < startingY + gridSizeY; ++y)
            {
                gridManager.Grid.SetCellValue(x, y, value);
            }
        }
    }

    private void SetGridLocation(Vector2Int position, Vector2Int gridSize, WorldInteractable value)
    {
        SetGridLocation(position.x, position.y, gridSize.x, gridSize.y, value);
    }
}