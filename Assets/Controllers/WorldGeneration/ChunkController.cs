using System.Collections.Generic;
using UnityEngine;
public class ChunkController : MonoBehaviour{
    [SerializeField] GameObject tilePrefab;
    public Chunk ChunkData { get; private set; }

    private List<GameObject> activeTiles = new List<GameObject>();
    
    public void Initialise(Chunk chunkData){
        ChunkData = chunkData;
        foreach(Tile tile in ChunkData.Tiles){
            // GameObject newTile = Instantiate(tilePrefab, new Vector3(tile.GlobalPosX, tile.GlobalPosY, 0), Quaternion.identity, transform);
            // newTile.GetComponent<TileController>().Initialise(tile);
            // newTile.name = $"{tile.Type} tile {tile.GlobalPosX} {tile.GlobalPosY}";
            GameObject newTile = TileObjectPool.Instance.GetObject(new Vector3(tile.GlobalPosX, tile.GlobalPosY, 0), transform);
            if(newTile != null){
                newTile.GetComponent<TileController>().Initialise(tile);
                newTile.name = $"{tile.Type} tile {tile.GlobalPosX} {tile.GlobalPosY}";
                activeTiles.Add(newTile);
            }
        }
    }

    public void UnloadChunk(){
        foreach(GameObject tile in activeTiles){
            // deal with tile detail controller as well
            // tile.GetComponentInChildren<TileDetailController>().gameObject.SetActive(false);
            // TileDetailObjectPool.Instance.ReturnObject(tile.GetComponentInChildren<TileDetailController>().gameObject);


            tile.SetActive(false);
            TileObjectPool.Instance.ReturnObject(tile);

        }
        activeTiles.Clear();
    }

    public void ResetData(){
        // ChunkData = null;
    }
}