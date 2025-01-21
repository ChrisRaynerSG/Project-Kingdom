using System;




public class TileDetail{ 
    // extend this class with buildable tile detail which includes materials and costs? 
    //maybe extend for different types of trees, rocks etc?
    public enum TileDetailType{
        None,
        Bush,
        Tree,
        Rock,
        Wall
    }

    public string name;

    public string spriteName;

    public string description;
    public string Description { get => description;  set{
        description = value;
    }}

    public int maxHitPoints;
    public int MaxHitPoints { get => maxHitPoints; set{
        maxHitPoints = value;
        currentHitPoints = maxHitPoints;
    }}

    public InventoryItem DroppedItem {get; private set;}

    int currentHitPoints;

    bool isFlammable;
    public bool isHarvestable;


    public int CurrentHitPoints{
        get => currentHitPoints;
        set{
            currentHitPoints = value;
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
    //public event Action<TileDetail> OnTileDetailHitPointsChanged;

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
                Description = "A small bush";
                MaxHitPoints = 10;
                isFlammable = true;
                isHarvestable = true;
                IsTraversable = true;
                DroppedItem = new InventoryItem(SpriteLoader.GetInstance.InventoryItems[0], UnityEngine.Random.Range(0,5), TileData); // maybe change this to use a dictionary name to grab the item
                break;

            case TileDetailType.Tree:
                name = "Tree";
                Description = "A large tree";
                MaxHitPoints = 50;
                isFlammable = true;
                isHarvestable = true;
                IsTraversable = false;
                DroppedItem = new InventoryItem(SpriteLoader.GetInstance.InventoryItemDictionary.TryGetValue("Wood", out InventoryItemSO wood) ? wood : null, UnityEngine.Random.Range(2, 9), TileData);
                break;

            case TileDetailType.Rock:
                name = "Rock";
                Description = "A large rock";
                MaxHitPoints = 100;
                isFlammable = false;
                isHarvestable = false; // set to true later when fixed
                IsTraversable = false;
                break;

            case TileDetailType.Wall:
                name = "Wall";
                Description = "A wall";
                MaxHitPoints = 100;
                isFlammable = false;
                isHarvestable = false;
                IsTraversable = false;
                break;

            case TileDetailType.None:
                name = "None";
                Description = "No detail";
                MaxHitPoints = 0;
                isFlammable = false;
                isHarvestable = false;
                IsTraversable = true;
                TileData.HasTileDetail = false;
                break;

            default:
                UnityEngine.Debug.Assert(false, "TileDetailType not handled");
                break;
        }
    }
}