using System;
using System.Diagnostics;

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
    string description;

    public int MaxHitPoints { get; private set; }
    int currentHitPoints;

    public int CurrentHitPoints{
        get => currentHitPoints;
        set{
            currentHitPoints = value;
            OnTileDetailHitPointsChanged?.Invoke(this);
        }
    }
    TileDetailType type;
    public TileDetailType Type{
        get => type;
        set{
            type = value;
            OnTileDetailTypeChanged?.Invoke(this);
        }
    }

    public Tile TileData { get; private set;}
    public bool IsTraversable {get; set;} = true;
    
    public event Action<TileDetail> OnTileDetailTypeChanged;
    public event Action<TileDetail> OnTileDetailHitPointsChanged;

    public TileDetail(TileDetailType type, Tile tile){
        this.type = type;
        TileData = tile;
    }
}