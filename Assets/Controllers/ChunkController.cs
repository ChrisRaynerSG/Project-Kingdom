using UnityEngine;
public class ChunkController : MonoBehaviour{
    [SerializeField] GameObject tilePrefab;
    public Chunk ChunkData { get; private set; }
    
    public void Initialise(Chunk chunkData){
        ChunkData = chunkData;
        foreach(Tile tile in ChunkData.Tiles){
            GameObject newTile = Instantiate(tilePrefab, new Vector3(tile.GlobalPosX, tile.GlobalPosY, 0), Quaternion.identity, transform);
            newTile.GetComponent<TileController>().Initialise(tile);
            newTile.name = $"{tile.Type} tile {tile.GlobalPosX} {tile.GlobalPosY}";
            
        }
    }
}