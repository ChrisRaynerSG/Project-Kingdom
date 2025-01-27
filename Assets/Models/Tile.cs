using System;

public class Tile{

    public enum TileType{
        Water,
        Grass,
        Dirt,
        Stone
    }

    public event Action<Tile> OnTileTypeChanged;

    public int GlobalPosX {get; private set;}
    public int GlobalPosY {get; private set;}
    
    public bool HasTileDetail {get; set;} = false;
    public TileDetail TileDetailData {get; set;}
    public bool HasInventoryItem {get; set;} = false;
    public InventoryItem inventoryItem {get; set;}

    public bool IsTraversable{
        get{
            if(Type == TileType.Water){
                return false;
            }
            return true;
        }
    }

    Chunk chunk;


    public TileType Type { get; private set; }

    /// <summary>
    /// Constructor for the Tile class
    /// </summary>
    /// <param name="type"></param>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    /// <param name="chunk"></param>

    public Tile(TileType type, int posX, int posY, Chunk chunk){
        Type = type;

        GlobalPosX = posX;
        GlobalPosY = posY;
        this.chunk = chunk;
    }

    public void SetTileType(TileType newType){
        Type = newType;
        OnTileTypeChanged?.Invoke(this);
    }
}