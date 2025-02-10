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
        // GameObject newTileDetail = Instantiate(tileDetailController, new Vector3(tileData.GlobalPosX,tileData.GlobalPosY,0), Quaternion.identity, transform);
        // newTileDetail.GetComponent<TileDetailController>().Initialise(tileData.TileDetailData);
        

        GameObject newTileDetail = TileDetailObjectPool.Instance.GetObject(new Vector3(tileData.GlobalPosX, tileData.GlobalPosY, 0), transform);
        

        if(newTileDetail != null){
            newTileDetail.GetComponent<TileDetailController>().ResetData(); // Clear any previous data
            newTileDetail.GetComponent<TileDetailController>().Initialise(tileData.TileDetailData); // Set up new data
            newTileDetail.name = $"{tileData.TileDetailData.Type} tile detail, {tileData.GlobalPosX} {tileData.GlobalPosY}";
            TileData.TileDetailData.TileDetailController = newTileDetail.GetComponent<TileDetailController>();
        }
    }
    else{ // set up a blank detail game object
        TileDetail tileDetail = new TileDetail(TileDetail.TileDetailType.None, TileData);
        TileData.TileDetailData = tileDetail;
        GameObject newTileDetail = TileDetailObjectPool.Instance.GetObject(new Vector3(tileData.GlobalPosX,tileData.GlobalPosY,0), transform);
        newTileDetail.GetComponent<TileDetailController>().ResetData();
        newTileDetail.GetComponent<TileDetailController>().Initialise(tileData.TileDetailData);
        TileData.TileDetailData.TileDetailController = newTileDetail.GetComponent<TileDetailController>();
    
    }
    //GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/VeryBasicTils}"); // Change to load tile type later?
    GetComponent<SpriteRenderer>().sprite = tileSprite;
    switch(TileData.Type){
        case Tile.TileType.Water:
            GetComponent<SpriteRenderer>().material.color = Color.blue;
            break;
        case Tile.TileType.Grass:
            GetComponent<SpriteRenderer>().material.color = Color.white;
            GetComponent<SpriteRenderer>().sprite = SpriteLoader.GetInstance.FloorTileDictionary.TryGetValue("NewGrassTile_0", out Sprite grassTile) ? grassTile : null;
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
            GetComponent<SpriteRenderer>().material.color = Color.white;
            GetComponent<SpriteRenderer>().sprite = SpriteLoader.GetInstance.FloorTileDictionary.TryGetValue("NewGrassTile_0", out Sprite grassTile) ? grassTile : null;
            break;
        case Tile.TileType.Dirt:
            GetComponent<SpriteRenderer>().material.color = Color.yellow;
            break;
        case Tile.TileType.Stone:
            GetComponent<SpriteRenderer>().material.color = Color.gray;
            break;
        }
    }
    public void ResetTile(){
        TileDetailObjectPool.Instance.ReturnObject(TileData.TileDetailData.TileDetailController.gameObject);
        // TileData.OnTileTypeChanged -= TileTypeChanged;
        // TileData.TileDetailData = null;
        // TileData.inventoryItem = null;
        //TileData = null;
    }
}



