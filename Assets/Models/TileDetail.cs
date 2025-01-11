using System;

public class TileDetail{
    public enum TileDetailType{
        None,
        Bush,
        Tree,
        Rock
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
    bool isTraversable = true;

    public event Action<TileDetail> OnTileDetailTypeChanged;

    public TileDetail(TileDetailType type, Tile tile){
        this.type = type;
        TileData = tile;
    }

    public void SetTileDetailType(TileDetailType newType){
        Type = newType;
    }
}