using UnityEngine;
//using UnityEngine.Rendering;

public class Chunk{

    public Vector2Int position { get; private set; }
    public Tile[,] Tiles { get; private set; }
    

    public float[,] HeightMap { get; private set; }
    public float[,] TemperatureMap { get; private set; }
    public float[,] HumidityMap { get; private set; }
    public float[,] BiomeMap { get; private set; }



    /// <summary>
    /// Constructor for the Chunk class, generates a new chunk from a given position
    /// </summary>
    /// <param name="position"></param>
    /// <param name="world"></param>
    /// <description>Generates a new chunk with a given position</description>
    public Chunk(Vector2Int position){

        HeightMap = SimpleNoise.GenerateNoiseMap(World.chunkSize, World.chunkSize, World.Seed, 10, 3, 0.5f, 2, position);
        TemperatureMap = SimpleNoise.GenerateNoiseMap(World.chunkSize, World.chunkSize, World.Seed, 10, 3, 0.5f, 2, position);
        HumidityMap = SimpleNoise.GenerateNoiseMap(World.chunkSize, World.chunkSize, World.Seed, 10, 3, 0.5f, 2, position);
        BiomeMap = SimpleNoise.GenerateNoiseMap(World.chunkSize, World.chunkSize, World.Seed, 10, 3, 0.5f, 2, position);

        this.position = position;
        Tiles = new Tile[World.chunkSize, World.chunkSize];
        GenerateTiles();
    }



    ///<summary>Generates the tiles for the chunk</summary>
    private void GenerateTiles(){
        for(int x = 0; x < World.chunkSize +(position.y * World.chunkSize); x++){
            for(int y = 0; y < World.chunkSize; y++){
                Tile.TileType type = GetTileTypeFromNoise(x, y);
                Tile tile = new Tile(type, x+(position.x*World.chunkSize), y+(position.y*World.chunkSize), this);
                Tiles[x, y] = tile;
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

        if(height < 0.4f){
            return Tile.TileType.Water;
        }
        if(height < 0.5f){
            return Tile.TileType.Dirt;
        }
        if(height < 0.6f){
            return Tile.TileType.Grass;
        }
        return Tile.TileType.Stone;
    }
}
