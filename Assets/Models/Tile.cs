using System;
using System.Numerics;

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

    public string DescriptionShort {get; set;}
    public string DescriptionLong {get; set;}

    // Pathfinding properties
    public Tile ParentTile {get; set;}
    public float GCost {get; set;}
    public float HCost {get; set;}
    public float FCost {get{return GCost + HCost;}}
    public Vector2 Position {get{return new Vector2(GlobalPosX, GlobalPosY);}}

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
        GetTileDescriptionFromType(Type);
    }

    public void SetTileType(TileType newType){
        Type = newType;
        OnTileTypeChanged?.Invoke(this);
    }

    public void GetTileDescriptionFromType(TileType type){
        switch(type){
            case TileType.Water:
                DescriptionShort = "A pool of water, clear and shimmering.";
                DescriptionLong = "A clear, shimmering pool of water ripples gently, reflecting the sky above. The cool surface invites a quick swim, the chance to cast a line for fish, or a refreshing drink to quench your thirst.";
                break;
            case TileType.Grass:
                DescriptionShort = "A patch of grass, soft and green.";
                DescriptionLong = "A patch of soft, green grass stretches beneath your feet, swaying gently in the breeze. The cool ground is dotted with wildflowers and worn spots, perfect for a campsite or hiding something beneath the surface.";
                break;
            case TileType.Dirt:
                DescriptionShort = "A stretch of dirt, dry and dusty.";
                DescriptionLong =  "A stretch of dry, dusty dirt spreads out beneath your feet, marked by scattered footprints and small stones. It’s perfect for setting up camp, planting crops, or leaving tracks behind as you pass through.";
                break;
            case TileType.Stone:
                DescriptionShort = "A solid stone surface, cool and rough.";
                DescriptionLong = "A solid, weathered stone surface lies underfoot, cool and rough to the touch. Perfect for building sturdy structures, gathering loose rocks, or navigating rugged terrain with careful steps.";
                break;
            default:
                break;
        }
    }
}