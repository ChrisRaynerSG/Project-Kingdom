using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class MouseController : MonoBehaviour{

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            Vector3 mousePosition = Input.mousePosition;
            Tile tile = GetTileAtMousePosition(mousePosition);
            if(tile != null){
                if(tile.HasTileDetail){
                    Debug.Log($"Tile at mouse position: {tile.Type} {tile.GlobalPosX},{tile.GlobalPosY} || Tile Detail: {tile.TileDetailData.Type}");
                }
                else{
                    Debug.Log($"Tile at mouse position: {tile.Type} {tile.GlobalPosX},{tile.GlobalPosY}");
                }
            }
        }
    }


    public Tile GetTileAtMousePosition(Vector3 mousePosition){
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
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
            Debug.Log($"{chunk.Tiles[tilePosition.x, tilePosition.y].Type}");
            return chunk.Tiles[tilePosition.x, tilePosition.y];
        }
        else return null;
    }
}