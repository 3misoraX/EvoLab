using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PathFinding
{
    //Tile list to create a path
    public List<OverlayTile> Path(OverlayTile start, OverlayTile end)
    {
        //two states of tile
        List<OverlayTile> openList = new List<OverlayTile>();
        List<OverlayTile> closedList = new List<OverlayTile>();

        openList.Add(start);
        while(openList.Count > 0)
        {
            OverlayTile current = openList.OrderBy(x => x.F).First();

            openList.Remove(current);
            closedList.Add(current);

            if(current == end)
            {
                return GetFinishedList(start, end);
            }
            //get neighboring tiles
            var neighbor = GetNeighbors(current);
            
            foreach(var n in neighbor)
            {
                if(n.blocked||closedList.Contains(n))
                {
                    continue;
                }
                //gets manhattan distance, in numbres G and H, F calculates automatically
                n.G = GetManhattanDistance(start, n);
                n.H = GetManhattanDistance(end, n);
                
                n.previous = current;
                
                if(!openList.Contains(n))
                {
                    openList.Add(n);
                }
            }
        }

        return new List<OverlayTile>();
    }
    //creates path
    public List<OverlayTile> GetFinishedList(OverlayTile start, OverlayTile end)
    {
        List<OverlayTile> list = new List<OverlayTile>();
        OverlayTile current = end;
        while(current != start)
        {
            list.Add(current);
            current = current.previous;
        }

        list.Reverse();

        return list;
    }

    //calculates manhattan distance between two points
    public int GetManhattanDistance(OverlayTile start, OverlayTile end)
    {
        return Mathf.Abs(start.location.x - end.location.x) + Mathf.Abs(start.location.y - end.location.y);
    }
    
    public List<OverlayTile> GetNeighbors(OverlayTile tile) 
    {
        var map = Map.Instance.map;

        List<OverlayTile> neighborTiles = new List<OverlayTile>();
        //bottom
        //Creates a list with neighboring tiles from current one
        Vector2Int locationToCheck = new Vector2Int(tile.location.x, tile.location.y-1);

        if(map.ContainsKey(locationToCheck))
        {
            neighborTiles.Add(map[locationToCheck]);
        }
        //top
        locationToCheck = new Vector2Int(tile.location.x, tile.location.y+1);

        if(map.ContainsKey(locationToCheck))
        {
            neighborTiles.Add(map[locationToCheck]);
        }
        //left
        locationToCheck = new Vector2Int(tile.location.x-1, tile.location.y);

        if(map.ContainsKey(locationToCheck))
        {
            neighborTiles.Add(map[locationToCheck]);
        }
        //right
        locationToCheck = new Vector2Int(tile.location.x+1, tile.location.y);

        if(map.ContainsKey(locationToCheck))
        {
            neighborTiles.Add(map[locationToCheck]);
        }

        return neighborTiles;
    }
}
