using Grid;
using ScriptableObjects.Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

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
        if (context.started && !isRotating)
        {
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

    private void SelectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, objectsLayerMask))
        {
            var hitObject = hit.collider.gameObject;
            var worldInteractable = hitObject.GetComponent<WorldInteractable>();
            if (worldInteractable != null)
            {
                _selectedInteractable = worldInteractable.selected == false ? worldInteractable : null;
                worldInteractable.selected = !worldInteractable.selected;

                worldInteractable.OnInteract();

                if (worldInteractable.selected)
                {
                    worldInteractable.Select(_mouseWorldPos);
                }
                else
                {
                    DropObject(worldInteractable);
                }
            }
        }
    }

    private void DragObject()
    {
        if (_selectedInteractable != null && _selectedInteractable.selected)
        {
            _mouseWorldPos = gridManager.Grid.GetWorldPos(_mouseGridPos.x, _mouseGridPos.y);

            _selectedInteractable.Drag(_mouseWorldPos);
        }
    }

    private void DropObject(WorldInteractable worldInteractable)
    {
        // on drop
        // check if there is nothing else on its location (check for its size aswell)
        var valid = true;
        Vector2Int worldInteractableGridPos =
            gridManager.Grid.GetGridPos(worldInteractable.transform.position);
        Debug.Log(worldInteractableGridPos);
        for (var x = worldInteractableGridPos.x;
            x < worldInteractableGridPos.x + worldInteractable.GridItem.ItemSize.x && valid;
            ++x)
        {
            for (var y = worldInteractableGridPos.y;
                y < worldInteractableGridPos.y + worldInteractable.GridItem.ItemSize.y;
                ++y)
            {
                if (gridManager.Grid.GetCellValue(x, y) == null) continue;
                valid = false;
                break;
            }
        }

        // if there is something set it back to its previous position (save that in a temp var when selected)
        if (!valid)
        {
            Debug.Log("Not Valid");
            worldInteractable.transform.position = worldInteractable.prevBuildingLoc;
        }
        else
        {
            // if its empty then set the grid locations to the item
            SetGridLocation(worldInteractableGridPos, worldInteractable.GridItem.ItemSize, worldInteractable);

            // set the previous values to null
            var prevLocGridPos = gridManager.Grid.GetGridPos(worldInteractable.prevBuildingLoc);
            SetGridLocation(prevLocGridPos, worldInteractable.GridItem.ItemSize, null);
        }
                    
        worldInteractable.DeSelect(_mouseWorldPos);
    }

    private void SetGridLocation(int startingX, int startingY, int gridSizeX, int gridSizeY, WorldInteractable value)
    {
        for (var x = startingX; x < startingX + gridSizeX; ++x)
        {
            for (var y = startingY; y < startingY + gridSizeY;
                ++y)
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