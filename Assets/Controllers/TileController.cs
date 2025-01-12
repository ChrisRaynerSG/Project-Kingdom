using Unity.VisualScripting;
using UnityEngine;

public class TileController : MonoBehaviour {
    [SerializeField] Sprite tileSprite;
    [SerializeField] GameObject tileDetailController;
    public Tile TileData { get; set; }

 public void Initialise(Tile tileData){

    TileData = tileData;
    if(TileData.HasTileDetail){
        // Set up tile detail controller
        GameObject newTileDetail = Instantiate(tileDetailController, new Vector3(tileData.GlobalPosX,tileData.GlobalPosY,0), Quaternion.identity, transform);
        newTileDetail.GetComponent<TileDetailController>().Initialise(tileData.TileDetailData);
    }
    else{ // set up a blank detail game object
        TileDetail tileDetail = new TileDetail(TileDetail.TileDetailType.None, TileData);
        TileData.TileDetailData = tileDetail;
        GameObject newTileDetail = Instantiate(tileDetailController, new Vector3(tileData.GlobalPosX,tileData.GlobalPosY,0), Quaternion.identity, transform);
        newTileDetail.GetComponent<TileDetailController>().Initialise(tileData.TileDetailData);
    
    }
    //GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/VeryBasicTils}"); // Change to load tile type later?
    GetComponent<SpriteRenderer>().sprite = tileSprite;
    switch(TileData.Type){
        case Tile.TileType.Water:
            GetComponent<SpriteRenderer>().material.color = Color.blue;
            break;
        case Tile.TileType.Grass:
            GetComponent<SpriteRenderer>().material.color = Color.green;
            break;
        case Tile.TileType.Dirt:
            GetComponent<SpriteRenderer>().material.color = new Color(0.6f, 0.3f, 0.1f);
            break;
        case Tile.TileType.Stone:
            GetComponent<SpriteRenderer>().material.color = Color.gray;
            break;
     }
     TileData.OnTileTypeChanged += TileTypeChanged;
 }
 public void TileTypeChanged(Tile tile){
    switch(tile.Type){
        case Tile.TileType.Water:
            GetComponent<SpriteRenderer>().material.color = Color.blue;
            break;
        case Tile.TileType.Grass:
            GetComponent<SpriteRenderer>().material.color = Color.green;
            break;
        case Tile.TileType.Dirt:
            GetComponent<SpriteRenderer>().material.color = Color.yellow;
            break;
        case Tile.TileType.Stone:
            GetComponent<SpriteRenderer>().material.color = Color.gray;
            break;
        }
    }
}


