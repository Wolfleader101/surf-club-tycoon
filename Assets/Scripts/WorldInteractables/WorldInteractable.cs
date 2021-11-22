using ScriptableObjects.GridItems;
using ScriptableObjects.GridItems.Interactables;
using UnityEngine;

public class WorldInteractable : MonoBehaviour
{
    [SerializeField] private GridItem gridItem;
    [SerializeField] private Renderer _renderer;

    [HideInInspector] public bool selected = false;

    private bool _highLight = false;
    private Color _prevColor;

    private void Start()
    {
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

        if (selected)
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
        Debug.Log($"Pickup Pos: {mousePos}");
    }

    public void DeSelect(Vector3 mousePos)
    {
        Debug.Log($"Drop Pos: {mousePos}");
    }
}