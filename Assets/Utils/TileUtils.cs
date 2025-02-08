using System.Collections.Generic;
using UnityEngine;

public static class TileUtils{

    public static List<Tile> GetNeighbourTiles(Tile tile){

        List<Tile> neighbourTiles = new List<Tile>();

        Vector2Int[] directions = {
            new Vector2Int(-1, 1),
            new Vector2Int(0, 1),
            new Vector2Int(1,1),
            new Vector2Int(1, 0),
            new Vector2Int(1, -1),
            new Vector2Int(0, -1),
            new Vector2Int(-1, -1),
            new Vector2Int(-1, 0)
        };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourPosition = new Vector2Int(tile.GlobalPosX + direction.x, tile.GlobalPosY + direction.y);
            Tile neighbourTile = WorldController.Instance.GetTileFromGlobalPosition(neighbourPosition);
            if(neighbourTile != null){
                neighbourTiles.Add(neighbourTile);
            }
        }
        return neighbourTiles;
    }

    public static Tile FindClosestNeighbourToPlayer(Tile tile, PlayerMovementController playerMovementController){
        List<Tile> neighbours = GetNeighbourTiles(tile);
        List<Tile> traversableNeighbours = new List<Tile>();
        Tile closestTile = null;
        float closestDistance = 1000f;
        foreach(Tile neighbour in neighbours){
            if(Pathfinder.findPath(WorldController.Instance.GetTileFromGlobalPosition(new Vector2Int(
                (int)playerMovementController.playerTransform.position.x, (int)playerMovementController.playerTransform.position.y
            )), neighbour) != null){
                traversableNeighbours.Add(neighbour);
            }
            if(traversableNeighbours.Count > 0){
                foreach(Tile traversableNeighbour in traversableNeighbours){
                    if(Vector2.Distance(playerMovementController.playerTransform.position, new Vector2(traversableNeighbour.GlobalPosX, traversableNeighbour.GlobalPosY)) < closestDistance){
                        closestDistance = Vector2.Distance(playerMovementController.playerTransform.position, new Vector2(traversableNeighbour.GlobalPosX, traversableNeighbour.GlobalPosY));
                        closestTile = traversableNeighbour;
                    }
                }
            }
        }
        return closestTile;
    }

    public static Tile FindClosestNeighbourToStartTile(Tile startTile, Tile endTile ){
        Vector2 startTilePosition = new Vector2(startTile.GlobalPosX, startTile.GlobalPosY);
        List<Tile> neighbours = GetNeighbourTiles(endTile);
        List<Tile> traversableNeighbours = new List<Tile>();
        Tile closestTile = null;
        float closestDistance = 1000f;
        foreach(Tile neighbour in neighbours){
            if(Pathfinder.findPath(startTile, endTile) != null){
                traversableNeighbours.Add(neighbour);
            }
            if(traversableNeighbours.Count > 0){
                foreach(Tile traversableNeighbour in traversableNeighbours){
                    if(Vector2.Distance(startTilePosition, new Vector2(traversableNeighbour.GlobalPosX, traversableNeighbour.GlobalPosY)) < closestDistance){
                        closestDistance = Vector2.Distance(startTilePosition, new Vector2(traversableNeighbour.GlobalPosX, traversableNeighbour.GlobalPosY));
                        closestTile = traversableNeighbour;
                    }
                }
            }
        }
        return closestTile;
    }
}