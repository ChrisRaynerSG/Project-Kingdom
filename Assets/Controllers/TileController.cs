using UnityEngine;

public class TileController : MonoBehaviour {
    public Tile TileData { get; set; }

 public void Initialise(Tile tileData){
    TileData = tileData;
    switch(TileData.Type){
        case Tile.TileType.Water:
            GetComponent<Renderer>().material.color = Color.blue;
            break;
        case Tile.TileType.Grass:
            GetComponent<Renderer>().material.color = Color.green;
            break;
        case Tile.TileType.Dirt:
            GetComponent<Renderer>().material.color = Color.yellow;
            break;
        case Tile.TileType.Stone:
            GetComponent<Renderer>().material.color = Color.gray;
            break;
     }
 }
}


