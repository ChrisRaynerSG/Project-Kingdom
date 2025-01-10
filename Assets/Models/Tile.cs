public class Tile{

    public enum TileType{
        Water,
        Grass,
        Dirt,
        Stone
    }

    public int GlobalPosX {get; private set;}
    public int GlobalPosY {get; private set;}
    
    bool hasTileDetail = false;
    TileDetail tileDetail;
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
}