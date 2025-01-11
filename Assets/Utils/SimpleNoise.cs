using UnityEngine;
public static class SimpleNoise{


    /// <summary>
    /// Generates a simple noise map using Perlin Noise with one octave
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="seed"></param>
    /// <param name="scale"></param>
    /// <param name="octaves"></param>
    /// <param name="persistance"></param>
    /// <param name="lacunarity"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static float[,] GenerateSimpleNoiseMap(int width, int height, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset){
        float[,] noiseMap = new float[width, height];

        System.Random prng = new System.Random(seed);

        float offsetX = prng.Next(-100000, 100000);
        float offsetY = prng.Next(-100000, 100000);

        for(int x =0; x<width; x++){
            for(int y = 0; y<height; y++){
                float sampleX = (x + offset.x + offsetX) / scale;
                float sampleY = (y + offset.y + offsetY) / scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }

        // Advanced Perlin Noise DOES NOT WORK
        // System.Random prng = new System.Random(seed);
        // Vector2[] octaveOffsets = new Vector2[octaves];
        // for(int i = 0; i < octaves; i++){
        //     float offsetX = prng.Next(-100000, 100000);
        //     float offsetY = prng.Next(-100000, 100000);
        //     octaveOffsets[i] = new Vector2(offsetX, offsetY);
        // }

        // if(scale <= 0){
        //     scale = 0.0001f;
        // }

        // float maxNoiseHeight = float.MinValue;
        // float minNoiseHeight = float.MaxValue;

        // float halfWidth = width / 2f;
        // float halfHeight = height / 2f;

        // for(int y = 0; y < height; y++){
        //     for(int x = 0; x < width; x++){
        //         float amplitude = 1;
        //         float frequency = 1;
        //         float noiseHeight = 0;

        //         for(int i = 0; i < octaves; i++){
        //             float sampleX = (x + offset.x + octaveOffsets[i].x) / scale * frequency;
        //             float sampleY = (y + offset.y + octaveOffsets[i].y) / scale * frequency;

        //             float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
        //             noiseHeight += perlinValue * amplitude;
        //             noiseHeight = perlinValue;

        //             amplitude *= persistance;
        //             frequency *= lacunarity;
        //         }

        //         if(noiseHeight > maxNoiseHeight){
        //             maxNoiseHeight = noiseHeight;
        //         }
        //         else if(noiseHeight < minNoiseHeight){
        //             minNoiseHeight = noiseHeight;
        //         }
        //         noiseMap[x, y] = noiseHeight;
        //     }
        // }

        // for(int y = 0; y < height; y++){
        //     for(int x = 0; x < width; x++){
        //         noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
        //     }
        // }

        return noiseMap;
    }

    /// <summary>
    /// Generates an advanced noise map using Perlin Noise with multiple octaves
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="seed"></param>
    /// <param name="scale"></param>
    /// <param name="octaves"></param>
    /// <param name="persistance"></param>
    /// <param name="lacunarity"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static float[,] GenerateAdvancedNoiseMap(int width, int height, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset){
        
        float[,] noiseMap = new float[width, height];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for(int i = 0; i < octaves; i++){
            float offsetX = prng.Next(-100000, 100000)+ offset.x;
            float offsetY = prng.Next(-100000, 100000)+ offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if(scale <= 0){
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for(int y = 0; y < height; y++){
            for(int x = 0; x < width; x++){
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for(int i = 0; i < octaves; i++){
                    float sampleX = (x + offset.x + octaveOffsets[i].x) / scale * frequency;
                    float sampleY = (y + offset.y + octaveOffsets[i].y) / scale * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if(noiseHeight > maxNoiseHeight){
                    maxNoiseHeight = noiseHeight;
                }
                else if(noiseHeight < minNoiseHeight){
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        for(int y = 0; y < height; y++){
            for(int x = 0; x < width; x++){
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }
}