using NUnit.Framework;
using UnityEngine;
public class TileDetailController : MonoBehaviour{

    Sprite[] basicTiles; // needs changing later when we have more tile types
    [SerializeField] Sprite tileDetailSprite;
    SpriteRenderer sr;
    public TileDetail TileDetailData { get; set; }
    public void Initialise(TileDetail tileDetailData){
        basicTiles = Resources.LoadAll<Sprite>("Sprites/VeryBasicTiles");
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
        }
        sr.sortingLayerName = "TileDetail";
    }

    // public void Initialise(TileDetail tileDetailData){
    //     TileDetailData = tileDetailData;
    //     GetComponent<SpriteRenderer>().sprite = tileDetailSprite;
    //     switch(TileDetailData.Type){
    //         case TileDetail.TileDetailType.Bush:
    //             GetComponent<SpriteRenderer>().material.color = Color.green;
    //             break;
    //         case TileDetail.TileDetailType.Tree:
    //             GetComponent<SpriteRenderer>().material.color = Color.green;
    //             break;
    //         case TileDetail.TileDetailType.Rock:
    //             GetComponent<SpriteRenderer>().material.color = Color.gray;
    //             break;
    //     }
    // }

    private void OnEnable(){
        if(TileDetailData != null){
            TileDetailData.OnTileDetailTypeChanged += TileDetailTypeChanged;
        }
    }
    private void OnDisable(){
        if(TileDetailData != null){
            TileDetailData.OnTileDetailTypeChanged -= TileDetailTypeChanged;
        }
    }

    private void TileDetailTypeChanged(TileDetail tileDetail){
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
        }
    }
}