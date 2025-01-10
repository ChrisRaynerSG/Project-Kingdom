public class TileDetail{
    public enum TileDetailType{
        None,
        Tree,
        Rock
    }

    TileDetailType type;
    Tile tile;

    public TileDetail(TileDetailType type, Tile tile){
        this.type = type;
        this.tile = tile;
    }
}