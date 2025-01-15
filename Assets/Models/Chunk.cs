using UnityEngine;
//using UnityEngine.Rendering;

public class Chunk{

    public Vector2Int position { get; private set; }
    public Tile[,] Tiles { get; private set; }
    

    public float[,] HeightMap { get; private set; }
    public float[,] TemperatureMap { get; private set; }
    public float[,] HumidityMap { get; private set; }
    public float[,] BiomeMap { get; private set; }
    public float[,] DetailMap { get; private set; }



    /// <summary>
    /// Constructor for the Chunk class, generates a new chunk from a given position
    /// </summary>
    /// <param name="position"></param>
    /// <param name="world"></param>
    /// <description>Generates a new chunk with a given position</description>
    public Chunk(Vector2Int position){

        HeightMap = SimpleNoise.GenerateSimpleNoiseMap(World.chunkSize, World.chunkSize, World.Seed, 200, 4, 0.5f, 2, position*World.chunkSize);
        TemperatureMap = SimpleNoise.GenerateSimpleNoiseMap(World.chunkSize, World.chunkSize, World.Seed, 10, 3, 0.5f, 2, position*World.chunkSize);
        HumidityMap = SimpleNoise.GenerateSimpleNoiseMap(World.chunkSize, World.chunkSize, World.Seed, 10, 3, 0.5f, 2, position*World.chunkSize);
        BiomeMap = SimpleNoise.GenerateSimpleNoiseMap(World.chunkSize, World.chunkSize, World.Seed, 10, 3, 0.5f, 2, position*World.chunkSize);
        DetailMap = SimpleNoise.GenerateAdvancedNoiseMap(World.chunkSize, World.chunkSize, World.Seed, 10, 2, 0.8f, 3, position*World.chunkSize);

        this.position = position;
        Tiles = new Tile[World.chunkSize, World.chunkSize];
        GenerateChunkTiles();
        GenerateChunkDetails();
    }

    ///<summary>Generates the tiles for the chunk</summary>
    private void GenerateChunkTiles(){
        for(int x = 0; x < World.chunkSize; x++){
            for(int y = 0; y < World.chunkSize; y++){
                Tile.TileType type = GetTileTypeFromNoise(x, y);
                Tiles[x,y] = new Tile(type, x+(position.x*World.chunkSize), y+(position.y*World.chunkSize), this);
            }
        }
    }

    private void GenerateChunkDetails(){
        for(int x = 0; x < World.chunkSize; x++){
            for(int y = 0; y < World.chunkSize; y++){
                if(DetailMap[x, y] > 0.7f){
                    if(Tiles[x, y].Type == Tile.TileType.Grass){
                        Tiles[x, y].HasTileDetail = true;
                        if(DetailMap[x, y] > 0.95f){
                            Tiles[x, y].TileDetailData = new TileDetail(TileDetail.TileDetailType.Tree, Tiles[x, y]);
                        }
                        else
                        Tiles[x, y].TileDetailData = new TileDetail(TileDetail.TileDetailType.Bush, Tiles[x, y]);
                    }
                    else if(Tiles[x, y].Type == Tile.TileType.Dirt){
                        Tiles[x, y].HasTileDetail = true;
                        Tiles[x, y].TileDetailData = new TileDetail(TileDetail.TileDetailType.Rock, Tiles[x, y]);
                        // need to check for adjacent tiles and change if they're rocks
                    }
                }
            }
        }
        //run again after initial setup to ensure adjacent rocks are changed
        foreach(Tile tile in Tiles){
            if(tile.HasTileDetail){
                TileDetail td = tile.TileDetailData;
                if(td.Type == TileDetail.TileDetailType.Rock){
                    td.Type = TileDetail.TileDetailType.Rock; // will this work or will I need to set it to none first?
                }
            }
        }
    }

    /// <summary>
    /// returns the type of tile based on combined noise maps
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private Tile.TileType GetTileTypeFromNoise(int x, int y){
        //@TODO: improve upon this method, it is very basic and does not take into account temperature, humidity, or biome. Maybe create a new class for this?
        float height = HeightMap[x, y];
        float temperature = TemperatureMap[x, y];
        float humidity = HumidityMap[x, y];
        float biome = BiomeMap[x, y];

        if(height < 0.3f){//oceans
            // temperature
            // biome
            return Tile.TileType.Water;
        }
        if(height < 0.34f){ //beaches
        // temperature
        // humidity
        // biome
            return Tile.TileType.Dirt;
        }
        if(height < 0.85f){//regular
            // temperature
            // humidity
            // biome
            return Tile.TileType.Grass;
        }
        if(height < 0.98f){//mountains
            return Tile.TileType.Dirt;
        }
        else{//peaks
            return Tile.TileType.Stone;
        }
    }
}
