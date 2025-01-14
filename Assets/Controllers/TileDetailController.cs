using System.Collections.Generic;
using UnityEngine;
public class TileDetailController : MonoBehaviour{

    Sprite[] basicTiles; 
    Dictionary<string,Sprite> wallTiles;// needs changing later when we have more tile types
    [SerializeField] Sprite tileDetailSprite;
    SpriteRenderer sr;
    public TileDetail TileDetailData { get; set; }
    public void Initialise(TileDetail tileDetailData){

        basicTiles = Resources.LoadAll<Sprite>("Sprites/VeryBasicTiles");
        Sprite[] wallTilesArray = Resources.LoadAll<Sprite>("Sprites/Walls/WallTilesAdvanced");

        wallTiles = new Dictionary<string, Sprite>();

        foreach (Sprite sprite in wallTilesArray)
        {
            wallTiles.Add(sprite.name, sprite);
        }

        TileDetailData = tileDetailData;
        sr = GetComponent<SpriteRenderer>();
        if(TileDetailData.Type == TileDetail.TileDetailType.Bush){
            sr.material.color = new Color(0f,0.6f,0f);
            sr.sprite = Resources.Load<Sprite>("Sprites/Bushes32x32_06");
        }
        else if(TileDetailData.Type == TileDetail.TileDetailType.Tree){
            sr.material.color = new Color(0f,0.3f,0f);
            sr.sprite = basicTiles[2];

        }
        else if(TileDetailData.Type == TileDetail.TileDetailType.Rock){
            sr.material.color = Color.gray;
            sr.sprite = wallTiles.TryGetValue("WoodWallSet_", out Sprite rockSprite) ? rockSprite : null;
        }
        else if(TileDetailData.Type == TileDetail.TileDetailType.Wall){
            if (wallTiles.TryGetValue("WoodWallSet_", out Sprite wallSprite))
            {
                sr.sprite = wallSprite;
            }
        }
        else if(TileDetailData.Type == TileDetail.TileDetailType.None){
            sr.sprite = null;
        }
        TileDetailData.OnTileDetailTypeChanged += TileDetailTypeChanged;
        sr.sortingLayerName = "TileDetail";
    }
    private void OnDisable(){
        if(TileDetailData != null){
            TileDetailData.OnTileDetailTypeChanged -= TileDetailTypeChanged;
        }
    }

    private void TileDetailTypeChanged(TileDetail tileDetail){
        //Debug.Log("Tile detail type changed");

        switch(tileDetail.Type){
            case TileDetail.TileDetailType.Bush:
                sr.material.color = Color.green;
                sr.sprite = Resources.Load<Sprite>("Sprites/Bushes32x32_06");
                break;
            case TileDetail.TileDetailType.Tree:
                sr.material.color = Color.green;
                sr.sprite = basicTiles[2];
                break;
            case TileDetail.TileDetailType.Rock:
                sr.material.color = Color.gray;
                break;
            case TileDetail.TileDetailType.None:
                sr.sprite = null;
                break;
            case TileDetail.TileDetailType.Wall:
                string wallSpriteName = "WallTilesAdvanced_";
                TileDetail[] adjacentTileDetails = GetAdjacentTileDetails();
                for(int i = 0; i<8; i++){
                    //Debug.Log($"{adjacentTileDetails[i].TileData.GlobalPosX},{adjacentTileDetails[i].TileData.GlobalPosY}");
                    // 0 2 4 6 are the corner pieces, 1 3 5 7 are cardinal directions, can ignore the co
                    if(adjacentTileDetails[i] != null){
                       // Debug.Log($"Adjacent tile detail type: {adjacentTileDetails[i].Type} TilePos: {adjacentTileDetails[i].TileData.GlobalPosX},{adjacentTileDetails[i].TileData.GlobalPosY}");
                        if(adjacentTileDetails[i].Type == TileDetail.TileDetailType.Wall){
                            wallSpriteName +=i;
                            //Debug.Log("Wall sprite name: "+wallSpriteName);
                        }
                    }
                }
                wallSpriteName = CleanWallSpriteName(wallSpriteName);
                //47 combinations of wall sprites how to get the right one?
                sr.sprite = wallTiles.TryGetValue(wallSpriteName, out Sprite wallSprite) ? wallSprite : null;
                tileDetail.name = wallSpriteName;
                break;
        }
    }
    private TileDetail[] GetAdjacentTileDetails(){// should this return a list of tile details or a list of tiles? should it be in the tile controller?
        // 8 tiles around the current tile, how to get these?
        int thisTileX = TileDetailData.TileData.GlobalPosX;
        Debug.Log($"This tile x: {thisTileX}");
        int thisTileY = TileDetailData.TileData.GlobalPosY;
        Debug.Log($"This tile y: {thisTileY}");
        TileDetail[] adjacentTileDetails = new TileDetail[8];
        // get the tile details of the 8 tiles around the current tile
        // if the tile is null, set the tile detail to null
        // if the tile is not null, set the tile detail to the tile detail of the tile
        // return the list of tile details
        // start with top left, go clockwise -1,1 0,1 1,1 1,0 1,-1 0,-1 -1,-1 -1,0
        for(int i = 0; i<8; i++){
            int x;int y;
            switch(i){
                case 0:
                    x = -1;y = 1;break;
                case 1:
                    x = 0;y = 1;break;
                case 2:
                    x = 1;y = 1;break;
                case 3:
                    x = 1;y = 0;break;
                case 4:
                    x = 1;y = -1;break;
                case 5:
                    x = 0;y = -1;break;
                case 6:
                    x = -1;y = -1;break;
                case 7:
                    x = -1;y = 0;break;
                default:
                    x = 0;y = 0;break; // should never get here but compiler needs it
            }
            Vector2Int tilePosition = new Vector2Int(thisTileX+x, thisTileY+y);
            Tile tile = WorldController.Instance.GetTileFromGlobalPosition(tilePosition);
            if(tile != null){
                adjacentTileDetails[i] = tile.TileDetailData;
            }
            else{
                adjacentTileDetails[i] = null;
            }
        }
        return adjacentTileDetails;
    }

    private string CleanWallSpriteName(string wallSpriteName){
        //string[] validWallSpriteNames = new string[]{"WallTilesAdvanced_0","WallTilesAdvanced_02","WallTilesAdvanced_024","WallTilesAdvanced_0246"}; // need to fill with all valid combinations...

        //standalone walls
        if(wallSpriteName == "WallTilesAdvanced_0" || wallSpriteName == "WallTilesAdvanced_02" || wallSpriteName == "WallTilesAdvanced_024" || wallSpriteName == "WallTilesAdvanced_0246"
        || wallSpriteName == "WallTilesAdvanced_026" || wallSpriteName == "WallTilesAdvanced_04" || wallSpriteName == "WallTilesAdvanced_046" || wallSpriteName == "WallTilesAdvanced_06"
        || wallSpriteName == "WallTilesAdvanced_2" || wallSpriteName == "WallTilesAdvanced_24" || wallSpriteName == "WallTilesAdvanced_246" || wallSpriteName == "WallTilesAdvanced_26"
        || wallSpriteName == "WallTilesAdvanced_4" || wallSpriteName == "WallTilesAdvanced_46" || wallSpriteName == "WallTilesAdvanced_6"){
            return "WallTilesAdvanced_"; // standalone wall
        }

        // walls facing a single direction
        if(wallSpriteName == "WallTilesAdvanced_456" || wallSpriteName == "WallTilesAdvanced_45" || wallSpriteName == "WallTilesAdvanced_56"){
            return "WallTilesAdvanced_5"; // wall facing south
        }
        if(wallSpriteName == "WallTilesAdvanced_067" || wallSpriteName == "WallSpriteAdvanced_07" || wallSpriteName == "WallSpriteAdvanced_67"){
            return "WallTilesAdvanced_7"; // wall facing west
        }
        if(wallSpriteName == "WallTilesAdvanced_012" || wallSpriteName == "WallTilesAdvanced_01" || wallSpriteName == "WallTilesAdvanced_12"){
            return "WallTilesAdvanced_1"; // wall facing north
        }
        if(wallSpriteName == "WallTilesAdvanced_234" || wallSpriteName == "WallTilesAdvanced_23" || wallSpriteName == "WallTilesAdvanced_34"){
            return "WallTilesAdvanced_3"; // wall facing east
        }

        //Straight Walls
        //East/West wall
        if(wallSpriteName == "WallTilesAdvanced_037" || wallSpriteName == "WallTilesAdvanced_237" || wallSpriteName == "WallTilesAdvanced_347" || wallSpriteName == "WallTilesAdvanced_367"
        || wallSpriteName == "WallTilesAdvanced_0237" || wallSpriteName == "WallTilesAdvanced_0347" || wallSpriteName == "WallTilesAdvanced_0367" || wallSpriteName == "WallTilesAdvanced_2347"
        || wallSpriteName == "WallTilesAdvanced_2367" || wallSpriteName == "WallTilesAdvanced_3467" || wallSpriteName == "WallTilesAdvanced_02347" || wallSpriteName == "WallTilesAdvanced_02367"
        || wallSpriteName == "WallTilesAdvanced_03467" || wallSpriteName == "WallTilesAdvanced_23467" || wallSpriteName == "WallTilesAdvanced_023467"){
            return "WallTilesAdvanced_37"; // centre wall east to west
        }
        //North/South wall
        if(wallSpriteName == "WallTilesAdvanced_015" || wallSpriteName == "WallTilesAdvanced_125" || wallSpriteName == "WallTilesAdvanced_145" || wallSpriteName == "WallTilesAdvanced_156"
        || wallSpriteName == "WallTilesAdvanced_0125" || wallSpriteName == "WallTilesAdvanced_0145" || wallSpriteName == "WallTilesAdvanced_0156" || wallSpriteName == "WallTilesAdvanced_1245"
        || wallSpriteName == "WallTilesAdvanced_1256" || wallSpriteName == "WallTilesAdvanced_1456" || wallSpriteName == "WallTilesAdvanced_01245" || wallSpriteName == "WallTilesAdvanced_01256"
        || wallSpriteName == "WallTilesAdvanced_01456" || wallSpriteName == "WallTilesAdvanced_12456" || wallSpriteName == "WallTilesAdvanced_012456"){
            return "WallTilesAdvanced_15"; // centre wall north to south
        }

        //corners
        //South/West corner
        if(wallSpriteName == "WallTilesAdvanced_057" || wallSpriteName == "WallTilesAdvanced_257" || wallSpriteName == "WallTilesAdvanced_457" || wallSpriteName == "WallTilesAdvanced_0257"
        || wallSpriteName == "WallTilesAdvanced_0457" || wallSpriteName == "WallTilesAdvanced_2457" || wallSpriteName == "WallTilesAdvanced_02457"){
            return "WallTilesAdvanced_57"; // south west corner
        }
        //South/East corner
        if(wallSpriteName == "WallTilesAdvanced_035" || wallSpriteName == "WallTilesAdvanced_235" || wallSpriteName == "WallTilesAdvanced_356"){
            return "WallTilesAdvanced_35"; // south east corner
        }

        //North/West corner

        // return "WallSpriteAdvanced_17"; //North/West corner

        //North/East corner

        // return "WallSpriteAdvanced_13"; //North/East corner

        //filled corners

        //South/West filled corner

        // return "WallSpriteAdvanced_567"; //South/West filled corner

        //South/East filled corner

        // return "WallSpriteAdvanced_345"; //South/East filled corner

        //North/West filled corner

        // return "WallSpriteAdvanced_017"; //North/West filled corner

        //North/East filled corner

        // return "WallSpriteAdvanced_123"; //North/East filled corner

        //T junctions

        //North T junction
        
        //East T junction
        
        //South T junction

        //West T junction

        //north wall

        //east wall

        //south wall

        //west wall

        //catch incase missing
        // if(!validWallSpriteNames.Contains<string>(wallSpriteName)){
        //     return "WallSpriteAdvanced_01234567";
        // }

        return wallSpriteName;
    }
}