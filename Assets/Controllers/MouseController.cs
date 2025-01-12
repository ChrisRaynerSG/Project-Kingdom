using System;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class MouseController : MonoBehaviour{

    public GameObject selectionBoxPrefab; // reference to the selection box prefab, will need to use object pooling for this

    public static event Action<Tile> onTileHoveredOver;
    public static event Action<Tile> onTileClicked;
    public static event Action<List<Tile>> onTilesSelected;

    public Tile tileAtStartOfDrag;
    public Tile tileAtEndOfDrag;

    void Update(){
        // if(Input.GetMouseButtonDown(0)){
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
            Tile tile = GetTileAtMousePosition(worldPosition);
            if(tile != null){
                onTileHoveredOver?.Invoke(tile);
            }
            if(Input.GetMouseButtonDown(0)){ // left click down
                onTileClicked?.Invoke(tile);
                tileAtStartOfDrag = tile;
            }
            if(Input.GetMouseButtonUp(0)){ // left click up
                tileAtEndOfDrag = tile;
                List<Tile> tiles = GetTilesFromDrag();
                // foreach(Tile t in tiles){
                //     Debug.Log($"Tile: {t.Type} {t.GlobalPosX},{t.GlobalPosY}");
                // }
                if(tiles.Count > 0){
                    onTilesSelected?.Invoke(tiles);
                }
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

        if(tileAtStartOfDrag.GlobalPosX > tileAtEndOfDrag.GlobalPosX){
            Tile temp = tileAtStartOfDrag;
            tileAtStartOfDrag = tileAtEndOfDrag;
            tileAtEndOfDrag = temp;
        }
        if(tileAtStartOfDrag.GlobalPosY > tileAtEndOfDrag.GlobalPosY){
            Tile temp = tileAtStartOfDrag;
            tileAtStartOfDrag = tileAtEndOfDrag;
            tileAtEndOfDrag = temp;
        }
        for(int x = tileAtStartOfDrag.GlobalPosX; x<= tileAtEndOfDrag.GlobalPosX; x++){
            for(int y = tileAtStartOfDrag.GlobalPosY; y<= tileAtEndOfDrag.GlobalPosY; y++){
                Vector3 mousePosition = new Vector3(x, y, 0);
                Tile tile = GetTileAtMousePosition(mousePosition);
                tiles.Add(tile);
            }
        }
        // how to deal with negative values?
        return tiles; // return an array of tiles
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