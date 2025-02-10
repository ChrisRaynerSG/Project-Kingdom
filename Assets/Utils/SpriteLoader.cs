using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is responsible for loading all sprites in the game
/// Loading all sprites in one place makes it easier to manage and load sprites
/// and makes it more performant as we only load sprites once
/// </summary>
public class SpriteLoader{

    private static SpriteLoader _instance;

    public Sprite[] BasicDetailSprites {get; private set;}
    public Sprite[] WallTilesAdvanced {get; private set;}
    public Sprite[] InventoryItemSprites {get; private set;}
    public Sprite[] KoMbasicTrees {get; private set;}

    public Sprite[] KoMBigTrees {get; private set;}

    public InventoryItemSO[] InventoryItems {get; private set;}

    public Dictionary<string, InventoryItemSO> InventoryItemDictionary = new Dictionary<string, InventoryItemSO>();
    public Dictionary<string, Sprite> WallTileDictionary = new Dictionary<string, Sprite>();

    public Dictionary<string, Sprite> TileDetailDictionary = new Dictionary<string, Sprite>();


    public static SpriteLoader GetInstance{
        get{
            if(_instance == null){
                _instance = new SpriteLoader();
            }
            return _instance;
        }
    }
    private SpriteLoader(){
        // constructor that loads all sprites, add more sprites here as needed
        BasicDetailSprites = Resources.LoadAll<Sprite>("Sprites/VeryBasicTiles");
        WallTilesAdvanced = Resources.LoadAll<Sprite>("Sprites/Walls/WallTilesAdvanced");
        InventoryItems = Resources.LoadAll<InventoryItemSO>("ScriptableObjects/InventoryItems");
        KoMbasicTrees = Resources.LoadAll<Sprite>("Sprites/KoMTreeSet1");
        KoMBigTrees = Resources.LoadAll<Sprite>("Sprites/BigTree");

        // InventoryItemSprites = Resources.LoadAll<Sprite>("Sprites/InventoryItems");  

        foreach(Sprite sprite in WallTilesAdvanced){
            WallTileDictionary.Add(sprite.name, sprite);
            //Debug.Log(sprite.name);
        }
        foreach(InventoryItemSO item in InventoryItems){
            InventoryItemDictionary.Add(item.name, item);
        }
        foreach(Sprite sprite in KoMbasicTrees){
            TileDetailDictionary.Add(sprite.name, sprite);
        }
        foreach(Sprite sprite in KoMBigTrees){
            TileDetailDictionary.Add(sprite.name, sprite);
        }
        TileDetailDictionary.Add("LongGrass", Resources.Load<Sprite>("Sprites/LongGrass"));
        TileDetailDictionary.Add("LongGrassDaisies", Resources.Load<Sprite>("Sprites/LongGrassDaisies"));
        
    }
}