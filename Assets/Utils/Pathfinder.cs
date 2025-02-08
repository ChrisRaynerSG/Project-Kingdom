using System.Collections.Generic;
using UnityEngine;

public static class Pathfinder{

    public static List<Tile> findPath(Tile startTile, Tile targetTile){

        List<Tile> openList = new List<Tile>{startTile};
        HashSet<Tile> closedList = new HashSet<Tile>();

        startTile.GCost = 0;
        startTile.HCost = GetDistance(startTile, targetTile);
        startTile.ParentTile = null;

        while(openList.Count>0){

            Tile currentTile = GetTileWithLowestFCost(openList);

            if(currentTile == targetTile){
                return RetracePath(startTile, targetTile);
            }

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            foreach(Tile neighbour in TileUtils.GetNeighbourTiles(currentTile)){
                if(!neighbour.IsTraversable || closedList.Contains(neighbour) || !neighbour.TileDetailData.IsTraversable){
                    continue;
                }

                float newMovementCostToNeighbour = currentTile.GCost + GetDistance(currentTile, neighbour);
                if(newMovementCostToNeighbour < neighbour.GCost || !openList.Contains(neighbour)){
                    neighbour.GCost = newMovementCostToNeighbour;
                    neighbour.HCost = GetDistance(neighbour, targetTile);
                    neighbour.ParentTile = currentTile;

                    if(!openList.Contains(neighbour)){
                        openList.Add(neighbour);
                    }
                }
            }
        }
        return null;
    }

    private static float GetDistance(Tile startTile, Tile targetTile){
        int dx = Mathf.Abs(startTile.GlobalPosX - targetTile.GlobalPosX);
        int dy = Mathf.Abs(startTile.GlobalPosY - targetTile.GlobalPosY);
        return (dx > dy) ? (1.4f * dy + (dx - dy)) : (1.4f * dx + (dy - dx));
    }

    private static Tile GetTileWithLowestFCost(List<Tile> tiles){
        Tile lowestFCostTile = tiles[0];
        foreach (Tile tile in tiles)
        {
            if (tile.FCost < lowestFCostTile.FCost ||
                (tile.FCost == lowestFCostTile.FCost && tile.HCost < lowestFCostTile.HCost))
            {
                lowestFCostTile = tile;
            }
        }
        return lowestFCostTile;
    }

    private static List<Tile> RetracePath(Tile startTile, Tile endTile){
        List<Tile> path = new List<Tile>();
        Tile currentTile = endTile;

        while(currentTile != startTile){
            path.Add(currentTile);
            currentTile = currentTile.ParentTile;
        }
        path.Reverse();
        return path;
    }
}