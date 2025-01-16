using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MouseController : MonoBehaviour{

    public GameObject selectionBoxPrefab; // reference to the selection box prefab, will need to use object pooling for this

    public static event Action<Tile> onTileHoveredOver;
    public static event Action<Tile> onTileClicked;
    public static event Action<List<Tile>> onTilesSelected;

    public Tile tileAtStartOfDrag;
    public Tile tileAtEndOfDrag;

    private bool isDragging = false;

    private Dictionary<Tile, GameObject> activeSelectionBoxes = new Dictionary<Tile, GameObject>();

    private ObjectPool<GameObject> selectionBoxPool;

    void Start(){
        // BuildController.onCancel += cancel;
    }

    // currently can build ++ and -- but not -+ or +- as the tiles are not being selected correctly? why?


    void Update()
    {
        UpdateCursorSelection();
        // if(Input.GetMouseButtonDown(0)){
        //Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Tile tile = GetTileAtMousePosition(worldPosition);
        if (tile != null)
        {
            onTileHoveredOver?.Invoke(tile);
        }
        if (Input.GetMouseButtonDown(0))
        { // left click down
            onTileClicked?.Invoke(tile);
            tileAtStartOfDrag = tile;
            isDragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        { // left click up
            tileAtEndOfDrag = tile;
            List<Tile> tiles = GetTilesFromDrag();
            // foreach(Tile t in tiles){
            //     Debug.Log($"Tile: {t.Type} {t.GlobalPosX},{t.GlobalPosY}");
            // }
            if (tiles.Count > 0)
            {
                onTilesSelected?.Invoke(tiles);
            }
            isDragging = false;
        }
    }

    private void UpdateCursorSelection()
    {
        List<Tile> tiles = new List<Tile>();
        if (BuildController.isBuildingWall || BuildController.isBulldozing)
        {
            
            if(isDragging){
                tiles = GetTilesFromDrag();
            }

            Color color = new Color();
            if(BuildController.isBuildingWall){
                color = new Color(0, 0.4f, 0, 0.5f);
            }
            else if(BuildController.isBulldozing){
                color = new Color(0.4f, 0, 0, 0.5f);
            }
            ClearSelectionBoxes();

            if(tiles.Count>0 && isDragging /* shouldnt need this but just in case */){
                // we are currently dragging and want to display all the tiles that are being dragged over
                // first we want to remove any selection boxes that are not in the current drag selection
                HashSet<Tile> currentTiles = new HashSet<Tile>(tiles);
                List<Tile> tilesToRemove = new List<Tile>();
                foreach (Tile t in activeSelectionBoxes.Keys)
                {
                    if (!currentTiles.Contains(t))
                    {
                        Destroy(activeSelectionBoxes[t]); // Destroy the GameObject
                        tilesToRemove.Add(t);            // Mark the tile for removal
                    }
                }
                foreach (Tile t in tilesToRemove)
                {
                    activeSelectionBoxes.Remove(t);
                }
                foreach(Tile t in tiles)
                {
                    CreateSelectionBoxComponent(color, t);
                }
                // clear selection boxes again here?
                ClearSelectionBoxes(); 
            }
            else{
                // we are not dragging so we only want to display the selection box for the tile the mouse is currently over
                CreateSelectionBoxComponent(color, GetTileAtMousePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            }
        }
        else
        {
            ClearSelectionBoxes();
        }
    }

    private void CreateSelectionBoxComponent(Color color, Tile t)
    {
        GameObject selectionBox = Instantiate(selectionBoxPrefab, new Vector3(t.GlobalPosX, t.GlobalPosY, 0), Quaternion.identity);
        SpriteRenderer sr = selectionBox.GetComponent<SpriteRenderer>();
        sr.material.color = color;
        sr.sortingLayerName = "Selection";
        if (activeSelectionBoxes.ContainsKey(t))
        {
            Destroy(activeSelectionBoxes[t]);
            activeSelectionBoxes.Remove(t);
        }
        activeSelectionBoxes.Add(t, selectionBox);
    }

    private void ClearSelectionBoxes()
    {
        if (activeSelectionBoxes.Count > 0 && !isDragging)
        {
            foreach (GameObject selBox in activeSelectionBoxes.Values)
            {
                Destroy(selBox);
            }
            activeSelectionBoxes.Clear();
        }
    }

    public Tile GetTileAtMousePosition(Vector3 worldPosition){
        Vector2Int chunkPosition = new Vector2Int(Mathf.FloorToInt(worldPosition.x / World.chunkSize), Mathf.FloorToInt(worldPosition.y / World.chunkSize));
        Vector2Int tilePosition = new Vector2Int(Mathf.FloorToInt(worldPosition.x % World.chunkSize), Mathf.FloorToInt(worldPosition.y % World.chunkSize));

        if(tilePosition.x < 0){
            tilePosition.x = World.chunkSize + tilePosition.x;
        }
        if(tilePosition.y < 0){
            tilePosition.y = World.chunkSize + tilePosition.y;
        }
        Chunk chunk = World.GetInstance().GetChunkFromCoordinates(chunkPosition);
        if(chunk != null){
            // Debug.Log($"{chunk.Tiles[tilePosition.x, tilePosition.y].Type}");
            return chunk.Tiles[tilePosition.x, tilePosition.y];
        }
        else return null;
    }

    // need to make a click and drag function to select multiple tiles
    public List<Tile> GetTilesFromDrag(){
        List<Tile> tiles = new List<Tile>();
        //local variable for end of drag as might not be set if coming from selection box method - need to maybe remove the field
        Tile tileAtEndOfDrag = GetTileAtMousePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        int startX = Mathf.Min(tileAtStartOfDrag.GlobalPosX, tileAtEndOfDrag.GlobalPosX);
        int endX = Mathf.Max(tileAtStartOfDrag.GlobalPosX, tileAtEndOfDrag.GlobalPosX);
        int startY = Mathf.Min(tileAtStartOfDrag.GlobalPosY, tileAtEndOfDrag.GlobalPosY);
        int endY = Mathf.Max(tileAtStartOfDrag.GlobalPosY, tileAtEndOfDrag.GlobalPosY);
        
        for (int x = startX; x <= endX; x++)
    {
        for (int y = startY; y <= endY; y++)
        {
            Vector3 mousePosition = new Vector3(x, y, 0);
            Tile tile = GetTileAtMousePosition(mousePosition);
            if (tile != null) // Ensure tile exists
            {
                tiles.Add(tile);
            }
        }
    }
        return tiles; // return a list of tiles
    }

    public static void OnTileHoveredOver(Tile tile){
        onTileHoveredOver?.Invoke(tile);
    }
    public static void OnTileClicked(Tile tile){
        onTileClicked?.Invoke(tile);
    }

    public static void OnTilesSelected(List<Tile> tiles){
        onTilesSelected?.Invoke(tiles);
    }
}