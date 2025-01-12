using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
public class TileDetailController : MonoBehaviour{

    Sprite[] basicTiles; 
    Dictionary<string,Sprite> wallTiles;// needs changing later when we have more tile types
    [SerializeField] Sprite tileDetailSprite;
    SpriteRenderer sr;
    public TileDetail TileDetailData { get; set; }
    public void Initialise(TileDetail tileDetailData){

        basicTiles = Resources.LoadAll<Sprite>("Sprites/VeryBasicTiles");
        Sprite[] wallTilesArray = Resources.LoadAll<Sprite>("Sprites/Walls/WoodWallSet");

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
    // private void OnEnable(){
    //     // Debug.Log("Tile detail controller enabled");
    //     if(TileDetailData != null){
    //         Debug.Log("Tile detail data not null");
    //         TileDetailData.OnTileDetailTypeChanged += TileDetailTypeChanged;
    //     }
    // }
    private void OnDisable(){
        if(TileDetailData != null){
            TileDetailData.OnTileDetailTypeChanged -= TileDetailTypeChanged;
        }
    }

    private void TileDetailTypeChanged(TileDetail tileDetail){
        Debug.Log("Tile detail type changed");

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
                if (wallTiles.TryGetValue("WoodWallSet_", out Sprite wallSprite))
                {
                    sr.sprite = wallSprite;
                }
                break;
        }
    }
}