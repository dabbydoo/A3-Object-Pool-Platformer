using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BuildingCreator : Singleton<BuildingCreator>
{
    [SerializeField] Tilemap previewMap, levelMap;
    PlayerAction playerAction;

    TileBase tileBase;
    BuildingObjectBase selectedObj;

    Camera cam;

    Vector2 mousePos;
    Vector3Int currentGridPos;
    Vector3Int previousGridPos;


    protected override void Awake()
    {
        base.Awake();
        playerAction = new PlayerAction();
        cam = Camera.main;
    }

    private void OnEnable()
    {
        playerAction.Enable();

        playerAction.Gameplay.MousePosition.performed += OnMouseMove;
        playerAction.Gameplay.MouseLeftClick.performed += OnLeftClick;
        playerAction.Gameplay.MouseRightClick.performed += OnRightClick;
    }

    private void OnDisable()
    {
        playerAction.Disable();

        playerAction.Gameplay.MousePosition.performed -= OnMouseMove;
        playerAction.Gameplay.MouseLeftClick.performed -= OnLeftClick;
        playerAction.Gameplay.MouseRightClick.performed -= OnRightClick;
    }

    private BuildingObjectBase SelectedObj
    {
        set
        {
            selectedObj = value;

            tileBase = selectedObj != null ? selectedObj.TileBase : null;

            UpdatePreview();
        }
    }

    private void Update()
    {
        //Show preview on selected obj
        if (selectedObj != null)
        {
            Vector3 pos = cam.ScreenToWorldPoint(mousePos);
            Vector3Int gridPos = previewMap.WorldToCell(pos);

            if (gridPos != currentGridPos)
            {
                previousGridPos = currentGridPos;
                currentGridPos = gridPos;

                UpdatePreview();
            }
        }
    }

    private void OnMouseMove (InputAction.CallbackContext ctx)
    {
        mousePos = ctx.ReadValue<Vector2>();
    }

    private void OnLeftClick(InputAction.CallbackContext ctx)
    {
        if (selectedObj != null && !EventSystem.current.IsPointerOverGameObject())
        {
            DrawHandle();
        }
    }
    private void OnRightClick(InputAction.CallbackContext ctx)
    {
        SelectedObj = null;
    }

    public void ObjectSelected (BuildingObjectBase obj)
    {
        SelectedObj = obj;
    }

    private void UpdatePreview()
    {
        //Remove old existing tile
        previewMap.SetTile(previousGridPos, null);
        //Set current tile to currrent mouse position
        previewMap.SetTile(currentGridPos, tileBase);
    }

    private void DrawHandle()
    {
        DrawObj();
    }

    private void DrawObj()
    {
        levelMap.SetTile(currentGridPos, tileBase);
    }
}
