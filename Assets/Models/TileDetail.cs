using System;
using System.Collections;
using UnityEngine;


public class TileDetail{ 
    // extend this class with buildable tile detail which includes materials and costs? 
    //maybe extend for different types of trees, rocks etc?
    public enum TileDetailType{
        None,
        Bush,
        Tree,
        Rock,
        Wall,
        LongGrass,
        LongGrassDaisies,

        BigTree
    }

    public string name;

    public string spriteName;

    public string descriptionShort;
    public string descriptionLong;
    public string DescriptionShort { get => descriptionShort;  set{
        descriptionShort = value;
    }}

    public string DescriptionLong { get => descriptionLong;  set{
        descriptionLong = value;
    }}

    public float maxHitPoints;
    public float MaxHitPoints { get => maxHitPoints; set{
        maxHitPoints = value;
        currentHitPoints = maxHitPoints;
    }}

    public InventoryItem DroppedItem {get; set;}

    float currentHitPoints;

    bool isFlammable;
    public bool isHarvestable;

    public bool isInteractable;

    public bool isMineable;

    public bool isChoppable;

    public TileDetailController TileDetailController {get; set;}


    public float CurrentHitPoints{
        get => currentHitPoints;
        set{
            currentHitPoints = value;
            OnTileDetailHitPointsChanged?.Invoke(this);
            if(currentHitPoints <= 0){
                currentHitPoints = 0;
                OnTileHitpointsZero?.Invoke(this);
                Type = TileDetailType.None;
            }
        }
    }
    TileDetailType type;
    public TileDetailType Type{
        get => type;
        set
        {
            type = value;
            SetTileType();
            OnTileDetailPropertyChanged?.Invoke(this);
        }
    }
    public Tile TileData { get; private set;}
    public bool IsTraversable {get; set;} = true;
    
    public event Action<TileDetail> OnTileDetailPropertyChanged;
    public event Action<TileDetail> OnTileHitpointsZero;
    public event Action<TileDetail> OnTileDetailHitPointsChanged;

    public TileDetail(TileDetailType type, Tile tile){
        this.type = type;
        TileData = tile;
        SetTileType();
    }
    private void SetTileType()
    {
        switch (type)
        {
            case TileDetailType.Bush:
                name = "Bush";
                DescriptionShort = "A small bush";
                DescriptionLong = "A dense berry bush sprawls with tangled branches, dotted with clusters of ripe, juicy berries. Its vibrant fruit offers a quick snack or a useful ingredient, while the thick foliage provides a bit of natural cover.";
                MaxHitPoints = 10;
                isFlammable = true;
                isHarvestable = true;
                IsTraversable = true;
                DroppedItem = new InventoryItem(SpriteLoader.GetInstance.InventoryItems[0], UnityEngine.Random.Range(0,5), TileData); // maybe change this to use a dictionary name to grab the item
                break;

            case TileDetailType.Tree:
                name = "Tree";
                DescriptionShort = "A large tree";
                DescriptionLong = "A towering oak tree stands tall, its thick, gnarled branches stretching wide to cast cool, dappled shade below. Its sturdy trunk is perfect for harvesting wood, and the rustling leaves provide a calm backdrop to the wilderness around you";
                MaxHitPoints = 50;
                isFlammable = true;
                isHarvestable = true;
                IsTraversable = false;
                isChoppable = true;
                DroppedItem = new InventoryItem(SpriteLoader.GetInstance.InventoryItemDictionary.TryGetValue("Wood", out InventoryItemSO wood) ? wood : null, UnityEngine.Random.Range(2, 9), TileData);
                break;

            case TileDetailType.Rock:
                name = "Rock";
                DescriptionShort = "A large rock";
                DescriptionLong = "A massive, jagged rock juts from the ground, its rough surface too steep and uneven to climb. Solid and unyielding, it’s impossible to traverse but can be mined for valuable stone and minerals.";
                MaxHitPoints = 100;
                isFlammable = false;
                isHarvestable = false; // set to true later when fixed
                IsTraversable = false;
                isMineable = true;
                break;

            case TileDetailType.Wall:
                name = "Wall";
                DescriptionShort = "A wooden wall";
                DescriptionLong = "A sturdy wooden wall stands firm, its tightly bound planks reinforced to block any passage. Though impassable, it can be dismantled with tools, smashed apart with force, or set ablaze by a cunning arsonist looking for a quicker way through.";
                MaxHitPoints = 100;
                isFlammable = false;
                isHarvestable = false;
                IsTraversable = false;
                isChoppable = true;
                break;

            case TileDetailType.None:
                name = "None";
                DescriptionShort = "No detail";
                DescriptionLong = "No detail";
                MaxHitPoints = 0;
                isFlammable = false;
                isHarvestable = false;
                IsTraversable = true;
                TileData.HasTileDetail = false;
                break;
            
            case TileDetailType.LongGrass:
                name = "Long Grass";
                DescriptionShort = "Long grassy grass... grass.";
                DescriptionLong = "Grass so Long it's like a jungle, but not really, because it's just grass and not a jungle. It's just grass. Long grass.";
                MaxHitPoints = 5;
                isFlammable = true;
                isHarvestable = false;
                IsTraversable = true;
                break;

            case TileDetailType.LongGrassDaisies:
                name = "Daisy Patch";
                DescriptionShort = "A patch of daisies";
                DescriptionLong = "A patch of daisies, the flowers are white with yellow centers. The daisies are surrounded by long grass.";
                MaxHitPoints = 5;
                isFlammable = true;
                isHarvestable = false;
                IsTraversable = true;
                break;

            case TileDetailType.BigTree:
                name = "Big Tree";
                DescriptionShort = "A big tree";
                DescriptionLong = "A big tree, it stands tall and proud, its thick, gnarled branches stretching wide to cast cool, dappled shade below. Its sturdy trunk is perfect for harvesting wood, and the rustling leaves provide a calm backdrop to the wilderness around you";
                MaxHitPoints = 100;
                isFlammable = true;
                isHarvestable = true;
                IsTraversable = false;
                isChoppable = true;
                DroppedItem = new InventoryItem(SpriteLoader.GetInstance.InventoryItemDictionary.TryGetValue("Wood", out InventoryItemSO wood2) ? wood2 : null, UnityEngine.Random.Range(4, 14), TileData);
                break;

            default:
                UnityEngine.Debug.Assert(false, "TileDetailType not handled");
                break;
        }
    }
}